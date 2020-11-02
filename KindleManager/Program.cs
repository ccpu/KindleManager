using System;
using System.Linq;
using System.Windows.Forms;

namespace KindleManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WebService.StartWebServer();

            Utils.AppSetting.LoadSetting();

            using (new SystemTrayMenu())
            {
                Application.Run();
            }

        }
    }
}
