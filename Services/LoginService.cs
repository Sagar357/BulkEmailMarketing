using BulkEmailMarketing.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Services
{
    public class LoginService
    {
        //public user_Model ValidateUsers(Login_Model model)
        //{
        //    DataSet ds = null;
        //    user_Model obj = new user_Model();
        //    try
        //    {
        //        using (SqlConnection db = ConnectionHelper.getConnection())
        //        {
        //            db.Open();
        //            ds = new DataSet();
        //            SqlParameter[] param = new SqlParameter[2];
        //            param[0] = new SqlParameter("@username", model.UserName);
        //            param[1] = new SqlParameter("@password", model.Password);
        //            ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcValidateUsers", param);

        //            if (ds.Tables.Count > 0)
        //            {
        //                foreach (DataTable dt in ds.Tables)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        foreach (DataRow dr in dt.Rows)
        //                        {

        //                            if (!string.IsNullOrEmpty(dr["UserId"].ToString()))
        //                            {
        //                                obj.userId = Convert.ToInt32(dr["UserId"].ToString());
        //                            }
        //                            else
        //                            {
        //                                obj.userId = 0;
        //                            }
        //                            if (!string.IsNullOrEmpty(dr["UserName"].ToString()))
        //                            {
        //                                obj.username = dr["UserName"].ToString();
        //                            }
        //                            else
        //                            {
        //                                obj.username = "";
        //                            }
        //                            if (!string.IsNullOrEmpty(dr["userType"].ToString()))
        //                            {
        //                                obj.userType = Convert.ToInt32(dr["userType"]);
        //                            }
        //                            else
        //                            {
        //                                obj.userType = 0;
        //                            }


        //                        }

        //                    }
        //                    else
        //                    {
        //                        obj = null;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return obj;
        //}

    }
}