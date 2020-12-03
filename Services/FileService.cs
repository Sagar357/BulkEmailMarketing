using BulkEmailMarketing.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace BulkEmailMarketing.Services
{
    public class FileService
    {
        public File_Model Upload(HttpPostedFileBase[] files, string uploadPath ,string supportedFile)
        {
            File_Model response = null;
            string result = string.Empty;
            try
            {
                HttpPostedFileBase file = files[0];

                if (file.ContentLength > 0
                    && supportedFile.Contains(Path.GetExtension(file.FileName).ToString()))
                {

                    string date = DateTime.UtcNow.ToString("dd-mm-yyyy");
                    string uploadedFileName = date + Guid.NewGuid() + Path.GetExtension(file.FileName).ToString();
                    string[] err = { uploadPath , uploadedFileName };

                    System.IO.File.WriteAllLines(uploadPath+"/error.txt", err);
                    string savePath = Path.Combine(uploadPath, uploadedFileName);
                    string extension = Path.GetExtension(file.FileName).ToString();
                    file.SaveAs(savePath);
                   
                    response = new File_Model();
                    response.fileName = uploadedFileName;
                    response.status = true;
                    
                    response.filePath = Path.Combine(ConfigurationManager.AppSettings["domainPath"].ToString(), uploadedFileName);
                    result = uploadedFileName;
                }
            }
            catch (Exception ex)
            {
                response = new File_Model();
                response.status = false;
            }

            return (response);
        }

        public static List<PostEmail_Obj> ConvertExcelToDataTable(string FileName)
        {
            DataTable dtResult = null;
            List<PostEmail_Obj> list = new List<PostEmail_Obj>();
            int totalSheet = 0; //No of sheets on excel file  
            using (OleDbConnection objConn = new OleDbConnection(  
    @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
               
                if (dt != null)
                {
                  
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    var temp=
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                if (dtResult.Rows.Count > 0)
                {
                    foreach(DataRow dr in dtResult.Rows)
                    {
                        PostEmail_Obj postEmail_Obj = new PostEmail_Obj();
                        //if (!string.IsNullOrEmpty(dr["body"].ToString()))
                        //{
                        //    postEmail_Obj.emailBody = dr["body"].ToString();
                        //}
                        //else
                        //{
                        //    postEmail_Obj.emailBody = "";
                        //}
                        if (!string.IsNullOrEmpty(dr["EmailTo"].ToString()))
                        {
                            postEmail_Obj.to = dr["EmailTo"].ToString();
                        }
                        else
                        {
                            postEmail_Obj.to = "";
                        }
                        if (!string.IsNullOrEmpty(dr["Subject"].ToString()))
                        {
                            postEmail_Obj.subject = dr["Subject"].ToString();


                        }
                        else
                        {
                            postEmail_Obj.subject = "";
                        }
                        list.Add(postEmail_Obj);
                    }
                }
                return list; //Returning Dattable  
            }
        }
        public static List<PostEmail_Obj> ImportExcell(string strFileName, string Table)
        {
            List<PostEmail_Obj> list = new List<PostEmail_Obj>();

            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorksheet;
            Microsoft.Office.Interop.Excel.Range xlRange;
            int xlRow;
            //string strfilename = @"~\ExcellUpload\SampleData.xlsx";
            if (!string.IsNullOrEmpty(strFileName))
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(strFileName);
                xlWorksheet = xlWorkbook.Worksheets[Table];
                xlRange = xlWorksheet.UsedRange;
                Microsoft.Office.Interop.Excel.Range x = xlRange.Cells[2, 1];
                object[,] valueArray = (object[,])xlRange.get_Value(
           Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault);
                int i = 0;

                for (xlRow = 2; xlRow <= xlRange.Rows.Count; xlRow++)
                {
                    PostEmail_Obj emailObj = new PostEmail_Obj();
                    //dr["index"] = valueArray[xlRow, 1];
                    emailObj.to = valueArray[xlRow, 2].ToString();
                    emailObj.subject = valueArray[xlRow, 3].ToString();
                    emailObj.emailBody = valueArray[xlRow, 4].ToString();
                    list.Add(emailObj);

                }
                xlWorkbook.Close();
                xlApp.Quit();
            }
            return list;
        }


    }
}