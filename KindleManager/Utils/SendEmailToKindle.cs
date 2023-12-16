using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Windows.Forms;
using System.Threading;

namespace KindleManager.Utils
{
    internal class SendEmailToKindle
    {
        public static bool Send(string file)
        {
            var emailFrom = AppSetting.Setting.EmailFrom;
            var emailFromPass = AppSetting.Setting.EmailFromPass;
            var emailTo = AppSetting.Setting.KindleEmailTo;

            if (emailFrom == null || emailFromPass == null || emailTo == null)
                throw new Exception("you must set environment variables 'emailFrom','emailFromPass','emailTo', set this variables in azure function application settings or for debugging in local.settings.json. ");

            var fromAddress = new MailAddress(emailFrom);
            var toAddress = new MailAddress(emailTo);
            const string subject = "From Chrome ToKindle APP";
            const string body = "From Chrome ToKindle APP";
            var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, emailFromPass)
            };
            // Create  the file attachment for this e-mail message.
            var data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            var disposition = data.ContentDisposition;
            disposition.CreationDate = File.GetCreationTime(file);
            disposition.ModificationDate = File.GetLastWriteTime(file);
            disposition.ReadDate = File.GetLastAccessTime(file);

            var sent = false;

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                Attachments = { data },
                // Set the delivery notification option
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
            })
            {
                try
                {
                    // Send the email
                    smtp.Send(message);
                    sent = true;
                }
                catch (SmtpFailedRecipientException ex)
                {
                    MessageBox.Show("Failed to deliver email to " + ex.FailedRecipient + ". Status code: "
                        + ex.StatusCode + Environment.NewLine + " Message:" + ex.Message + "");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred:" + Environment.NewLine + ex.Message);
                }
            }

            if (sent)
            {
                var fileName = Path.GetFileName(file);
                var oldFilePath = Path.Combine(AppSetting.OldDocumentFolder, fileName);
                if (!File.Exists(oldFilePath))
                {
                    File.Copy(file, oldFilePath);
                }
                Thread.Sleep(2000);
                File.Delete(file);
            }

            return sent;
        }
    }
}
