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
            this.records = new List<PostEmail_Obj>();
        }
        public string fileName { get; set; }
        public List<PostEmail_Obj> records { get; set; }
        public bool status { get; set; }
    }
}