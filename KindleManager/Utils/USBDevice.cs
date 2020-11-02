using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KindleManager.Utils
{
    public static class USBDevice
    {

        public static List<DriveInfo> All()
        {
            List<DriveInfo> devices = new List<DriveInfo>();
            foreach (DriveInfo driveInfo in DriveInfo.GetDrives())
            {
                if (driveInfo.DriveType == DriveType.Removable)
                {
                    devices.Add(driveInfo);
                }
            }
            return devices;
        }

        internal static DriveInfo GetEbookDeviceInfo()
        {
            var devices = All();
            return devices.Find(x => x.VolumeLabel == AppSetting.Setting.EbookValumeName);
        }


        internal static string GetEbookDocumentPath()
        {
            var deviceInfo = GetEbookDeviceInfo();
            return Path.Combine(deviceInfo.Name + AppSetting.EbookDocumentFolderName);
        }


        internal static string GetMyClippingPath()
        {
            var docPath = GetEbookDocumentPath();
            return Path.Combine(docPath, AppSetting.MyClippingFileName);
        }

        internal static bool IsEbookConnected()
        {
            var devices = All();

            if (devices.Find(x => x.VolumeLabel == AppSetting.Setting.EbookValumeName) != null)
            {
                return true;
            }
            return false;
        }

        internal static void CopyMyClipping()
        {
            if (IsEbookConnected())
            {
                var filePath = USBDevice.GetMyClippingPath();
                if (File.Exists(filePath))
                    File.Copy(filePath, Path.Combine(AppSetting.DataDirectory, AppSetting.MyClippingFileName), true);
            }

        }

    }

    public class USBDeviceWatcher : Form
    {
        Size mDeferredSize;

        protected override void OnHandleCreated(EventArgs e)
        {
            mDeferredSize = Size;
            Size = new Size(0, 0);
            Size = mDeferredSize;
            base.OnHandleCreated(e);
        }
        public USBDeviceWatcher()
        {
            Show();
            Visible = false;
            Opacity = 0;
            ShowInTaskbar = false;
        }

        public event EventHandler OnChange;

        private const int DBT_DEVICEARRIVAL = 0x8000;
        private const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
        private const int DBT_DEVTYP_PORT = 0x00000002;

        protected override void WndProc(ref Message m)
        {
            int devType;
            base.WndProc(ref m);

            switch (m.WParam.ToInt32())
            {
                case DBT_DEVICEARRIVAL:

                devType = Marshal.ReadInt32(m.LParam, 4);

                if (devType == DBT_DEVTYP_PORT)
                {
                    // usb device inserted,
                    OnChange(this, null);
                }

                break;

                case DBT_DEVICEREMOVECOMPLETE:

                devType = Marshal.ReadInt32(m.LParam, 4);
                if (devType == DBT_DEVTYP_PORT)
                {
                    // usb device removed,
                    OnChange(this, null);
                }
                break;
            }
        }
    }
}

