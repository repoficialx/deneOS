namespace dosu.UI;

public class CustomMessageBox : Form
{
    public CustomMessageBox(string message, string title, Icon customIcon)
    {
        this.Text = title;
        this.Size = new Size(400, 200);

        PictureBox iconBox = new PictureBox
        {
            Image = customIcon.ToBitmap(),
            SizeMode = PictureBoxSizeMode.StretchImage,
            Location = new Point(20, 40),
            Size = new Size(50, 50)
        };

        Label messageLabel = new Label
        {
            Text = message,
            Location = new Point(80, 50),
            AutoSize = true
        };

        Button okButton = new Button
        {
            Text = "OK",
            Location = new Point(150, 120),
            DialogResult = DialogResult.OK
        };

        this.Controls.Add(iconBox);
        this.Controls.Add(messageLabel);
        this.Controls.Add(okButton);
        this.AcceptButton = okButton;
    }
    public static DialogResult Show(string message, string title, Icon customIcon)
    {
        using var box = new CustomMessageBox(message, title, customIcon);
        return box.ShowDialog();
    }
}
