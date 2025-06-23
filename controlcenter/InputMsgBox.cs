using System;
using System.Windows.Forms;

public static class InputMessageBox
{
    public static string Show(string prompt, string title = "Input")
    {
        Form form = new Form()
        {
            Width = 400,
            Height = 150,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MinimizeBox = false,
            MaximizeBox = false
        };

        Label label = new Label() { Left = 10, Top = 10, Text = prompt, AutoSize = true };
        TextBox textBox = new TextBox() { Left = 10, Top = 35, Width = 360 };
        Button okButton = new Button() { Text = "OK", Left = 210, Width = 75, Top = 70, DialogResult = DialogResult.OK };
        Button cancelButton = new Button() { Text = "Cancel", Left = 295, Width = 75, Top = 70, DialogResult = DialogResult.Cancel };

        okButton.Click += (sender, e) => form.Close();
        cancelButton.Click += (sender, e) => form.Close();

        form.Controls.Add(label);
        form.Controls.Add(textBox);
        form.Controls.Add(okButton);
        form.Controls.Add(cancelButton);

        form.AcceptButton = okButton;
        form.CancelButton = cancelButton;

        var result = form.ShowDialog();

        if (result == DialogResult.OK)
            return textBox.Text;
        else
            return null;
    }
}