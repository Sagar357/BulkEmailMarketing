using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.utils
{
    public class ConnectionHelper
    {
        private static SqlConnection con;

        public static SqlConnection getConnection()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["PortalConnectionString"].ToString());
            return con;
        }
    }
}