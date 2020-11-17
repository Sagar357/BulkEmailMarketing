using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Models
{
    public class File_Model
    {
        public File_Model() 
        {
            this.records = new ExcelData();
        }
        public string fileName { get; set; }
        public ExcelData records { get; set; }
        public bool status { get; set; }
    }
}