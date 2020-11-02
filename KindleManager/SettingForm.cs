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
            tbValumeName.Text = Utils.AppSetting.Setting.EbookValumeName;
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


            string json = JsonConvert.SerializeObject(new Utils.Setting
            {
                DataDirectory = dataDirectory,
                EbookValumeName = tbValumeName.Text,
                StartOnDeviceInsert = cbShowOnConnect.Checked
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

    }
}
