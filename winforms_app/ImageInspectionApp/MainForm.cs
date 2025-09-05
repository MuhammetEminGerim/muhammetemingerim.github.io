using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ImageInspectionApp
{
    public partial class MainForm : Form
    {
        private string referenceImagePath = "";
        private string testFolderPath = "";
        private string pythonScriptPath = "";
        private List<AnalysisResult> analysisResults = new List<AnalysisResult>();

        public MainForm()
        {
            InitializeComponent();
            InitializePythonPath();
        }

        private void InitializePythonPath()
        {
            // Python script yolunu ayarla
            string currentDir = Directory.GetCurrentDirectory();
            pythonScriptPath = Path.Combine(currentDir, "..", "..", "python_backend", "image_analyzer.py");
            
            if (!File.Exists(pythonScriptPath))
            {
                MessageBox.Show("Python script bulunamadı: " + pythonScriptPath, "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSelectReference_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim dosyaları|*.jpg;*.jpeg;*.png;*.bmp|Tüm dosyalar|*.*";
                openFileDialog.Title = "Referans OK Fotoğrafını Seçin";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    referenceImagePath = openFileDialog.FileName;
                    txtReferencePath.Text = referenceImagePath;
                    
                    // Referans görüntüyü göster
                    try
                    {
                        picReference.Image = Image.FromFile(referenceImagePath);
                        picReference.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görüntü yüklenemedi: " + ex.Message, "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSelectTestFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Test edilecek fotoğrafların bulunduğu klasörü seçin";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    testFolderPath = folderDialog.SelectedPath;
                    txtTestFolderPath.Text = testFolderPath;
                    
                    // Klasördeki resim dosyalarını listele
                    LoadTestImages();
                }
            }
        }

        private void LoadTestImages()
        {
            if (string.IsNullOrEmpty(testFolderPath) || !Directory.Exists(testFolderPath))
                return;

            lstTestImages.Items.Clear();
            
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };
            var imageFiles = Directory.GetFiles(testFolderPath)
                .Where(file => imageExtensions.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => Path.GetFileName(file))
                .ToArray();

            foreach (string imageFile in imageFiles)
            {
                lstTestImages.Items.Add(imageFile);
            }

            lblImageCount.Text = $"Toplam {imageFiles.Length} resim bulundu";
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(referenceImagePath))
            {
                MessageBox.Show("Lütfen referans OK fotoğrafını seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(testFolderPath))
            {
                MessageBox.Show("Lütfen test edilecek fotoğrafların bulunduğu klasörü seçin.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!File.Exists(pythonScriptPath))
            {
                MessageBox.Show("Python script bulunamadı. Lütfen python_backend klasörünün doğru konumda olduğundan emin olun.", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // UI'yi devre dışı bırak
            SetControlsEnabled(false);
            progressBar.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "Analiz yapılıyor...";

            try
            {
                await Task.Run(() => RunAnalysis());
                DisplayResults();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Analiz sırasında hata oluştu: " + ex.Message, "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // UI'yi tekrar etkinleştir
                SetControlsEnabled(true);
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;
                lblStatus.Text = "Hazır";
            }
        }

        private void RunAnalysis()
        {
            try
            {
                // Python script'ini çalıştır
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = $"\"{pythonScriptPath}\" --reference \"{referenceImagePath}\" --folder \"{testFolderPath}\"",
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

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Python script hatası: {error}");
                    }

                    // JSON sonuçlarını parse et
                    var results = JsonConvert.DeserializeObject<Dictionary<string, AnalysisResult>>(output);
                    
                    analysisResults.Clear();
                    foreach (var result in results)
                    {
                        result.Value.FileName = result.Key;
                        analysisResults.Add(result.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Analiz hatası: {ex.Message}");
            }
        }

        private void DisplayResults()
        {
            dgvResults.Rows.Clear();
            
            foreach (var result in analysisResults)
            {
                string status = result.IsOk ? "OK" : "HATA";
                Color statusColor = result.IsOk ? Color.Green : Color.Red;
                
                int rowIndex = dgvResults.Rows.Add(
                    result.FileName,
                    status,
                    result.OverallScore.ToString("F1") + "%",
                    result.ErrorType ?? "OK",
                    result.SsimScore.ToString("F3"),
                    result.HistogramCorrelation.ToString("F3"),
                    result.SiftMatchRatio.ToString("F3")
                );
                
                dgvResults.Rows[rowIndex].Cells[1].Style.ForeColor = statusColor;
            }

            // Özet istatistikleri göster
            int okCount = analysisResults.Count(r => r.IsOk);
            int errorCount = analysisResults.Count - okCount;
            
            lblSummary.Text = $"Toplam: {analysisResults.Count} | OK: {okCount} | Hata: {errorCount}";
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnSelectReference.Enabled = enabled;
            btnSelectTestFolder.Enabled = enabled;
            btnAnalyze.Enabled = enabled;
            btnExportResults.Enabled = enabled;
        }

        private void btnExportResults_Click(object sender, EventArgs e)
        {
            if (analysisResults.Count == 0)
            {
                MessageBox.Show("Dışa aktarılacak sonuç bulunamadı.", "Uyarı", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV dosyaları|*.csv|JSON dosyaları|*.json|Tüm dosyalar|*.*";
                saveDialog.Title = "Sonuçları Dışa Aktar";
                saveDialog.FileName = "analiz_sonuclari";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string extension = Path.GetExtension(saveDialog.FileName).ToLower();
                        
                        if (extension == ".csv")
                        {
                            ExportToCsv(saveDialog.FileName);
                        }
                        else if (extension == ".json")
                        {
                            ExportToJson(saveDialog.FileName);
                        }
                        else
                        {
                            ExportToCsv(saveDialog.FileName + ".csv");
                        }
                        
                        MessageBox.Show("Sonuçlar başarıyla dışa aktarıldı.", "Başarılı", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Dışa aktarma hatası: " + ex.Message, "Hata", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportToCsv(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Başlık satırı
                writer.WriteLine("Dosya Adı,Durum,Genel Skor,Hata Tipi,SSIM Skoru,Histogram Korelasyonu,SIFT Eşleşme Oranı");
                
                // Veri satırları
                foreach (var result in analysisResults)
                {
                    writer.WriteLine($"{result.FileName}," +
                                   $"{(result.IsOk ? "OK" : "HATA")}," +
                                   $"{result.OverallScore:F1}%," +
                                   $"{result.ErrorType ?? "OK"}," +
                                   $"{result.SsimScore:F3}," +
                                   $"{result.HistogramCorrelation:F3}," +
                                   $"{result.SiftMatchRatio:F3}");
                }
            }
        }

        private void ExportToJson(string filePath)
        {
            string json = JsonConvert.SerializeObject(analysisResults, Formatting.Indented);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        private void lstTestImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTestImages.SelectedItem != null)
            {
                string selectedImage = lstTestImages.SelectedItem.ToString();
                string imagePath = Path.Combine(testFolderPath, selectedImage);
                
                try
                {
                    picTest.Image = Image.FromFile(imagePath);
                    picTest.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Görüntü yüklenemedi: " + ex.Message, "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    public class AnalysisResult
    {
        [JsonProperty("overall_score")]
        public double OverallScore { get; set; }
        
        [JsonProperty("is_ok")]
        public bool IsOk { get; set; }
        
        [JsonProperty("error_type")]
        public string ErrorType { get; set; }
        
        [JsonProperty("ssim_score")]
        public double SsimScore { get; set; }
        
        [JsonProperty("histogram_correlation")]
        public double HistogramCorrelation { get; set; }
        
        [JsonProperty("sift_match_ratio")]
        public double SiftMatchRatio { get; set; }
        
        [JsonProperty("sift_matches")]
        public int SiftMatches { get; set; }
        
        [JsonProperty("difference_percentage")]
        public double DifferencePercentage { get; set; }
        
        public string FileName { get; set; }
    }
}