using Students_Data_and_Reports.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Students_Data_and_Reports
{
    public partial class ConnectionStringForm : Form
    {
        public ConnectionStringForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.connectionString = CNTextBox.Text;
            Settings.Default.Save();

            MessageBox.Show("¡Todo bien!\nSe guardó la cadenad de conexión", "Success 200", MessageBoxButtons.OK, MessageBoxIcon.Information);

            button2_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void ConnectionStringForm_Load(object sender, EventArgs e)
        {
            CNTextBox.Text = Settings.Default.connectionString;
        }
    }
}
