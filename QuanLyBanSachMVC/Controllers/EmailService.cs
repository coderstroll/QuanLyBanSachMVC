using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace QuanLyBanSachMVC.Controllers
{
    public class EmailService
    {


        /// <summary>
        /// Sand email
        /// </summary>
        /// <param name="smtpUsername">smtp Username</param>
        /// <param name="smtpPassword">smtp Password</param>
        /// <param name="smtpHost">smtp Host</param>
        /// <param name="smtpPort">smtp Port</param>
        /// <param name="toEmail">to Email</param>
        /// <param name="subject">subject</param>
        /// <param name="content">content</param>
        /// <returns></returns>
        public bool sendEmail(string smtpUsername, string smtpPassword, string smtpHost, int smtpPort, string toEmail, string subject, string content)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Host = smtpHost;
                    smtpClient.Port = smtpPort;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    var msg = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(smtpUsername),
                        Subject = subject,
                        Body = content,
                        Priority = MailPriority.Normal,

                    };

                    msg.To.Add(toEmail);
                    smtpClient.Send(msg);
                    return true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Debug Error SendMail: " + ex);
                return false;
            }
        }


    }
}