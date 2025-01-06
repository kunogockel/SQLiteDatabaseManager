// 2025-01-06 KG Beschraenkung auf Dateien, die mit "sqlite" enden, aufgehoben
using System;
using System.IO;
using System.Windows.Forms;

namespace SQLite_DataBaseManager
{
    public partial class CreateConnectionForm : Form
    {
        SQLiteManager _sqliteManager = new SQLiteManager();
        public CreateConnectionForm()
        {
            InitializeComponent();
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePathName.Text = openFileDialog.FileName;
            }
        }

        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            try
            {
                string filePathName = txtFilePathName.Text;
                if (File.Exists(filePathName))
                {
                    _sqliteManager.createNewConnection(filePathName);
                    MessageBox.Show("Connection performed successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                showErrorMessage(ex);
            }
        }

        private void showErrorMessage(Exception ex)
        {
            string error = ex.Message;
            if (ex.InnerException != null && ex.InnerException.Message != null)
            {
                error += " InnerException: " + ex.InnerException.Message;
            }
            MessageBox.Show(error);
        }
    }
}
