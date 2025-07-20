namespace LLM_test
{
    public static class Helpers
    {
        public static void ShowDialogueWindowWithOKAndCustomButton(string windowTitle, string windowMsg,
            string customButName, Action onCustomButClicked)
        {
            var dlg = new Form()
            {
                Text = windowMsg,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(300, 120)
            };

            var lbl = new Label()
            {
                AutoSize = true,
                Text = windowTitle,
                Location = new Point(20, 20)
            };
            dlg.Controls.Add(lbl);

            var btnOk = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Size = new Size(80, 30),
                Location = new Point(dlg.ClientSize.Width - 100, dlg.ClientSize.Height - 50)
            };
            dlg.Controls.Add(btnOk);

            var customBut = new Button()
            {
                Text = customButName,
                Size = new Size(100, 30),
                Location = new Point(btnOk.Left - 110, btnOk.Top)
            };
            customBut.Click += (s, e) =>
            {
                dlg.DialogResult = DialogResult.OK;
                dlg.Close();
                onCustomButClicked?.Invoke();
            };
            dlg.Controls.Add(customBut);
            dlg.AcceptButton = btnOk;
            dlg.ShowDialog();
        }
    }
}
