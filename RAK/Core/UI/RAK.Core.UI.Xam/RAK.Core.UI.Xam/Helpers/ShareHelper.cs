using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RAK.Core.UI.Xam.Helpers
{
    public static class ShareHelper
    {

        /// <summary>
        /// Comparte un archivo
        /// </summary>
        public static async Task ShareFile(ShareFile file, string title)
        {
            await Share.RequestAsync(new ShareFileRequest
            {
                File = file,
                Title = title
            });
        }

        /// <summary>
        /// Compartimos un texto a cualquier APP
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static async Task ShareText(string title, string text)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = title
            });
        }

        /// <summary>
        /// Compartimos una URI a cualquier APP
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task ShareUri(string title, string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = title,
            });
        }

        /// <summary>
        /// Enviamos texto por Mail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public static async Task ShareByMail(string subject, string body, List<string> recipients, List<EmailAttachment> emailAttachments = null)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                if (emailAttachments != null)
                {
                    message.Attachments = emailAttachments;
                }
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                // Email is not supported on this device
            }
            catch (Exception ex)
            {
                // Some other exception occurred
            }
        }

        /// <summary>
        /// Enviamos texto por sms
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="telefono"></param>
        /// <returns></returns>
        public static async Task ShareBySMS(string texto, string telefono)
        {
            try
            {
                var sms = new SmsMessage(texto, telefono);
                await Sms.ComposeAsync(sms);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
