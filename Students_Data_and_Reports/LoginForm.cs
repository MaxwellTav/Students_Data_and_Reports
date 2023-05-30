using Students_Data_and_Reports.Properties;
using System;
using System.Windows.Forms;

namespace Students_Data_and_Reports
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        { InitializeComponent(); }

        int tryAttemps = 3;
        bool canGetIn = false;
        public bool firstTime = false;

        public DialogResult dialogResult = DialogResult.None;

        private void LoginForm_Load(object sender, EventArgs e)
        {
            canGetIn = false;
            Text = "Intentos restantes " + tryAttemps;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text != Settings.Default.password)
            {
                tryAttemps--;
                MessageBox.Show("¡Contraseña incorrecta!\nRespeta las mayúsculas, minúsculas, signos, números e intenta de nuevo.", "Forbiden 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Text = "Intentos restantes " + tryAttemps;
            }
            else
            {
                if (firstTime)
                {
                    Settings.Default.password = PasswordTextBox.Text;
                    Settings.Default.Save();
                }

                CloseForm(true);
            }

            if (tryAttemps < 1)
            {
                CloseForm(false);
            }

        }

        void CloseForm(bool getLogIn)
        {
            switch (getLogIn)
            {
                case true:
                    dialogResult = DialogResult.OK;
                    break;

                default:
                    dialogResult = DialogResult.Abort;
                    break;
            }

            Close();
            Dispose();
        }
    }
}
