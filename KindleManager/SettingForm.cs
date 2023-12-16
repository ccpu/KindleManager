using System;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace KindleManager
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }


        RegistryKey key = Registry.CurrentUser.OpenSubKey(Utils.AppSetting.StartupRegistryKey, true);

        private void SettingForm_Load(object sender, EventArgs e)
        {
            tbDataDirectory.Text = Utils.AppSetting.Setting.DataDirectory;
            var regValue = key.GetValue(Utils.AppSetting.AppName);

            cbStartOnBoot.Checked = regValue != null;
            cbShowOnConnect.Checked = Utils.AppSetting.Setting.StartOnDeviceInsert;
            chkAutoSendEmail.Checked= Utils.AppSetting.Setting.AutoSendEmail;
            tbValumeName.Text = Utils.AppSetting.Setting.EbookValumeName;
            tbEmailFrom.Text = Utils.AppSetting.Setting.EmailFrom;
            tbEmailFromPass.Text = Utils.AppSetting.Setting.EmailFromPass;
            tbKindleEmail.Text = Utils.AppSetting.Setting.KindleEmailTo;
            tbCalibreFilePath.Text = Utils.AppSetting.Setting.CalibreEbookEonvertFile;
        }

        private void tbDataDirectory_Click(object sender, EventArgs e)
        {
            DialogResult result = fbdDataDirectory.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbDataDirectory.Text = fbdDataDirectory.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dataDirectory = tbDataDirectory.Text;

            if (string.IsNullOrEmpty(dataDirectory) )
            {
                MessageBox.Show("Please provide data directory path.");
                return;
            }

            if (string.IsNullOrEmpty(tbCalibreFilePath.Text))
            {
                MessageBox.Show("Please provide calibre ebook-convert.exe path.");
                return;
            }

            string json = JsonConvert.SerializeObject(new Utils.Setting
            {
                DataDirectory = dataDirectory,
                EbookValumeName = tbValumeName.Text,
                StartOnDeviceInsert = cbShowOnConnect.Checked,
                AutoSendEmail= chkAutoSendEmail.Checked,
                EmailFrom = tbEmailFrom.Text,
                EmailFromPass = tbEmailFromPass.Text,
                KindleEmailTo = tbKindleEmail.Text,
                CalibreEbookEonvertFile=tbCalibreFilePath.Text
            });

            File.WriteAllText(Utils.AppSetting.SettingFilePath, json);
            System.Threading.Thread.Sleep(500);
            Utils.AppSetting.LoadSetting();
            Close();
        }

        private void cbStartOnBoot_CheckedChanged(object sender, EventArgs e)
        {

            if (cbStartOnBoot.Checked)
            {
                key.SetValue(Utils.AppSetting.AppName, Application.ExecutablePath.ToString());
            }
            else
            {
                key.DeleteValue(Utils.AppSetting.AppName);
            }

        }

        private void tbCalibreFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select an Executable File";
            openFileDialog.Filter = "Executable Files|*.exe";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                tbCalibreFilePath.Text = selectedFilePath;
            }

        }
    }
}
