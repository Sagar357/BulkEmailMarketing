using BulkEmailMarketing.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                ) {
                    return true;
                };
        }
        public  bool SendEmail(PostEmail_Obj collection , user_Model userData) 
        {
            bool status = false;

            //try
            //{
            //    string senderEmail = "jhanvimalhotra357@gmail.com";
            //    string senderPassword = "sagar@12345";
            //    SmtpClient client = new SmtpClient("smtp.gmail.com" ,587);
            //    client.EnableSsl = true;
            //    client.Timeout = 100000;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new NetworkCredential(senderEmail ,senderPassword);
            //    MailMessage message = new MailMessage(senderEmail ,collection["Toname"].ToString() ,collection["subject"].ToString() , "<p>email body 1</p>");
            //    message.IsBodyHtml = true;
            //    message.BodyEncoding=UTF8Encoding.UTF8;
            //    client.Send(message);
            //}
            //catch (Exception ex)
            //{

            //}

              try
              {


                  var senderEmail = new MailAddress(userData.user_name, "Mark<markushno357@gmail.com>");
                  var receiverEmail = new MailAddress(collection.to, "Receiver");
                  var password = userData.password;
                  var sub = collection.subject;

                MailAddress godaddy = new MailAddress("noreply@emailblasterservices.com");
                MailMessage message = new MailMessage(senderEmail.Address, receiverEmail.Address);
                message.Sender = godaddy;
                message.Body = collection.emailBody;
                message.IsBodyHtml = true;
                message.Subject = collection.subject;
                /* var mail = new MailAddress("shub@gmail.com");

                 //var body = collection["textarea"].ToString();
                 var body = collection.emailBody;
                 IPAddress[] ip = Dns.GetHostAddresses("smtp.gmail.com");
                 var smtp = new SmtpClient
                 {
                     Host = ip[0].ToString(),
                     Port = 587,
                     EnableSsl = true,
                     DeliveryMethod = SmtpDeliveryMethod.Network

                 };
                 using (var mess = new System.Net.Mail.MailMessage(mail, receiverEmail)
                 {
                     IsBodyHtml = true,
                     BodyEncoding = UTF8Encoding.UTF8,
                     Subject = collection.subject,
                     Body = body,
                     Sender = senderEmail
                 })
                 {
                     EmailService.NEVER_EAT_POISON_Disable_CertificateValidation();
                     smtp.Send(mess);
                 }*/


                MailAddress sender = new MailAddress("noreply@emailblasterservices.com");


            //MailMessage msgs = new MailMessage();
            //msgs.To.Add(receiverEmail.Address);
            //MailAddress address = new MailAddress(senderEmail.Address);
            //msgs.From = address;
            //msgs.Subject = collection.subject;
            //string htmlBody = collection.emailBody;
            //msgs.Body = htmlBody;
            //msgs.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Host = "relay-hosting.secureserver.net";
            client.Port = 25;
            client.UseDefaultCredentials = false;
                // client.Credentials = new System.Net.NetworkCredential(msgs.Sender.Address, "Za#&9=1u=a");
                client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Za#&9=1u=a");
                //Send the msgs  
                client.Send(message);
            status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }

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

        internal void SendEmail(FormCollection formCollection, object collection)
        {
            throw new NotImplementedException();
        }


    }
}