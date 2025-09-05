namespace ImageInspectionApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picReference = new System.Windows.Forms.PictureBox();
            this.txtReferencePath = new System.Windows.Forms.TextBox();
            this.btnSelectReference = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picTest = new System.Windows.Forms.PictureBox();
            this.lstTestImages = new System.Windows.Forms.ListBox();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.txtTestFolderPath = new System.Windows.Forms.TextBox();
            this.btnSelectTestFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOverallScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSsimScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHistogramCorrelation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSiftMatchRatio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.btnExportResults = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSummary = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReference)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picReference);
            this.groupBox1.Controls.Add(this.txtReferencePath);
            this.groupBox1.Controls.Add(this.btnSelectReference);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Referans OK Fotoğrafı";
            // 
            // picReference
            // 
            this.picReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picReference.Location = new System.Drawing.Point(6, 50);
            this.picReference.Name = "picReference";
            this.picReference.Size = new System.Drawing.Size(388, 144);
            this.picReference.TabIndex = 3;
            this.picReference.TabStop = false;
            // 
            // txtReferencePath
            // 
            this.txtReferencePath.Location = new System.Drawing.Point(6, 24);
            this.txtReferencePath.Name = "txtReferencePath";
            this.txtReferencePath.ReadOnly = true;
            this.txtReferencePath.Size = new System.Drawing.Size(308, 20);
            this.txtReferencePath.TabIndex = 2;
            // 
            // btnSelectReference
            // 
            this.btnSelectReference.Location = new System.Drawing.Point(320, 22);
            this.btnSelectReference.Name = "btnSelectReference";
            this.btnSelectReference.Size = new System.Drawing.Size(74, 23);
            this.btnSelectReference.TabIndex = 1;
            this.btnSelectReference.Text = "Seç...";
            this.btnSelectReference.UseVisualStyleBackColor = true;
            this.btnSelectReference.Click += new System.EventHandler(this.btnSelectReference_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Referans OK fotoğrafını seçin:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picTest);
            this.groupBox2.Controls.Add(this.lstTestImages);
            this.groupBox2.Controls.Add(this.lblImageCount);
            this.groupBox2.Controls.Add(this.txtTestFolderPath);
            this.groupBox2.Controls.Add(this.btnSelectTestFolder);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(418, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Fotoğrafları";
            // 
            // picTest
            // 
            this.picTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picTest.Location = new System.Drawing.Point(200, 50);
            this.picTest.Name = "picTest";
            this.picTest.Size = new System.Drawing.Size(194, 144);
            this.picTest.TabIndex = 5;
            this.picTest.TabStop = false;
            // 
            // lstTestImages
            // 
            this.lstTestImages.FormattingEnabled = true;
            this.lstTestImages.Location = new System.Drawing.Point(6, 50);
            this.lstTestImages.Name = "lstTestImages";
            this.lstTestImages.Size = new System.Drawing.Size(188, 95);
            this.lstTestImages.TabIndex = 4;
            this.lstTestImages.SelectedIndexChanged += new System.EventHandler(this.lstTestImages_SelectedIndexChanged);
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(6, 151);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(0, 13);
            this.lblImageCount.TabIndex = 3;
            // 
            // txtTestFolderPath
            // 
            this.txtTestFolderPath.Location = new System.Drawing.Point(6, 24);
            this.txtTestFolderPath.Name = "txtTestFolderPath";
            this.txtTestFolderPath.ReadOnly = true;
            this.txtTestFolderPath.Size = new System.Drawing.Size(308, 20);
            this.txtTestFolderPath.TabIndex = 2;
            // 
            // btnSelectTestFolder
            // 
            this.btnSelectTestFolder.Location = new System.Drawing.Point(320, 22);
            this.btnSelectTestFolder.Name = "btnSelectTestFolder";
            this.btnSelectTestFolder.Size = new System.Drawing.Size(74, 23);
            this.btnSelectTestFolder.TabIndex = 1;
            this.btnSelectTestFolder.Text = "Seç...";
            this.btnSelectTestFolder.UseVisualStyleBackColor = true;
            this.btnSelectTestFolder.Click += new System.EventHandler(this.btnSelectTestFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Test edilecek fotoğrafların bulunduğu klasör:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvResults);
            this.groupBox3.Location = new System.Drawing.Point(12, 218);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(806, 300);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Analiz Sonuçları";
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFileName,
            this.colStatus,
            this.colOverallScore,
            this.colErrorType,
            this.colSsimScore,
            this.colHistogramCorrelation,
            this.colSiftMatchRatio});
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(3, 16);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(800, 281);
            this.dgvResults.TabIndex = 0;
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "Dosya Adı";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.Width = 150;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Durum";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.Width = 60;
            // 
            // colOverallScore
            // 
            this.colOverallScore.HeaderText = "Genel Skor";
            this.colOverallScore.Name = "colOverallScore";
            this.colOverallScore.ReadOnly = true;
            this.colOverallScore.Width = 80;
            // 
            // colErrorType
            // 
            this.colErrorType.HeaderText = "Hata Tipi";
            this.colErrorType.Name = "colErrorType";
            this.colErrorType.ReadOnly = true;
            this.colErrorType.Width = 150;
            // 
            // colSsimScore
            // 
            this.colSsimScore.HeaderText = "SSIM Skoru";
            this.colSsimScore.Name = "colSsimScore";
            this.colSsimScore.ReadOnly = true;
            this.colSsimScore.Width = 80;
            // 
            // colHistogramCorrelation
            // 
            this.colHistogramCorrelation.HeaderText = "Histogram Korelasyonu";
            this.colHistogramCorrelation.Name = "colHistogramCorrelation";
            this.colHistogramCorrelation.ReadOnly = true;
            this.colHistogramCorrelation.Width = 120;
            // 
            // colSiftMatchRatio
            // 
            this.colSiftMatchRatio.HeaderText = "SIFT Eşleşme Oranı";
            this.colSiftMatchRatio.Name = "colSiftMatchRatio";
            this.colSiftMatchRatio.ReadOnly = true;
            this.colSiftMatchRatio.Width = 120;
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAnalyze.Location = new System.Drawing.Point(12, 524);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 40);
            this.btnAnalyze.TabIndex = 3;
            this.btnAnalyze.Text = "Analiz Et";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // btnExportResults
            // 
            this.btnExportResults.Location = new System.Drawing.Point(138, 524);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(120, 40);
            this.btnExportResults.TabIndex = 4;
            this.btnExportResults.Text = "Sonuçları Dışa Aktar";
            this.btnExportResults.UseVisualStyleBackColor = true;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(264, 524);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 23);
            this.progressBar.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(264, 550);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Hazır";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSummary.Location = new System.Drawing.Point(470, 540);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(0, 17);
            this.lblSummary.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 576);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExportResults);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Görüntü Hata Tespiti Sistemi";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReference)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTest)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picReference;
        private System.Windows.Forms.TextBox txtReferencePath;
        private System.Windows.Forms.Button btnSelectReference;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picTest;
        private System.Windows.Forms.ListBox lstTestImages;
        private System.Windows.Forms.Label lblImageCount;
        private System.Windows.Forms.TextBox txtTestFolderPath;
        private System.Windows.Forms.Button btnSelectTestFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOverallScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSsimScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHistogramCorrelation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSiftMatchRatio;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.Button btnExportResults;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSummary;
    }
}