using BulkEmailMarketing.Models;
using BulkEmailMarketing.utils;
using Flurl;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
        public string SendEmail(PostEmail_Obj collection, user_Model userData , SmtpConnectionDetail_Model smtpDetail)
        {
            string status = "false";
           
            try
            {
            
                using (SmtpClient client = new SmtpClient())
                {

                    var senderEmail = new MailAddress(userData.user_name, collection.Name);
                    var receiverEmail = new MailAddress(collection.to, "Receiver");
                    var sub = collection.subject;

                    /*var path = Url.Combine("http://emailblasterservices.com/", collection.filePath); */
                    var path = Url.Combine("https://membershipview.us/", collection.filePath);

                    MailAddress godaddy = new MailAddress(smtpDetail.instanceEmail);
                    //MailAddress godaddy = new MailAddress("support@host.earthithub.com");
                    //MailAddress godaddy = new MailAddress("noreply@emailblasterservices.com");
                    //MailAddress godaddy = new MailAddress("noreply@emailtick.com");
                    //MailAddress godaddy = new MailAddress("support@helpfulltips.us");
                    ContentType mimeType = new System.Net.Mime.ContentType("text/html");
                    string body = collection.emailBody + "<img alt=\"logo\" src=\"" + path + "\" style =\"float:left;height:90px;margin-left:5px;margin-right:5px;width:100px\" class=\"CToWUd\">";
                    AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);

                    MailMessage message = new MailMessage(senderEmail.Address, receiverEmail.Address);
                    message.Sender = godaddy;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.Subject = collection.subject;
                    message.AlternateViews.Add(alternate);


                    //client.Host = "relay-hosting.secureserver.net";
                    //client.Host = "host.earthithub.com";
                    client.Host = smtpDetail.serverString;
                    //client.Port = 25;
                    client.Port = 587;
                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, smtpDetail.instanceEmail);

                    /*Email Blaster*/
                    //client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Settings@123");
                    //client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Za#&9=1u=a");
                    //client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Emzfp!xY4x");
                    //client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "helpfulltips.us");
                    client.Send(message);
                }
                //Send the msgs  
                status = "Email Sent";
            }
            catch (Exception ex)
            {
                status = ex.Message;

            }
            return status;
        }

        public SmtpConnectionDetail_Model getSmtpDetails(int connectionId)
        {
            SmtpConnectionDetail_Model detail = null;
            string status = string.Empty;
            try
            {
                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[8];

                    param[0] = new SqlParameter("@connectionId", connectionId);
                    param[1] = new SqlParameter("@mode", 1);

                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcGetSmtpConnection", param);
                    if (ds.Tables.Count > 0)
                    {
                        detail = new SmtpConnectionDetail_Model();
                        foreach(DataRow dr in ds.Tables[0].Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["serverstring"].ToString()))
                            {
                                detail.serverString = dr["serverstring"].ToString();
                            }
                            else
                            {
                                detail.serverString = "";
                            }
                            if (!string.IsNullOrEmpty(dr["instanceemail"].ToString()))
                            {
                                detail.instanceEmail = dr["instanceemail"].ToString();
                            }
                            else
                            {
                                detail.instanceEmail = "";
                            }
                            if (!string.IsNullOrEmpty(dr["password"].ToString()))
                            {
                                detail.password = dr["password"].ToString();
                            }
                            else
                            {
                                detail.password = "";
                            }
                        }
                    }

                    //if (!string.IsNullOrEmpty(param[3].Value.ToString()))
                    //{
                    //    uniqueId = Convert.ToInt32(param[3].Value);
                    //}


                    //status = "success";
                }

            }
            catch (Exception Ex)
            {

            }
            return detail;
        }
        //Gmail SMTP


        //public string SendEmail(PostEmail_Obj collection, user_Model userData)
        //{
        //    string status = "false";
        //    try
        //    {

        //        //string senderEmail = "markushno357@gmail.com";
        //        var senderEmail = new MailAddress("markushno357@gmail.com", collection.Name);
        //        string senderPassword = "marcia@357";
        //        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        //        client.EnableSsl = true;
        //        client.Timeout = 100000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = false;

        //        client.Credentials = new NetworkCredential(senderEmail.Address, senderPassword);
        //        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        //        var path = Url.Combine("http://emailblasterservices.com/", collection.filePath);

        //        MailMessage message = new MailMessage(senderEmail.Address, collection.to, collection.subject, collection.emailBody + "<img alt=\"logo\" src=\"" + path + "\" style =\"float:left;height:90px;margin-left:5px;margin-right:5px;width:100px\" class=\"CToWUd\">");
        //        message.IsBodyHtml = true;
        //        message.BodyEncoding = UTF8Encoding.UTF8;
        //        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //        message.Headers.Add("Disposition-Notification-To", "sagar@massmancybergeeks.com");

        //        client.Send(message);
        //        status = "Email Sent";
        //    }
        //    catch (SmtpFailedRecipientsException ex)
        //    {
        //        status = ex.Message;
        //    }

        //    //try
        //    //{
        //    //    /*good code*/

        //    //    var senderEmail = new MailAddress(userData.user_name, collection.Name);
        //    //    var receiverEmail = new MailAddress(collection.to, "Receiver");
        //    //    var password = userData.password;
        //    //    var sub = collection.subject;


        //    //    //MailAddress godaddy = new MailAddress("noreply@emailblasterservices.com");
        //    //    MailAddress godaddy = new MailAddress("noreply@emailtick.com");
        //    //    MailMessage message = new MailMessage(senderEmail.Address, receiverEmail.Address);
        //    //    message.Sender = godaddy;
        //    //    message.Body = collection.emailBody;
        //    //    message.IsBodyHtml = true;
        //    //    message.Subject = collection.subject;
        //    //    /*good code*/

        //    //    /* var mail = new MailAddress("shub@gmail.com");

        //    //     //var body = collection["textarea"].ToString();
        //    //     var body = collection.emailBody;
        //    //     IPAddress[] ip = Dns.GetHostAddresses("smtp.gmail.com");
        //    //     var smtp = new SmtpClient
        //    //     {
        //    //         Host = ip[0].ToString(),
        //    //         Port = 587,
        //    //         EnableSsl = true,
        //    //         DeliveryMethod = SmtpDeliveryMethod.Network

        //    //     };
        //    //     using (var mess = new System.Net.Mail.MailMessage(mail, receiverEmail)
        //    //     {
        //    //         IsBodyHtml = true,
        //    //         BodyEncoding = UTF8Encoding.UTF8,
        //    //         Subject = collection.subject,
        //    //         Body = body,
        //    //         Sender = senderEmail
        //    //     })
        //    //     {
        //    //         EmailService.NEVER_EAT_POISON_Disable_CertificateValidation();
        //    //         smtp.Send(mess);
        //    //     }*/


        //    //    MailAddress sender = new MailAddress("noreply@emailblasterservices.com");


        //    //    //MailMessage msgs = new MailMessage();
        //    //    //msgs.To.Add(receiverEmail.Address);
        //    //    //MailAddress address = new MailAddress(senderEmail.Address);
        //    //    //msgs.From = address;
        //    //    //msgs.Subject = collection.subject;
        //    //    //string htmlBody = collection.emailBody;
        //    //    //msgs.Body = htmlBody;
        //    //    //msgs.IsBodyHtml = true;
        //    //    //SmtpClient client = new SmtpClient();

        //    //    using (SmtpClient client = new SmtpClient())
        //    //    {
        //    //        client.Host = "relay-hosting.secureserver.net";
        //    //        client.Port = 25;
        //    //        client.UseDefaultCredentials = false;
        //    //        // client.Credentials = new System.Net.NetworkCredential(msgs.Sender.Address,"Za#&9=1u=a" );
        //    //        client.Credentials = new System.Net.NetworkCredential(message.Sender.Address, "Emzfp!xY4x");
        //    //        client.Send(message);
        //    //    }
        //    //    //Send the msgs  
        //    //    status = "Email Sent";
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    var w32ex = ex as Win32Exception;
        //    //    if (w32ex == null)
        //    //    {
        //    //        w32ex = ex.InnerException as Win32Exception;
        //    //    }
        //    //    if (w32ex != null)
        //    //    {
        //    //        int code = w32ex.ErrorCode;
        //    //        // do stuff
        //    //    }

        //    //}

        //    //try
        //    //{

        //    //    System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
        //    //    Sender e-mail address.
        //    //   Msg.From = "markushno357@gmail.com";
        //    //    Recipient e-mail address.
        //    //   Msg.To = "sagar@massmancybergeeks.com";
        //    //    Msg.Subject = "Enquiry";
        //    //    Msg.Body = "Hi";
        //    //    IPAddress[] ip = Dns.GetHostAddresses("smtp.gmail.com");
        //    //    your remote SMTP server IP.
        //    //   SmtpMail.SmtpServer = ip[0].ToString();//your ip address
        //    //    SmtpMail.Send(Msg);

        //    //    Msg = null;


        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //}

        //    return status;
        //}


        public int SaveEmail(PostEmail_Obj Model)
        {
            string status;
            int uniqueId = -1;
            try
            {


                int emailStatus = 0;
                if (Model.status == "Email Sent")
                    emailStatus = 2;
                if (Model.status == "false")
                    emailStatus = 1;
                if (Model.status == "opened")
                    emailStatus = 3;
                if (Model.status == "Sending")
                    emailStatus = 4;

                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();                                                          
                    SqlParameter[] param = new SqlParameter[8];

                    param[0] = new SqlParameter("@campaignId", Model.campaignId);

                    param[1] = new SqlParameter("@toMailId", Model.to.ToString());

                    param[2] = new SqlParameter("@emailStatus", emailStatus);
                    param[3] = new SqlParameter("@output", SqlDbType.Int);
                    param[3].Direction = ParameterDirection.Output;

                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcSaveEmail", param);
                  
                    if (!string.IsNullOrEmpty(param[3].Value.ToString()))
                    {
                        uniqueId = Convert.ToInt32(param[3].Value);
                    }


                    status = "success";
                }

            }
            catch (Exception ex)
            {
                status = "failed";
            }
            return uniqueId;
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

        public string UpdateStatus(int id ,int statusId)
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

                    param[0] = new SqlParameter("@logId", id);

                    param[1] = new SqlParameter("@statusId", statusId);

                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcupdateEmailLog", param);
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