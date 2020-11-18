using BulkEmailMarketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BulkEmailMarketing.Controllers
{
    public class AppBaseController : Controller
    {

        // GET: AppBase
        protected static user_Model userData = null;
        public AppBaseController()
        {
            userData = getClaims();
            string role = string.Empty;
            if (userData != null)
            {

                
                ViewBag.username = userData.user_name;
            }
        }


        public static user_Model getClaims()
        {
            var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            if (claims.Count > 0)
            {
                userData = new user_Model();

                userData.user_id = Convert.ToInt32(claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", StringComparison.OrdinalIgnoreCase))?.Value);
                userData.password = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/rsa", StringComparison.OrdinalIgnoreCase))?.Value;
                userData.user_name = claims?.FirstOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.OrdinalIgnoreCase))?.Value;
            }
            return userData;
        }
    }
}
