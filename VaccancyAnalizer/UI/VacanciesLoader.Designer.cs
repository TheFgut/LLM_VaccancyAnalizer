namespace VaccancyAnalizer
{
    partial class VacanciesLoader
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            LoadNewBut = new Button();
            LoadExistingBut = new Button();
            label1 = new Label();
            DetailsTextBox = new RichTextBox();
            SaveBut = new Button();
            SuspendLayout();
            // 
            // LoadNewBut
            // 
            LoadNewBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            LoadNewBut.Location = new Point(3, 161);
            LoadNewBut.Name = "LoadNewBut";
            LoadNewBut.Size = new Size(101, 23);
            LoadNewBut.TabIndex = 0;
            LoadNewBut.Text = "Load new";
            LoadNewBut.UseVisualStyleBackColor = true;
            LoadNewBut.Click += LoadNewBut_Click;
            // 
            // LoadExistingBut
            // 
            LoadExistingBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            LoadExistingBut.Location = new Point(110, 161);
            LoadExistingBut.Name = "LoadExistingBut";
            LoadExistingBut.Size = new Size(124, 23);
            LoadExistingBut.TabIndex = 1;
            LoadExistingBut.Text = "Load existing";
            LoadExistingBut.UseVisualStyleBackColor = true;
            LoadExistingBut.Click += LoadExistingBut_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15F);
            label1.Location = new Point(20, 12);
            label1.Name = "label1";
            label1.Size = new Size(200, 28);
            label1.TabIndex = 2;
            label1.Text = "Vacancies data loader";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DetailsTextBox
            // 
            DetailsTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DetailsTextBox.Location = new Point(3, 43);
            DetailsTextBox.Name = "DetailsTextBox";
            DetailsTextBox.ReadOnly = true;
            DetailsTextBox.Size = new Size(231, 112);
            DetailsTextBox.TabIndex = 3;
            DetailsTextBox.Text = "";
            // 
            // SaveBut
            // 
            SaveBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SaveBut.Location = new Point(3, 186);
            SaveBut.Name = "SaveBut";
            SaveBut.Size = new Size(231, 23);
            SaveBut.TabIndex = 4;
            SaveBut.Text = "Save";
            SaveBut.UseVisualStyleBackColor = true;
            SaveBut.Click += SaveBut_Click;
            // 
            // VacanciesLoader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SaveBut);
            Controls.Add(DetailsTextBox);
            Controls.Add(label1);
            Controls.Add(LoadExistingBut);
            Controls.Add(LoadNewBut);
            Name = "VacanciesLoader";
            Size = new Size(237, 212);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button LoadNewBut;
        private Button LoadExistingBut;
        private Label label1;
        private RichTextBox DetailsTextBox;
        private Button SaveBut;
    }
}
