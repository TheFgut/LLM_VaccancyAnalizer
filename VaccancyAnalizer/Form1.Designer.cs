namespace VaccancyAnalizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            answerMessageBox = new RichTextBox();
            SendMessageBut = new Button();
            messageTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // answerMessageBox
            // 
            answerMessageBox.Location = new Point(12, 159);
            answerMessageBox.Name = "answerMessageBox";
            answerMessageBox.Size = new Size(343, 279);
            answerMessageBox.TabIndex = 0;
            answerMessageBox.Text = "";
            // 
            // SendMessageBut
            // 
            SendMessageBut.Location = new Point(12, 130);
            SendMessageBut.Name = "SendMessageBut";
            SendMessageBut.Size = new Size(346, 23);
            SendMessageBut.TabIndex = 1;
            SendMessageBut.Text = "Send message";
            SendMessageBut.UseVisualStyleBackColor = true;
            SendMessageBut.Click += SendMessageBut_Click;
            // 
            // messageTextBox
            // 
            messageTextBox.Location = new Point(12, 12);
            messageTextBox.Name = "messageTextBox";
            messageTextBox.Size = new Size(346, 112);
            messageTextBox.TabIndex = 2;
            messageTextBox.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(370, 450);
            Controls.Add(messageTextBox);
            Controls.Add(SendMessageBut);
            Controls.Add(answerMessageBox);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox answerMessageBox;
        private Button SendMessageBut;
        private RichTextBox messageTextBox;
    }
}
