using System.IO;
using System.Threading;

namespace KindleManager.Utils
{
    public class NewDocWatcher
    {

        public NewDocWatcher()
        {
            if (AppSetting.Setting.AutoSendEmail)
                WatchDocsfolder();
        }
        private void WatchDocsfolder()
        {
            if (!Directory.Exists(AppSetting.NewDocumentFolder))
            {
                Thread.Sleep(5000);
                WatchDocsfolder();
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher
            {
                Path = AppSetting.NewDocumentFolder,
                EnableRaisingEvents = true
            };

            watcher.Created += new FileSystemEventHandler(OnFileCreated);
        }

        // Define the event handler for the Created event
        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(4000);
            SendEmailToKindle.Send(e.FullPath);
        }

    }
}
