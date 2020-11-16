using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        private static EmailService service = new EmailService();
        public async Task<ActionResult> SendEmail(FormCollection collection)
        {

            await service.SendEmail( collection);
            Task.Delay(1000);
            return RedirectToAction("Index" ,"Home",null);
        }

        // GET: Email/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }

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
