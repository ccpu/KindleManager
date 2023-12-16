using Kindlegen;
using KindleManager.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Windows.Forms;

namespace KindleManager.Utils
{
    public static class HandleRequestData
    {
        public static async Task<StatusCodeResult> ProcessContent(RequestModel requestData)
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
                    return new StatusCodeResult(208);
                }

                File.WriteAllText(filePath, requestData.Html.ToUTF8());

                await WriteToStreamAsync(fileName);
                VisitedSite.Add(requestData.Link);

                return new OkResult();
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
            var temPath = Path.Combine(AppSetting.TempFolder, fileName + ".epub");
            var newPath = Path.Combine(AppSetting.NewDocumentFolder, fileName + ".epub");

            if (!File.Exists(filepath)) throw new Exception("not html file");

            if (!File.Exists(temPath))
            {
                await RunProcessAsync(filepath, temPath);
            }

            if (File.Exists(filepath))
                File.Delete(filepath);

            if (!File.Exists(temPath))
            {
                throw new Exception("Unable to generate mobi file!");
            }

            if (File.Exists(newPath))
            {
                File.Delete(newPath);
            }

            File.Copy(temPath, Path.Combine(AppSetting.NewDocumentFolder, fileName + ".epub"));

            File.Delete(temPath);
        }

        static async Task RunProcessAsync(string inputFile, string outputFile)
        {
            try
            {
                var outputProfile = "kindle";

                var process = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = AppSetting.Setting.CalibreEbookEonvertFile,
                        CreateNoWindow=true,
                        Arguments = string.Format("\"{0}\" \"{1}\" --output-profile={2} --no-default-epub-cover --insert-blank-line --insert-blank-line-size=0.5", inputFile, outputFile, outputProfile)
                }
                };

                process.Start();
                await process.WaitForExitAsync();
            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occurred:" + Environment.NewLine + ex.Message);
            }


        }

    }
}
