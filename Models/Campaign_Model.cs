using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Models
{
    public class Campaign_Model
    {
        public int campaighId { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string campaignName { get; set; }
        public string attachmentCode { get; set; }
        public string emailBody { get; set; }
        public string campaignLogo { get; set; }
        public string to { get; set; }
        public string fromName { get; set; }
    }

    public class PostEmail_Obj
    {
        public string from { get; set; }
        public string subject { get; set; }
        public string emailBody { get; set; }
        public string to { get; set; }
        public string filePath { get; set; }
        public string Name { get; set; }
        public int campaignId { get; set; }
        public string status { get; set; }
        public DateTime createdDate { get; set; }
        public int connectionId { get; set; }
    }

    public class postBulkObj
    {
        public postBulkObj() 
        {
            this.list = new List<PostEmail_Obj>();
            this.statusList = new List<EmailSumary>();
        }
        public List<PostEmail_Obj> list { get; set; }
        public List<EmailSumary> statusList { get; set; }
        public string BulkEmailBody { get; set; }
        public int delay { get; set; }
        public string Name { get; set; }
        public int campaignId { get; set; }
        public int connectionId { get; set; }

    }
    public class Campaign_List
    {
        public Campaign_List() 
        {
            this.list = new List<Campaign_Model>();
            this.Smtp = new SmtpConnection_List();
        }
        public List<Campaign_Model> list { get; set; }
        public SmtpConnection_List Smtp { get; set; }
        public string message { get; set; }
    }

    public class EmailSumary
    {
        public string status  { get; set; }
        public int statusCount { get; set; }
    }
  }