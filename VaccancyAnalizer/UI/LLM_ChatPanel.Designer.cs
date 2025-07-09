namespace VaccancyAnalizer.JobAnalyticsMaking
{
    partial class LLM_ChatPanel
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
            SendMessageBut = new Button();
            inputTextPanel = new RichTextBox();
            outputTextPanel = new RichTextBox();
            SuspendLayout();
            // 
            // SendMessageBut
            // 
            SendMessageBut.Location = new Point(3, 173);
            SendMessageBut.Name = "SendMessageBut";
            SendMessageBut.Size = new Size(289, 23);
            SendMessageBut.TabIndex = 0;
            SendMessageBut.Text = "Send message";
            SendMessageBut.UseVisualStyleBackColor = true;
            SendMessageBut.Click += SendMessageBut_Click;
            // 
            // inputTextPanel
            // 
            inputTextPanel.Location = new Point(3, 3);
            inputTextPanel.Name = "inputTextPanel";
            inputTextPanel.Size = new Size(289, 164);
            inputTextPanel.TabIndex = 1;
            inputTextPanel.Text = "";
            // 
            // outputTextPanel
            // 
            outputTextPanel.Location = new Point(3, 202);
            outputTextPanel.Name = "outputTextPanel";
            outputTextPanel.ReadOnly = true;
            outputTextPanel.Size = new Size(289, 169);
            outputTextPanel.TabIndex = 2;
            outputTextPanel.Text = "";
            // 
            // LLM_ChatPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(outputTextPanel);
            Controls.Add(inputTextPanel);
            Controls.Add(SendMessageBut);
            Name = "LLM_ChatPanel";
            Size = new Size(300, 374);
            ResumeLayout(false);
        }

        #endregion

        private Button SendMessageBut;
        private RichTextBox inputTextPanel;
        private RichTextBox outputTextPanel;
    }
}
