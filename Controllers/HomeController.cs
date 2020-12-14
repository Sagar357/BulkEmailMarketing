﻿using BulkEmailMarketing.Models;
using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class HomeController : AppBaseController
    {
        public async Task<ActionResult> Index(string status="")
        {
            ViewBag.status = status;
            CampaignServices service = new CampaignServices();
            Campaign_List list=service.GetCampaignList(userData);
            return View("index2");
           // return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}