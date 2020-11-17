using BulkEmailMarketing.Models;
using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        // GET: File/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: File/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: File/Create
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase[] files)
        {
            File_Model model = new File_Model();
            string res = "";
            if (ModelState.IsValid)
            {
                // TODO: Add insert logic here
                FileService services = new FileService();
                string uploadPath = Server.MapPath(ConfigurationManager.AppSettings["AttachmentPath"]);
                string supportedFile = ConfigurationManager.AppSettings["FilesSupported"]; 
                 model = services.Upload(files, uploadPath ,supportedFile);
                HttpPostedFileBase file = files[0];
                string extension = Path.GetExtension(file.FileName).ToString();
                if (extension == ".xlsx")
                {
                    string x = Path.Combine( uploadPath , model.fileName);
                    model.records = FileService.ImportExcell(x, "Sheet1");

                }
            }
            else
            {
                model.status = false;
            }
            var v = Json(model, JsonRequestBehavior.AllowGet);
            return (Json(model, JsonRequestBehavior.AllowGet));
        }

        // GET: File/Edit/5E:\react\Projects\BulkEmailMarketing\upload\
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: File/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: File/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: File/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
