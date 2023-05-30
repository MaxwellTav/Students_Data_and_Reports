using System;
using System.Windows.Forms;

namespace Students_Data_and_Reports
{
    public partial class SeachForm : Form
    {
        public string OutputQuery;

        string OutputStringValue = "";
        int OutputColunmValue = 0;

        public SeachForm()
        { InitializeComponent(); }

        void CerrarForm()
        {
            Close();
            Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            OutputQuery = SearchValueTextBox.Text;

            CerrarForm();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            CerrarForm();
        }
    }
}
