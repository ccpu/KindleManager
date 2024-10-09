using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Thread = System.Threading.Thread;

namespace KindleManager.Utils
{
    internal class SendEmailToKindle
    {
        static string[] Scopes = { GmailService.Scope.GmailSend };
        static string ApplicationName = "Gmail API .NET Quickstart";



        public static bool Send(string filePath)
        {
            var emailFrom = AppSetting.Setting.EmailFrom;
            var emailTo = AppSetting.Setting.KindleEmailTo;
            var sent = false;

            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Create the email content
            var email = new AE.Net.Mail.MailMessage
            {
                Subject = "From Chrome To Kindle APP",
                Body = "From Chrome To Kindle APP",
                From = new MailAddress(emailFrom)
            };
            email.To.Add(new MailAddress(emailTo));

            var fileName = Path.GetFileName(filePath);

            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Determine MIME type based on file extension
            string mimeType = "application/octet-stream"; // Default MIME type
            if (fileName.EndsWith(".epub"))
            {
                mimeType = "application/epub+zip";
            }
            else if (fileName.EndsWith(".pdf"))
            {
                mimeType = "application/pdf";
            }
            Console.WriteLine("MIME type: " + mimeType);

            // Create and add the attachment
            var attachment = new AE.Net.Mail.Attachment(fileBytes, fileName, mimeType);
            email.Attachments.Add(attachment);

            attachment.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");


            // Convert email to base64 format
            using (var memoryStream = new MemoryStream())
            {
                email.Save(memoryStream);
                var message = new Message
                {
                    Raw = Convert.ToBase64String(memoryStream.ToArray())
                        .Replace('+', '-').Replace('/', '_').Replace("=", "")
                };

                try
                {
                    // Send email
                    service.Users.Messages.Send(message, "me").Execute();
                    sent = true; // Set sent to true if the email is sent successfully
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }

            if (sent)
            {
                var oldFilePath = Path.Combine(AppSetting.OldDocumentFolder, fileName);
                if (!File.Exists(oldFilePath))
                {
                    File.Copy(filePath, oldFilePath);
                }
                Thread.Sleep(2000);
                File.Delete(filePath);
            }

            return sent;
        }
    }
}
