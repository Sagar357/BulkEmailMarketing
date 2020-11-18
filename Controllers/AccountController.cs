using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using BulkEmailMarketing.App_Start;
using BulkEmailMarketing.Models;
using BulkEmailMarketing.Services;
using System.Security.Cryptography;
using System.Text;

namespace CRM.Controllers
{
    public class AccountController : Controller
    {
        BulkEmailMarketing.App_Start.DatabaseContext db = new DatabaseContext();
        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            try
            {
                if (this.Request.IsAuthenticated)
                {
                    returnUrl = Url.Action("Index" ,"Home");
                    return this.RedirectToLocal(returnUrl);
                }
            }
            catch (Exception ex)
            {

            }
            return this.View("Index");
        }

        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return this.Redirect(returnUrl);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return this.RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authenticationManger = ctx.Authentication;
            authenticationManger.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login_Model model)
        {
            //string returnUrl = "Account";
            string controller = "Login";
            ////string returnUrl = model.returnUrl;
            //if (!string.IsNullOrEmpty(model.returnUrl) && !string.IsNullOrEmpty(model.controller))
            //{
            //    returnUrl = model.returnUrl;
            //    controller = model.controller;
            //}
            try
            {
                if (ModelState.IsValid)
                {

                    //var logininfo = db.User_Log.Where(x => (x.email.ToLower() == model.UserName.ToLower() || x.phone_number == model.UserName) && x.password == model.Password).ToList();
                    LoginService userObj = new LoginService();
                    user_Model logininfo = userObj.ValidateUsers(model);
                    if (logininfo != null)
                    {
                        var logindetails = logininfo;
                        this.SigninUser(logininfo, false);
                        return this.RedirectToLocal(Url.Action("Index" ,"Home"));
                        //return this.RedirectToAction(returnUrl ,controller);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return this.View("Index" ,model);
        }

        private void SigninUser(user_Model userdata, bool isPersistent)
        {
            var Claims = new List<Claim>();
            try
            {

                Claims.Add(new Claim(ClaimTypes.Name, userdata.user_name));
                Claims.Add(new Claim(ClaimTypes.Rsa, userdata.password.ToString()));
                Claims.Add(new Claim(ClaimTypes.NameIdentifier, userdata.user_id.ToString()));

                var claimIdenties = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);
                var ctx = Request.GetOwinContext();
                var authenticationManager = ctx.Authentication;
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, claimIdenties);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
