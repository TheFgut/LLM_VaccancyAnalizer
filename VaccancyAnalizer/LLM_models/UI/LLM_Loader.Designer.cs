namespace VaccancyAnalizer.LLM_models.UI
{
    partial class LLM_Loader
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
            SetupBut = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // SetupBut
            // 
            SetupBut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SetupBut.Location = new Point(3, 36);
            SetupBut.Name = "SetupBut";
            SetupBut.Size = new Size(144, 23);
            SetupBut.TabIndex = 0;
            SetupBut.Text = "Setup";
            SetupBut.UseVisualStyleBackColor = true;
            SetupBut.Click += SetupBut_Click;
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(3, 7);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(144, 23);
            textBox1.TabIndex = 1;
            // 
            // LLM_Loader
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox1);
            Controls.Add(SetupBut);
            Name = "LLM_Loader";
            Size = new Size(150, 62);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SetupBut;
        private TextBox textBox1;
    }
}
