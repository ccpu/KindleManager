using Kindlegen;
using KindleManager.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace KindleManager.Utils
{
    public static class HandleRequestData
    {
        public static async Task<int> ProcessContent(RequestModel requestData)
        {
            var fileName = Path.GetInvalidFileNameChars().Aggregate(requestData.Title, (current, c) => current.Replace(c.ToString(), "-"));
            fileName = fileName.Truncate(200);
            string filePath = Path.Combine(AppSetting.TempFolder, fileName + ".html");

            try
            {
                var writer = new FileStream(filePath, FileMode.Create);
                writer.Dispose();

                if (!requestData.ReSend && VisitedSite.IsSiteVisited(requestData.Link))
                {
                    return 208;
                }

                File.WriteAllText(filePath, requestData.Html.ToUTF8());

                await WriteToStreamAsync(fileName);
                VisitedSite.Add(requestData.Link);

                return (int)HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                File.Delete(filePath);
                throw e;
            }
        }

        private static async Task WriteToStreamAsync(string fileName)
        {

            var filepath = Path.Combine(AppSetting.TempFolder, fileName + ".html");
            var tempMobipath = Path.Combine(AppSetting.TempFolder, fileName + ".mobi");
            var newMobipath = Path.Combine(AppSetting.NewDocumentFolder, fileName + ".mobi");

            if (!File.Exists(filepath)) throw new Exception("not html file");

            if (!File.Exists(tempMobipath))
            {
                await RunProcessAsync(filepath);
            }

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

        static async Task<KindleConvertResult> RunProcessAsync(string filepath)
        {
            //var tcs = new TaskCompletionSource<int>();

            var process = new Process
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

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            await process.WaitForExitAsync();
            return KindleOutputParser.ParseOutput(output);
        }

    }
}
