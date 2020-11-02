using KindleManager.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KindleManager.Utils
{
    public static class HandleRequestData
    {
        public static int ProcessContent(RequestModel requestData)
        {
            var fileName = Path.GetInvalidFileNameChars().Aggregate(requestData.Title, (current, c) => current.Replace(c.ToString(), "-"));
            string filePath = Path.Combine(AppSetting.TempFolder, fileName + ".html");

            try
            {
                var writer = new FileStream(filePath, FileMode.Create);
                writer.Dispose();

                if (!requestData.ReSend && VisitedSite.IsSiteVisited(requestData.Link))
                {
                    return 208;
                }

                File.WriteAllText(filePath, requestData.Html);

                WriteToStreamAsync(fileName);
                VisitedSite.Add(requestData.Link);

                return (int)HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                File.Delete(filePath);
                throw e;
            }
        }

        private static void WriteToStreamAsync(string fileName)
        {
            var tcs = new TaskCompletionSource<object>();

            var filepath = Path.Combine(AppSetting.TempFolder, fileName + ".html");
            var tempMobipath = Path.Combine(AppSetting.TempFolder, fileName + ".mobi");
            var newMobipath = Path.Combine(AppSetting.NewDocumentFolder, fileName + ".mobi");

            var lines = new StringBuilder();

            if (!File.Exists(filepath)) throw new Exception("not html file");

            if (!File.Exists(tempMobipath))
            {
                var kindleGen = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = AppSetting.KindlegenFileName,
                        Arguments = string.Format(@"""{0}""", filepath),
                        CreateNoWindow=true,
                    }
                };
                kindleGen.Start();
                while (!kindleGen.StandardOutput.EndOfStream)
                {
                    string line = kindleGen.StandardOutput.ReadLine();
                    if (string.IsNullOrEmpty(line) || line.Contains("Hyperlink not resolved:"))
                        continue;
                    lines.AppendLine(line);
                }
                kindleGen.WaitForExit();
            }

            tcs.SetResult(null);

            if (File.Exists(filepath))
                File.Delete(filepath);

            if (!File.Exists(tempMobipath))
            {
                throw new Exception("Unable to generate mobi file!");
            }

            if (File.Exists(newMobipath))
            {
                File.Delete(newMobipath);
            }

            File.Copy(tempMobipath, Path.Combine(AppSetting.NewDocumentFolder, fileName + ".mobi"));

            File.Delete(tempMobipath);
        }

    }
}
