using BulkEmailMarketing.Models;
using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class EmailController : AppBaseController
    {
        // GET: Email
        private static EmailService service = new EmailService();

        [HttpPost]
        public  JsonResult SendEmail(PostEmail_Obj postObj)
        {
            postObj.status = "Sending";
            int id = service.SaveEmail(postObj);
            postObj.filePath = Url.Action("GetLogo", "Home", new { unique=id });
          
            SmtpConnectionDetail_Model smtpDetial=service.getSmtpDetails(postObj.connectionId);
            string status = service.SendEmail(postObj , userData ,smtpDetial);
            postObj.status = status;
            if (status == "Email Sent")
                service.UpdateStatus(id, 2);
            else
                service.UpdateStatus(id, 1);
            var x = Json(status, JsonRequestBehavior.AllowGet);
            return (Json(status ,JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public JsonResult SaveTemplate(Campaign_Model postObj)
        {

            string status = service.SaveTemplate(postObj);
            
            return (Json(status, JsonRequestBehavior.AllowGet));
        }

        // GET: Email/Details/5SaveTemplate
        [HttpPost]
        public async Task<JsonResult> SendBulkEmail(postBulkObj postObj)
        {
            string status = "";
            //postObj.list = (List<PostEmail_Obj>)Session["excel"];
            SmtpConnectionDetail_Model smtpDetial = service.getSmtpDetails(postObj.connectionId);

            foreach (var obj in postObj.list)
            {
                obj.status = "Sending";
                obj.campaignId = postObj.campaignId;
                int id = service.SaveEmail(obj);
                obj.filePath = Url.Action("GetLogo", "Home", new { unique = id });

                obj.emailBody = postObj.BulkEmailBody;
                status =  service.SendEmail(obj , userData , smtpDetial);
                obj.status = status;
                if (status=="Email Sent")
                service.UpdateStatus(id, 2);
                else
                    service.UpdateStatus(id, 1);

                await Task.Delay(postObj.delay);
            }
            //bool status = service.SendEmail(postObj);
            //var x = Json(status, JsonRequestBehavior.AllowGet);
            
            return (Json(status ,JsonRequestBehavior.AllowGet));
        }
        // GET: Email/Create
       

        // POST: Email/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Email/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Email/Edit/5
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

        // GET: Email/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Email/Delete/5
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
