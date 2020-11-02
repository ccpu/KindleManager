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

            menu.MenuItems.Add("Transfer Docs", OpenTransferDocs);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("My Clippings", OpenMyClipping);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Open Data Directory", OpenExplorer);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Setting", onSettingClick);
            this.SystemTrayIcon.ContextMenu = menu;

            menu.MenuItems.Add("Exit", ContextMenuExit);
            this.SystemTrayIcon.ContextMenu = menu;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;

            if (string.IsNullOrEmpty(Utils.AppSetting.Setting.DataDirectory))
            {
                var form = new SettingForm();
                form.Show();
                MessageBox.Show("Please select data directory.");
            }

            USBDeviceWatcher = new USBDeviceWatcher();
            USBDeviceWatcher.OnChange += USBDeviceWatcher_OnChange1;
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
