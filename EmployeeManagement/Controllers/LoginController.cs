using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessAccessLayer;
using DataAccessLayer;
using Helper;

namespace EmployeeManagement.Controllers
{
    public class LoginController : BaseController
    {
        private EmployeeBal _bal;

        public LoginController()
        {
            _bal = new EmployeeBal();
        }
        // GET: Login
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Index_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Index_Post(Login user)
        {
            TryUpdateModel(user);
            if (ModelState.IsValid)
            {

                var passcheck = _bal.CheckPass(user);
                Session["FirstName"] = passcheck.FirstName;
                Session["Id"] = passcheck.Id;

                if (passcheck.FirstName != null)
                {
                    Session["Password"] = user.Password;
                    var result = _bal.GetMenuInfo(passcheck.Id);
                    Session["User"] = result;

                    if (passcheck.RoleId == 1)
                    {
                        Session["Role"] = "Admin";
                    }
                    else
                    {
                        Session["Role"] = "Employee";
                    }

                    Success(string.Format("Login Successful, Welcome {0}", passcheck.FirstName), true);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    Danger("Invalid Username or Password", true);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [ActionName("ChangePass")]
        public ActionResult ChangePass_Get()
        {
            if (Session["Id"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ActionName("ChangePass")]
        public ActionResult ChangePass_Post(Password user)
        {
            TryUpdateModel(user, null, new[] { "Id", "Password", "NewPass" });
            if (ModelState.IsValid)
            {
                var res = _bal.UpdatePassword(user);
                if (res)
                {
                    Success("Password changed successfully", true);
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        [ActionName("Forgot")]
        public ActionResult Forgot_Get()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Forgot")]
        public ActionResult Forgot_Pot(string Email)
        {
            User res = _bal.GetUserFromEmail(Email);
            _bal.ResetPasswordEmail(Email, res.FirstName, res.Password);
            Success("Password Sent to Email Address", true);
            return RedirectToAction("Forgot");
        }
    }
}