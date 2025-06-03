namespace dosu
{
    public class Program
    {
        private enum msgIcons {
            Error = 0x01, // Error
            Question = 0x02, // Question
            Warning = 0x03, // Warning
            Information = 0x04, // Information
            Stop = 0x05, // Stop
            Exclamation = 0x06, // Exclamation
            Hand = 0x07, // Hand
            Asterisk = 0x08, // Asterisk
            Application = 0x09, // Application
            User = 0x0A, // User
            Custom = 0x0B, // Custom
            None = 0x00, // No icon
            Success = 0x0C // Success
        }

        public static int SysMsg(string msg, IntPtr level = 0x01, IntPtr Icon = 0xA0, string title = "", string pathToCustomIcon = "")
        {
            MessageBoxIcon icon = MessageBoxIcon.None;
            switch (Icon)
            {
                case 0x01:
                    icon = MessageBoxIcon.Error;
                    break;
                case 0x02:
                    icon = MessageBoxIcon.Question;
                    break;
                case 0x03:
                    icon = MessageBoxIcon.Warning;
                    break;
                case 0x04:
                    icon = MessageBoxIcon.Information;
                    break;
                case 0x05:
                    icon = MessageBoxIcon.Stop;
                    break;
                case 0x06:
                    icon = MessageBoxIcon.Exclamation;
                    break;
                case 0x07:
                    icon = MessageBoxIcon.Hand;
                    break;
                case 0x08:
                    icon = MessageBoxIcon.Asterisk;
                    break;
                case 0x09:
                    switch (level)
                    {
                        case 0x01:
                            icon = MessageBoxIcon.Information;
                            break;
                        case 0x02:
                            icon = MessageBoxIcon.Warning;
                            break;
                        case 0x03:
                            icon = MessageBoxIcon.Error;
                            break;
                        default:
                            icon = MessageBoxIcon.None;
                            break;
                    }
                    break;
                case 0x0A:
                    //icon = MessageBoxIcon.User;
                    break;
                case 0x0B:
                    System.Drawing.Icon icon_ = new Icon(pathToCustomIcon);
                    var _ = CustomMessageBox.Show(msg, title, icon_);
                    return (int)_;
                case 0x0C:
                    icon = MessageBoxIcon.None; // Success does not have a specific icon in MessageBox
                    break;
                default:
                    icon = MessageBoxIcon.None;
                    break;
            }
            var __ = MessageBox.Show(msg, title, MessageBoxButtons.OK, icon);
            return (int)__;
        }
    }

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
            using (var box = new CustomMessageBox(message, title, customIcon))
            {
                return box.ShowDialog();
            }
        }
    }
}
