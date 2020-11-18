using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BulkEmailMarketing.Models;
using System.Data;
using CRM;

namespace BulkEmailMarketing.App_Start
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=BulkEmailPrjEntities")
        {

        }
        public DbSet<user_Model> User_Log { get; set; }

    }

}