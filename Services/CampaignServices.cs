﻿using BulkEmailMarketing.Models;
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
    public class CampaignServices
    {
        public string AddCampaign(FormCollection campaignData ,user_Model userData)
        {
            string status;
            try
            {
                Campaign_Model Model = new Campaign_Model();
                Model.campaignName = campaignData["CampaignName"];
                Model.attachmentCode = Guid.NewGuid().ToString();
                Model.emailBody = campaignData["textarea1"];
                Model.from = campaignData[""];
                Model.subject = campaignData["Subject"];
                Model.from = campaignData["Frominput"];
                Model.fromName= campaignData["Name"];
                string filename;
                if (!string.IsNullOrEmpty(campaignData["campaignLogo"].ToString()))
                {
                    filename = campaignData["campaignLogo"];
                }
                else
                {
                    filename = "";
                }
                
                string filepath = "~/upload/";

                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[9];
                    param[0] = new SqlParameter("@campaignName", Model.campaignName);
                    param[1] = new SqlParameter("@attachmentCode", Model.attachmentCode);
                    param[2] = new SqlParameter("@emailBody", Model.emailBody);
                    param[3] = new SqlParameter("@from", Model.from);
                    param[4] = new SqlParameter("@subject", Model.subject);
                    param[5] = new SqlParameter("@filename", filename);
                    param[6] = new SqlParameter("@filepath", filepath);
                    param[7] = new SqlParameter("@userid", userData.user_id); 
                    param[8] = new SqlParameter("@fromName", Model.fromName);

                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prccreateCampaign", param);
                    status = "success";
                }

            }
            catch (Exception ex)
            {
                status = "failed";
            }
            return status;
        }

        public Campaign_List GetCampaignList(user_Model userData)
        {
            DataSet ds = null;
            Campaign_List detail = new Campaign_List();
            try
            {
                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@userid" ,userData.user_id);
                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcgetCampaignData" , param);

                    if (ds.Tables.Count > 0)
                    {
                    
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Campaign_Model obj = new Campaign_Model();
                                if (!string.IsNullOrEmpty(dr["campaignid"].ToString()))
                                {
                                    obj.campaighId = Convert.ToInt32(dr["campaignid"].ToString());
                                }
                                else
                                {
                                    obj.campaighId = 0;
                                }
                                if (!string.IsNullOrEmpty(dr["canpaign_name"].ToString()))
                                {
                                    obj.campaignName = dr["canpaign_name"].ToString();
                                }
                                else
                                {
                                    obj.campaignName = "";
                                }
                                if (!string.IsNullOrEmpty(dr["campaign_from"].ToString()))
                                {
                                    obj.from = dr["campaign_from"].ToString();
                                }
                                else
                                {
                                    obj.from = "";
                                }
                                if (!string.IsNullOrEmpty(dr["templateContent"].ToString()))
                                {
                                    obj.emailBody = dr["templateContent"].ToString();
                                }
                                else
                                {
                                    obj.emailBody = "";
                                }
                                if (!string.IsNullOrEmpty(dr["campaign_subject"].ToString()))
                                {
                                    obj.subject = dr["campaign_subject"].ToString();
                                }
                                else
                                {
                                    obj.subject = "";
                                }
                                if (!string.IsNullOrEmpty(dr["FromName"].ToString()))
                                {
                                    obj.fromName = dr["FromName"].ToString();
                                }
                                else
                                {
                                    obj.fromName = "";
                                }
                                if (!string.IsNullOrEmpty(dr["campaignLogo"].ToString()))
                                {
                                    obj.campaignLogo = dr["campaignLogo"].ToString();
                                }
                                else
                                {
                                    obj.campaignLogo = "";
                                }

                                detail.list.Add(obj);
                            }
                            foreach (DataRow dr in ds.Tables[1].Rows)
                            {
                                SmtpConnection_Model objCons = new SmtpConnection_Model();
                                if (!string.IsNullOrEmpty(dr["connectionId"].ToString()))
                                {
                                objCons.connectionId = Convert.ToInt32(dr["connectionId"].ToString());
                                }
                                else
                                {
                                objCons.connectionId = 0;
                                }
                                if (!string.IsNullOrEmpty(dr["instanceName"].ToString()))
                                {
                                objCons.instanceName = dr["instanceName"].ToString();
                                }
                                else
                                {
                                objCons.instanceName = "";
                                }
                              

                                detail.Smtp.smtpConnectionList.Add(objCons);
                            }
                        
                    }
                    detail.message = "success";
                }

            }
            catch (Exception ex)
            {
                detail.message = "failed";
            }
            return detail;
        }
        public string DeleteCampaign(int id)
        {
            string status;
            try
            {
               

                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    DataSet ds = new DataSet();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@campaignId",id);
                
                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcdeleteCampaign", param);
                    status = "success";
                }

            }
            catch (Exception ex)
            {
                status = "failed";
            }
            return status;
        }

        
    public postBulkObj GetEmailStatus(user_Model userData)
        {
            DataSet ds = null;
            postBulkObj detail = new postBulkObj();
            try
            {
                using (SqlConnection db = ConnectionHelper.getConnection())
                {
                    db.Open();
                    ds = new DataSet();
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@userid", userData.user_id);
                    ds = SqlHelper.ExecuteDataset(db, CommandType.StoredProcedure, "prcGetEmailStatus" ,param);

                    if (ds.Tables.Count > 0)
                    {
                     
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PostEmail_Obj obj = new PostEmail_Obj();
                                if (!string.IsNullOrEmpty(dr["Logid"].ToString()))
                                {
                                    obj.campaignId = Convert.ToInt32(dr["Logid"].ToString());
                                }
                                else
                                {
                                    obj.campaignId = 0;
                                }
                                if (!string.IsNullOrEmpty(dr["canpaign_name"].ToString()))
                                {
                                    obj.Name = dr["canpaign_name"].ToString();
                                }
                                else
                                {
                                    obj.Name = "";
                                }
                                if (!string.IsNullOrEmpty(dr["campaign_subject"].ToString()))
                                {
                                    obj.subject = dr["campaign_subject"].ToString();
                                }
                                else
                                {
                                    obj.subject = "";
                                }
                                if (!string.IsNullOrEmpty(dr["toMailId"].ToString()))
                                {
                                    obj.to = dr["toMailId"].ToString();
                                }
                                else
                                {
                                    obj.to = "";
                                }
                                if (!string.IsNullOrEmpty(dr["status"].ToString()))
                                {
                                    obj.status = dr["status"].ToString();
                                }
                                else
                                {
                                    obj.status = "";
                                }
                                if (!string.IsNullOrEmpty(dr["createdDate"].ToString()))
                                {
                                    obj.createdDate = Convert.ToDateTime(dr["createdDate"].ToString());
                                }
                                else
                                {
                                    obj.createdDate = DateTime.Now;
                                }

                                detail.list.Add(obj);
                            }

                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            EmailSumary obj = new EmailSumary();
                            if (!string.IsNullOrEmpty(dr["EmailCount"].ToString()))
                            {
                                obj.statusCount = Convert.ToInt32(dr["EmailCount"].ToString());
                            }
                            else
                            {
                                obj.statusCount = 0;
                            }
                            if (!string.IsNullOrEmpty(dr["status"].ToString()))
                            {
                                obj.status = dr["status"].ToString();
                            }
                            else
                            {
                                obj.status = "";
                            }
                            detail.statusList.Add(obj);
                        }


                    }
        
                }

            }
            catch (Exception ex)
            {
       
            }
            return detail;
        }


    }
}