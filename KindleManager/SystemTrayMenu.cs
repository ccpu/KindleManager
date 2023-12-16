using KindleManager.Utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace KindleManager
{
    public partial class SystemTrayMenu : Form
    {
        public USBDeviceWatcher USBDeviceWatcher { get; }

        public SystemTrayMenu()
        {
            InitializeComponent();
            this.CenterToScreen();

            this.Icon = Properties.Resources.ebook;
            this.SystemTrayIcon.Icon = Properties.Resources.ebook;

            this.SystemTrayIcon.Text = "Kindle Manager";
            this.SystemTrayIcon.Visible = true;
            this.SystemTrayIcon.Click += SystemTrayIcon_Click;

            ContextMenu menu = new ContextMenu();

            menu.MenuItems.Add("Transfer documents with email", transferWithEmail);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Transfer documents with USB cable", OpenTransferDocs);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Clear old Documents", clareOldDocs);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("My clippings", OpenMyClipping);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Open data directory", OpenExplorer);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Setting", onSettingClick);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Exit", ContextMenuExit);
            this.SystemTrayIcon.ContextMenu = menu;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;

            if (string.IsNullOrEmpty(Utils.AppSetting.Setting.DataDirectory) 
                || string.IsNullOrEmpty(Utils.AppSetting.Setting.CalibreEbookEonvertFile) 
                || !File.Exists(AppSetting.Setting.CalibreEbookEonvertFile))
            {
                var form = new SettingForm();
                form.Show();
                MessageBox.Show("Please provide required settings.");
            }



            USBDeviceWatcher = new USBDeviceWatcher();
            USBDeviceWatcher.OnChange += USBDeviceWatcher_OnChange1;
        }

        private void transferWithEmail(object sender, EventArgs e)
        {

            var files = Directory.GetFiles(Utils.AppSetting.NewDocumentFolder);
            foreach (var file in files)
            {
                SendEmailToKindle.Send(file);
            }

            MessageBox.Show("Transfer Completed");
        }

        private void clareOldDocs(object sender, EventArgs e)
        {
            if (!USBDevice.IsEbookConnected())
            {
                MessageBox.Show("Kindle not connected");
                return;
            }
            var files = Directory.GetFiles(AppSetting.OldDocumentFolder);
            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var kindleFilePath = Path.Combine(USBDevice.GetEbookDocumentPath(), fileName);
                if (!File.Exists(kindleFilePath))
                {
                    File.Delete(file);
                }
            }
            MessageBox.Show("Old docs cleared");
        }

        private void OpenMyClipping(object sender, EventArgs e)
        {
            var form = new MyClippingForm();
            form.Show();
        }

        private void SystemTrayIcon_Click(object sender, EventArgs e)
        {
            if (!AppSetting.IsTransferDocsFormOpen && (e as MouseEventArgs).Button == MouseButtons.Left)
            {
                var form = new MyClippingForm();
                form.Show();
            }
        }

        private void USBDeviceWatcher_OnChange1(object sender, EventArgs e)
        {
            if (USBDevice.IsEbookConnected())
            {
                USBDevice.CopyMyClipping();
                if (!AppSetting.IsTransferDocsFormOpen && AppSetting.Setting.StartOnDeviceInsert)
                {
                    var form = new TransferDocsForm();
                    form.Show();
                }
            }
        }

        private void SystemTrayIconDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.WindowState = FormWindowState.Normal;
        }

        private void OpenTransferDocs(object sender, EventArgs e)
        {
            var form = new TransferDocsForm();
            form.Show();
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.SystemTrayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void OpenExplorer(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Utils.AppSetting.Setting.DataDirectory);
        }


        private void onSettingClick(object sender, EventArgs e)
        {
            var form = new SettingForm();
            form.Show();
        }

        private void WindowResize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
