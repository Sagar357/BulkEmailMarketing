using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Models
{
    public class SmtpConnection_Model
    {
        public int connectionId { get; set; }
        public string instanceName { get; set; }
    }

    public class SmtpConnection_List
    {
        public SmtpConnection_List()
        {
            this.smtpConnectionList = new List<SmtpConnection_Model>();
        }
        public List<SmtpConnection_Model> smtpConnectionList { get; set; }
    }

    public class SmtpConnectionDetail_Model
    {
        public string serverString { get; set; }
        public string instanceEmail { get; set; }
        public string  password { get; set; }
    }
}