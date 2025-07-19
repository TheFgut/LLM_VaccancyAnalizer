namespace VaccancyAnalizer.JobAnalyticsMaking
{
    partial class AnalyticsMakerForm
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
            vacanciesLoader1 = new VacanciesLoader();
            StartAnalizeBut = new Button();
            progressBar1 = new ProgressBar();
            PromtTextBox = new RichTextBox();
            llM_Loader = new VaccancyAnalizer.LLM_models.UI.LLM_Loader();
            SaveInWordFormatCheckBox = new CheckBox();
            analysisTypeSelector = new ComboBox();
            SuspendLayout();
            // 
            // vacanciesLoader1
            // 
            vacanciesLoader1.Location = new Point(12, 12);
            vacanciesLoader1.Name = "vacanciesLoader1";
            vacanciesLoader1.Size = new Size(237, 163);
            vacanciesLoader1.TabIndex = 0;
            // 
            // StartAnalizeBut
            // 
            StartAnalizeBut.Location = new Point(12, 237);
            StartAnalizeBut.Name = "StartAnalizeBut";
            StartAnalizeBut.Size = new Size(237, 23);
            StartAnalizeBut.TabIndex = 2;
            StartAnalizeBut.Text = "Start analize";
            StartAnalizeBut.UseVisualStyleBackColor = true;
            StartAnalizeBut.Click += StartAnalizeBut_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 266);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(237, 23);
            progressBar1.TabIndex = 3;
            // 
            // PromtTextBox
            // 
            PromtTextBox.Location = new Point(255, 85);
            PromtTextBox.Name = "PromtTextBox";
            PromtTextBox.Size = new Size(291, 204);
            PromtTextBox.TabIndex = 4;
            PromtTextBox.Text = "";
            PromtTextBox.Validated += PromtTextBox_Validated;
            // 
            // llM_Loader
            // 
            llM_Loader.Location = new Point(255, 12);
            llM_Loader.Name = "llM_Loader";
            llM_Loader.Size = new Size(291, 67);
            llM_Loader.TabIndex = 5;
            // 
            // SaveInWordFormatCheckBox
            // 
            SaveInWordFormatCheckBox.AutoSize = true;
            SaveInWordFormatCheckBox.Checked = true;
            SaveInWordFormatCheckBox.CheckState = CheckState.Checked;
            SaveInWordFormatCheckBox.Location = new Point(66, 212);
            SaveInWordFormatCheckBox.Name = "SaveInWordFormatCheckBox";
            SaveInWordFormatCheckBox.Size = new Size(134, 19);
            SaveInWordFormatCheckBox.TabIndex = 6;
            SaveInWordFormatCheckBox.Text = "Save in Word format";
            SaveInWordFormatCheckBox.UseVisualStyleBackColor = true;
            // 
            // analysisTypeSelector
            // 
            analysisTypeSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            analysisTypeSelector.Location = new Point(66, 183);
            analysisTypeSelector.Name = "analysisTypeSelector";
            analysisTypeSelector.Size = new Size(121, 23);
            analysisTypeSelector.TabIndex = 7;
            analysisTypeSelector.SelectedIndexChanged += analysisType_SelectedIndexChanged;
            // 
            // AnalyticsMakerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(557, 301);
            Controls.Add(analysisTypeSelector);
            Controls.Add(SaveInWordFormatCheckBox);
            Controls.Add(llM_Loader);
            Controls.Add(PromtTextBox);
            Controls.Add(progressBar1);
            Controls.Add(StartAnalizeBut);
            Controls.Add(vacanciesLoader1);
            Name = "AnalyticsMakerForm";
            Text = "AnalyticsMakerForm";
            FormClosed += AnalyticsMakerForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private VacanciesLoader vacanciesLoader1;
        private Button StartAnalizeBut;
        private ProgressBar progressBar1;
        private RichTextBox PromtTextBox;
        private LLM_models.UI.LLM_Loader llM_Loader;
        private CheckBox SaveInWordFormatCheckBox;
        private ComboBox analysisTypeSelector;
    }
}