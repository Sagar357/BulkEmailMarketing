using BulkEmailMarketing.Models;
using BulkEmailMarketing.utils;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BulkEmailMarketing.Services
{
    public class EmailService
    {
        private static string emailStatus=string.Empty;

        [Obsolete("Do not use this in Production code!!!", false)]
        static void NEVER_EAT_POISON_Disable_CertificateValidation()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                )
                {
                    return true;
                };
        }

        #region SendAsyncCancel

        /// <summary>

        /// this code used to SmtpClient.SendAsyncCancel Method

        /// </summary>

        // static bool mailSent = false;

        void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)

        {

            if (e.Cancelled)

            {

                emailStatus="Send canceled.";

            }

            if (e.Error != null)

            {

                emailStatus=e.Error.ToString();

            }

            else

            {

                emailStatus="Email sent successfully";

            }

        }

        #endregion
        public  string SendEmail(PostEmail_Obj collection, user_Model userData)
        {
            string status = "false";
            try
            {

                //string senderEmail = "markushno357@gmail.com";
                var senderEmail = new MailAddress("markushno357@gmail.com", collection.Name);
                string senderPassword = "marcia@357";
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                    
                client.Credentials = new NetworkCredential(senderEmail.Address, senderPassword);
                client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                MailMessage message = new MailMessage(senderEmail.Address, collection.to, collection.subject, collection.emailBody + "<img alt=\"logo\" src=\"https://emailblasterservices.com/Home/Index\" style =\"float:left;height:90px;margin-left:5px;margin-right:5px;width:100px\" class=\"CToWUd\">");
                message.IsBodyHtml = true;
                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.Headers.Add("Disposition-Notification-To", "sagar@massmancybergeeks.com");
         
                client.Send(message);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                status = ex.Message;
            }

            //try
            //{
            //    /*good code*/

            //    var senderEmail = new MailAddress(userData.user_name, collection.Name);
            //    var receiverEmail = new MailAddress(collection.to, "Receiver");
            //    var password = userData.password;
            //    var sub = collection.subject;


            //    //MailAddress godaddy = new MailAddress("noreply@emailblasterservices.com");
            //    MailAddress godaddy = new MailAddress("noreply@emailtick.com");
            //    MailMessage message = new MailMessage(senderEmail.Address, receiverEmail.Address);
            //    message.Sender = godaddy;
            //    message.Body = collection.emailBody;
            //    message.IsBodyHtml = true;
            //    message.Subject = collection.subject;
            //    /*good code*/

            //    /* var mail = new MailAddress("shub@gmail.com");

            //     //var body = collection["textarea"].ToString();
            //     var body = collection.emailBody;
            //     IPAddress[] ip = Dns.GetHostAddresses("smtp.gmail.com");
            //     var smtp = new SmtpClient
            //     {
            //         Host = ip[0].ToString(),
            //         Port = 587,
            //         EnableSsl = true,
            //         DeliveryMethod = SmtpDeliveryMethod.Network

            //     };
            //     using (var mess = new System.Net.Mail.MailMessage(mail, receiverEmail)
            //     {
            //         IsBodyHtml = true,
            //         BodyEncoding = UTF8Encoding.UTF8,
            //         Subject = collection.subject,
            //         Body = body,
            //         Sender = senderEmail
            //     })
            //     {
            //         EmailService.NEVER_EAT_POISON_Disable_CertificateValidation();
            //         smtp.Send(mess);
            //     }*/


            //    MailAddress sender = new MailAddress("noreply@emailblasterservices.com");


            //    //MailMessage msgs = new MailMessage();
            //    //msgs.To.Add(receiverEmail.Address);
            //    //MailAddress address = new MailAddress(senderEmail.Address);
            //    //msgs.From = address;
            //    //msgs.Subject = collection.subject;
            //    //string htmlBody = collection.emailBody;
            //    //msgs.Body = htmlBody;
            //    //msgs.IsBodyHtml = true;
            //    //SmtpClient client = new SmtpClient();

            //    using (SmtpClient client = new SmtpClient()) 
            //    {
            //        client.Host = "relay-hosting.secureserver.net";
            //        client.Port = 25;
            //        client.UseDefaultCredentials = false;
            //        // client.Credentials = new System.Net.NetworkCredential(msgs.Sender.Address,"Za#&9=1u=a" );
            //        client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Emzfp!xY4x");
            //        client.Send(message);
            //    }
            //    //Send the msgs  
            //    status = true;
            //}
            //catch (Exception ex)
            //{
            //    var w32ex = ex as Win32Exception;
            //    if (w32ex == null)
            //    {
            //        w32ex = ex.InnerException as Win32Exception;
            //    }
            //    if (w32ex != null)
            //    {
            //        int code = w32ex.ErrorCode;
            //        // do stuff
            //    }
            //    status = false;
            //}

            //try
            //{

            //    System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
            //    Sender e-mail address.
            //   Msg.From = "markushno357@gmail.com";
            //    Recipient e-mail address.
            //   Msg.To = "sagar@massmancybergeeks.com";
            //    Msg.Subject = "Enquiry";
            //    Msg.Body = "Hi";
            //    IPAddress[] ip = Dns.GetHostAddresses("smtp.gmail.com");
            //    your remote SMTP server IP.
            //   SmtpMail.SmtpServer = ip[0].ToString();//your ip address
            //    SmtpMail.Send(Msg);

            //    Msg = null;


            //}
            //catch (Exception ex)
            //{
            //}

            return status;
        }

        public string SaveTemplate(Campaign_Model Model)
        {
            string status;
            try
            {
              

               

                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[8];
                   
                    param[1] = new SqlParameter("@emailBody", Model.emailBody);
                   
                    param[0] = new SqlParameter("@campaignid", Model.campaighId);

                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "updateTemplate", param);
                    status = "success";
                }

            }
            catch (Exception ex)
            {
                status = "failed";
            }
            return status;
        }

        


    }
}