using BulkEmailMarketing.Models;
using BulkEmailMarketing.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
            return View(list);
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


        public ActionResult GetLogo(string mail)
        {
            string filePath = Server.MapPath("~/img");
            string path = Path.Combine(filePath, "camp1.png");
            Byte[] b = System.IO.File.ReadAllBytes(path);   // You can use your own method over here.         
            return File(b, "image/jpeg");
        }
    }
}