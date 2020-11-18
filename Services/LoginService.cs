using BulkEmailMarketing.Models;
using BulkEmailMarketing.utils;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Services
{
    public class LoginService
    {
        public user_Model ValidateUsers(Login_Model model)
        {
            DataSet ds = null;
            user_Model obj = new user_Model();
            try
            {
                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@username", model.UserName);
                    param[1] = new SqlParameter("@password", model.Password);
                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcValidateUsers", param);

                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {

                                    if (!string.IsNullOrEmpty(dr["user_id"].ToString()))
                                    {
                                        obj.user_id = Convert.ToInt32(dr["user_id"].ToString());
                                    }
                                    else
                                    {
                                        obj.user_id = 0;
                                    }
                                    if (!string.IsNullOrEmpty(dr["email"].ToString()))
                                    {
                                        obj.user_name = dr["email"].ToString();
                                    }
                                    else
                                    {
                                        obj.user_name = "";
                                    }

                                    if (!string.IsNullOrEmpty(dr["password"].ToString()))
                                    {
                                        obj.password = dr["password"].ToString();
                                    }
                                    else
                                    {
                                        obj.password = "";
                                    }

                                }

                            }
                            else
                            {
                                obj = null;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return obj;
        }
        public string AddUser(FormCollection userData)
        {
            string status;
            try
            {
                user_Model Model = new user_Model();
                Model.user_name = userData["Username1"];
                Model.password = userData["password2"];
                Model.email = userData["email1"];

                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@userName", Model.user_name);
                    param[1] = new SqlParameter("@password", Model.password);
                    param[2] = new SqlParameter("@email", Model.email);
              
                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcCreateUser", param);
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