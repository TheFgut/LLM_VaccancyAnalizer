namespace VaccancyAnalizer.JobAnalyticsMaking
{
    partial class VacanciesNetLoader
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
            QueryTextBox = new RichTextBox();
            RequestVacanciesBut = new Button();
            progressBar1 = new ProgressBar();
            VacanciesLimitTextBox = new TextBox();
            label1 = new Label();
            TranslateToEnglishCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // QueryTextBox
            // 
            QueryTextBox.Location = new Point(12, 12);
            QueryTextBox.Name = "QueryTextBox";
            QueryTextBox.Size = new Size(285, 107);
            QueryTextBox.TabIndex = 0;
            QueryTextBox.Text = "";
            QueryTextBox.Validated += QueryTextBox_Validated;
            // 
            // RequestVacanciesBut
            // 
            RequestVacanciesBut.Location = new Point(12, 180);
            RequestVacanciesBut.Name = "RequestVacanciesBut";
            RequestVacanciesBut.Size = new Size(285, 23);
            RequestVacanciesBut.TabIndex = 1;
            RequestVacanciesBut.Text = "Request vacancies";
            RequestVacanciesBut.UseVisualStyleBackColor = true;
            RequestVacanciesBut.Click += RequestVacanciesBut_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 209);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(285, 23);
            progressBar1.TabIndex = 2;
            // 
            // VacanciesLimitTextBox
            // 
            VacanciesLimitTextBox.Location = new Point(173, 149);
            VacanciesLimitTextBox.Name = "VacanciesLimitTextBox";
            VacanciesLimitTextBox.Size = new Size(124, 23);
            VacanciesLimitTextBox.TabIndex = 3;
            VacanciesLimitTextBox.Validated += VacanciesLimitTextBox_Validated;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F);
            label1.Location = new Point(12, 149);
            label1.Name = "label1";
            label1.Size = new Size(155, 21);
            label1.TabIndex = 4;
            label1.Text = "Vacancies count limit";
            // 
            // TranslateToEnglishCheckBox
            // 
            TranslateToEnglishCheckBox.AutoSize = true;
            TranslateToEnglishCheckBox.Checked = true;
            TranslateToEnglishCheckBox.CheckState = CheckState.Checked;
            TranslateToEnglishCheckBox.Location = new Point(12, 127);
            TranslateToEnglishCheckBox.Name = "TranslateToEnglishCheckBox";
            TranslateToEnglishCheckBox.Size = new Size(127, 19);
            TranslateToEnglishCheckBox.TabIndex = 5;
            TranslateToEnglishCheckBox.Text = "Translate to english";
            TranslateToEnglishCheckBox.UseVisualStyleBackColor = true;
            TranslateToEnglishCheckBox.CheckedChanged += TranslateToEnglishCheckBox_CheckedChanged;
            // 
            // VacanciesNetLoader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(309, 244);
            Controls.Add(TranslateToEnglishCheckBox);
            Controls.Add(label1);
            Controls.Add(VacanciesLimitTextBox);
            Controls.Add(progressBar1);
            Controls.Add(RequestVacanciesBut);
            Controls.Add(QueryTextBox);
            Name = "VacanciesNetLoader";
            Text = "VacancienNetLoader";
            FormClosing += VacanciesNetLoader_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox QueryTextBox;
        private Button RequestVacanciesBut;
        private ProgressBar progressBar1;
        private TextBox VacanciesLimitTextBox;
        private Label label1;
        private CheckBox TranslateToEnglishCheckBox;
    }
}