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
    }

    public class Campaign_List
    {
        public Campaign_List() 
        {
            this.list = new List<Campaign_Model>(); 
        }
        public List<Campaign_Model> list { get; set; }
        public string message { get; set; }
    }
}