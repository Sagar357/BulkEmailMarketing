using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Models
{
    public class ExcelData
    {
        public ExcelData()
        {
            this.smsRecords = new DataTable();
            this.status = false;
        }
        public DataTable smsRecords { get; set; }
        public bool status { get; set; }
    }
}