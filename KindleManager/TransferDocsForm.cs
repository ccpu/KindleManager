using KindleManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KindleManager
{
    public partial class TransferDocsForm : Form
    {
        private Utils.USBDeviceWatcher USBDeviceWatcher;
        List<DriveInfo> _devices;
        private bool _closingSelf = false;

        public TransferDocsForm()
        {
            InitializeComponent();
        }

        private void TransferDocsForm_Load(object sender, EventArgs e)
        {

            if (AppSetting.IsTransferDocsFormOpen)
            {
                _closingSelf = true;
                MessageBox.Show("Only one instance of TransferDocs Form can be opened at a time.");
                Close();
            }
            AppSetting.IsTransferDocsFormOpen = true;
            USBDeviceWatcher = new Utils.USBDeviceWatcher();
            USBDeviceWatcher.OnChange += USBDeviceWatcher_OnChange1;
            LoadList();
            USBDevice.CopyMyClipping();
        }

        private void USBDeviceWatcher_OnChange1(object sender, EventArgs e)
        {
            LoadList();
        }

        private void USBDeviceWatcher_OnChange(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            btnTransfer.Enabled = true;
            lblMsg.Text = "";
            usbListBox.DataSource = null;
            _devices = Utils.USBDevice.All();

            if (_devices.Any(x => x.IsReady))
            {
                Dictionary<string, string> listDataSource = new Dictionary<string, string>();
                foreach (var item in _devices)
                {
                    listDataSource.Add(item.Name, (string.IsNullOrEmpty(item.VolumeLabel) ? "USB Drive" : item.VolumeLabel) + " (" + item.Name + ")");
                }

                usbListBox.DataSource = new BindingSource(listDataSource, null); ;
                usbListBox.ValueMember = "Key";
                usbListBox.DisplayMember = "Value";

                var selectedDevice = _devices.Find(x => x.VolumeLabel == AppSetting.Setting.EbookValumeName);
                if (selectedDevice != null)
                {
                    usbListBox.SelectedValue = selectedDevice.Name;
                }
            }
            else
            {
                lblMsg.Text = "No USB Device Selected!";
                btnTransfer.Enabled = false;
            }

        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            DriveInfo selectedDevice = _devices.Find(x => x.Name == usbListBox.SelectedValue);
            if (selectedDevice != null)
            {
                var deviceDocPath = Path.Combine(selectedDevice.Name, "documents");
                var files = Directory.GetFiles(Utils.AppSetting.NewDocumentFolder);
                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    lblMsg.Text = "Tranfreing " + fileName;
                    var deviceFilePath = Path.Combine(deviceDocPath, fileName);

                    FileAsyncCopy copy = new FileAsyncCopy(file, deviceFilePath);
                    copy.Completed += Copy_Completed; ;
                    copy.StartAsync();
                }
                lblMsg.Text = "";
                MessageBox.Show("Transfer completed");
            }
        }

        private void Copy_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var file = (string)e.Result;
            var fileName = Path.GetFileName(file);
            FileAsyncCopy copy2Old = new FileAsyncCopy(file, Path.Combine(AppSetting.OldDocumentFolder, fileName));
            copy2Old.Completed += Copy2Old_Completed;
            copy2Old.StartAsync();
        }

        private void Copy2Old_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            var file = (string)e.Result;
            File.Delete(file);
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.OpenRead(sourcePath))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }

        private void TransferDocsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (USBDeviceWatcher != null)
                USBDeviceWatcher.Dispose();
        }

        private void TransferDocsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_closingSelf)
                Utils.AppSetting.IsTransferDocsFormOpen = false;
        }
    }
}
