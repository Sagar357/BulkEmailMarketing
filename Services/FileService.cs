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

        public static ExcelData ImportExcell(string strFileName, string Table)
        {
            ExcelData records = new ExcelData();
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
                records.smsRecords.Columns.Add("index");
                records.smsRecords.Columns.Add("EmailTo");
                records.smsRecords.Columns.Add("Subject");
                records.smsRecords.Columns.Add("body");

                for (xlRow = 2; xlRow <= xlRange.Rows.Count; xlRow++)
                {
                    DataRow dr = records.smsRecords.NewRow();
                    dr["index"] = valueArray[xlRow, 1];
                    dr["name"] = valueArray[xlRow, 2];
                    dr["phoneno"] = valueArray[xlRow, 3];
                    dr["message"] = valueArray[xlRow, 4];
                    records.smsRecords.Rows.Add(dr);

                }
                records.status = true;
                xlWorkbook.Close();
                xlApp.Quit();
            }
            return records;
        }


    }
}