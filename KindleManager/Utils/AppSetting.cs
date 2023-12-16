using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace KindleManager.Utils
{
    public class Setting
    {
        public Setting()
        {
            EbookValumeName = "Kindle";
            StartOnDeviceInsert = true;
            AutoSendEmail = true;
        }
        public string DataDirectory { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromPass { get; set; }
        public string KindleEmailTo { get; set; }

        public string CalibreEbookEonvertFile { get; set; }

        public bool AutoSendEmail { get; set; }

        public string EbookValumeName { get; set; }
        public bool StartOnDeviceInsert { get; set; }
    }
    public static class AppSetting
    {
        public static string SettingFileName = "setting.json";
        public static readonly string StartupRegistryKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public static readonly string AppName = "KindleManager";


        public static bool IsTransferDocsFormOpen = false;

        public static string SettingFilePath
        {
            get
            {
                FileInfo fileInfo = new FileInfo(Application.ExecutablePath.ToString());
                return Path.Combine(fileInfo.DirectoryName, SettingFileName);
            }
        }

        public static string TempFolder
        {
            get
            {
                return Path.Combine(Setting.DataDirectory, "temp");
            }
        }

        public static string VisitedSitesFileName
        {
            get
            {
                return Path.Combine(Setting.DataDirectory, "visited-sites.json");
            }
        }

        public static string NewDocumentFolder
        {
            get
            {
                return Path.Combine(Setting.DataDirectory, "docs");
            }
        }

        public static string MyClippingFileName
        {
            get
            {
                return "My Clippings.txt";
            }
        }

        public static string OldDocumentFolder
        {
            get
            {
                return Path.Combine(Setting.DataDirectory, "old-docs");
            }
        }

        public static string DataDirectory
        {
            get
            {
                return Setting.DataDirectory;
            }
        }

        public static Setting Setting { get; private set; } = new Setting();

        public static string EbookDocumentFolderName
        {
            get
            {
                return "documents";
            }
        }

        public static void LoadSetting()
        {
            if (!File.Exists(SettingFilePath))
            {
                return;
            }

            Setting = JsonConvert.DeserializeObject<Setting>(File.ReadAllText(SettingFilePath));

            if (string.IsNullOrEmpty(Setting.DataDirectory))
            {
                return;
            }

            if (!Directory.Exists(OldDocumentFolder))
            {
                Directory.CreateDirectory(OldDocumentFolder);
            }

            if (!Directory.Exists(TempFolder))
            {
                Directory.CreateDirectory(TempFolder);
            }

            if (!Directory.Exists(NewDocumentFolder))
            {
                Directory.CreateDirectory(NewDocumentFolder);
            }
        }


        public static void SetSetting(Setting setting)
        {
            Setting = setting;
        }

    }
}
