using BulkEmailMarketing.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Services
{
    public class EmailService
    {
        public  bool SendEmail(PostEmail_Obj collection) 
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
                
                    var senderEmail = new MailAddress("markushno357@gmail.com", "Mark");
                    var receiverEmail = new MailAddress(collection.to, "Receiver");
                    var password = "marcia@357";
                    var sub = collection.subject;
                //var body = collection["textarea"].ToString();
                var body=collection.emailBody;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        IsBodyHtml = true,   
                         BodyEncoding = UTF8Encoding.UTF8,
                         Subject = collection.subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                status = true;
            }
            catch (Exception ex)
            {
                status=false;
            }

            return status;
        }

        internal void SendEmail(FormCollection formCollection, object collection)
        {
            throw new NotImplementedException();
        }


    }
}