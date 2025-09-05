using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ImageDefectDetection
{
    public partial class MainForm : Form
    {
        private Button btnSelectReference;
        private Button btnSelectTestFolder;
        private Button btnStartAnalysis;
        private PictureBox picReference;
        private PictureBox picTest;
        private Label lblReference;
        private Label lblTest;
        private Label lblResult;
        private TextBox txtResults;
        private ProgressBar progressBar;
        private string referenceImagePath = "";
        private string testFolderPath = "";

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Görüntü Hata Tespiti - Ok Analizi";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Reference Image Section
            lblReference = new Label
            {
                Text = "Referans Ok Fotoğrafı:",
                Location = new Point(20, 20),
                Size = new Size(200, 20)
            };
            this.Controls.Add(lblReference);

            btnSelectReference = new Button
            {
                Text = "Referans Fotoğraf Seç",
                Location = new Point(20, 50),
                Size = new Size(150, 30)
            };
            btnSelectReference.Click += BtnSelectReference_Click;
            this.Controls.Add(btnSelectReference);

            picReference = new PictureBox
            {
                Location = new Point(20, 90),
                Size = new Size(200, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(picReference);

            // Test Folder Section
            lblTest = new Label
            {
                Text = "Test Fotoğrafları Klasörü:",
                Location = new Point(250, 20),
                Size = new Size(200, 20)
            };
            this.Controls.Add(lblTest);

            btnSelectTestFolder = new Button
            {
                Text = "Test Klasörü Seç",
                Location = new Point(250, 50),
                Size = new Size(150, 30)
            };
            btnSelectTestFolder.Click += BtnSelectTestFolder_Click;
            this.Controls.Add(btnSelectTestFolder);

            picTest = new PictureBox
            {
                Location = new Point(250, 90),
                Size = new Size(200, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(picTest);

            // Analysis Button
            btnStartAnalysis = new Button
            {
                Text = "Analizi Başlat",
                Location = new Point(480, 50),
                Size = new Size(150, 30),
                Enabled = false
            };
            btnStartAnalysis.Click += BtnStartAnalysis_Click;
            this.Controls.Add(btnStartAnalysis);

            // Progress Bar
            progressBar = new ProgressBar
            {
                Location = new Point(20, 260),
                Size = new Size(600, 20),
                Visible = false
            };
            this.Controls.Add(progressBar);

            // Results Section
            lblResult = new Label
            {
                Text = "Analiz Sonuçları:",
                Location = new Point(20, 300),
                Size = new Size(200, 20)
            };
            this.Controls.Add(lblResult);

            txtResults = new TextBox
            {
                Location = new Point(20, 330),
                Size = new Size(600, 300),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            this.Controls.Add(txtResults);
        }

        private void BtnSelectReference_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.Title = "Referans Ok Fotoğrafını Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    referenceImagePath = openFileDialog.FileName;
                    picReference.Image = Image.FromFile(referenceImagePath);
                    CheckIfReadyToAnalyze();
                }
            }
        }

        private void BtnSelectTestFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Test fotoğraflarının bulunduğu klasörü seçin";
                folderDialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    testFolderPath = folderDialog.SelectedPath;
                    CheckIfReadyToAnalyze();
                }
            }
        }

        private void CheckIfReadyToAnalyze()
        {
            btnStartAnalysis.Enabled = !string.IsNullOrEmpty(referenceImagePath) && !string.IsNullOrEmpty(testFolderPath);
        }

        private async void BtnStartAnalysis_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenceImagePath) || string.IsNullOrEmpty(testFolderPath))
            {
                MessageBox.Show("Lütfen referans fotoğraf ve test klasörünü seçin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStartAnalysis.Enabled = false;
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Marquee;
            txtResults.Clear();

            try
            {
                await Task.Run(() => AnalyzeImages());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Analiz sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                btnStartAnalysis.Enabled = true;
            }
        }

        private void AnalyzeImages()
        {
            try
            {
                // Python script'ini çalıştır
                string pythonScript = Path.Combine(Application.StartupPath, "image_analyzer.py");
                
                if (!File.Exists(pythonScript))
                {
                    this.Invoke(new Action(() => {
                        txtResults.AppendText("Python script bulunamadı!\r\n");
                    }));
                    return;
                }

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{pythonScript}\" \"{referenceImagePath}\" \"{testFolderPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    
                    process.WaitForExit();

                    this.Invoke(new Action(() => {
                        if (!string.IsNullOrEmpty(error))
                        {
                            txtResults.AppendText($"Hata: {error}\r\n");
                        }
                        txtResults.AppendText(output);
                    }));
                }
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() => {
                    txtResults.AppendText($"Analiz hatası: {ex.Message}\r\n");
                }));
            }
        }
    }
}