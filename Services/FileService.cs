using BulkEmailMarketing.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                    && supportedFile.Contains( Path.GetExtension(file.FileName).ToString()))
                {

                    string date = DateTime.UtcNow.ToString("dd-mm-yyyy");
                    string uploadedFileName = date + Guid.NewGuid() + Path.GetExtension(file.FileName).ToString();
                    string savePath = Path.Combine(uploadPath, uploadedFileName);
                    string extension = Path.GetExtension(file.FileName).ToString();
                    file.SaveAs(savePath);
                   
                    response = new File_Model();
                    response.fileName = uploadedFileName;
                    response.status = true;
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