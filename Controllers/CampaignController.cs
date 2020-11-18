using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class CampaignController : AppBaseController
    {
        // GET: Campaign
        public static CampaignServices service = new CampaignServices();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Campaign/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Campaign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaign/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
          
                // TODO: Add insert logic here
                string status= service.AddCampaign(collection);

                return RedirectToAction("Index" ,"Home" ,new { status=status });
          
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Campaign/Edit/5
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

        // GET: Campaign/Delete/5
        public ActionResult Delete(int id)
        {
            service.DeleteCampaign(id);
            return RedirectToAction("Index" ,"Home");
        }

        // POST: Campaign/Delete/5
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
