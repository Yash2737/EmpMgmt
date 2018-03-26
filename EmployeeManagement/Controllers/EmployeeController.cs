using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BusinessAccessLayer;
using Helper;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : BaseController
    {
        private EmployeeBal _bal;

        public EmployeeController()
        {
            _bal = new EmployeeBal();
        }

        [HttpGet]
        [ActionName("Add")]
        public ActionResult Add_Get()
        {
            if (Session["Id"] != null)
            {
                if ((string)Session["Role"] == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        [ActionName("Add")]
        public JsonResult Add_Post(User user)
        {
            TryUpdateModel(user, null, new[] { "Id", "FirstName", "LastName", "EmailPersonal", "DateofBirth", "DateofJoining", "Gender", "Status", "RoleId" });

            var res = _bal.AddEmployee(user);


            if (!string.IsNullOrEmpty(res.Password) || !string.IsNullOrEmpty(res.UniqueEmpCode))
            {
                user.Password = res.Password;
                user.UniqueEmpCode = res.UniqueEmpCode;

                _bal.SendEmail(user);
                Success("User " + user.FirstName + "created successfully Please check your mail", true);

                //return RedirectToAction("ViewList");
                return Json(user, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
                //return View();
            }

        }

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_Get(int id)
        {
            var res = _bal.GetEditInfo(id);
            return View(res);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_Post(Edit edit)
        {
            TryUpdateModel(edit, null, null, new[] { "Id" });
            if (ModelState.IsValid)
            {
                var res = _bal.UpdateEditInfo(edit);
                if (res)
                {
                    Success("Employee named " + edit.FirstName + " updated Successfully", true);
                    return RedirectToAction("ViewList");
                }
                else
                {
                    Danger("Employee named " + edit.FirstName + " could not be updated", true);
                    return RedirectToAction("ViewList");
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult ViewList()
        {
            List<User> res = _bal.GetAllEmployees();
            Session["List"] = res;
            if (Session["Id"] != null)
            {
                if ((string)Session["Role"] == "Admin")
                {
                    return View(res);
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var a = Session["List"] as List<User>;
            var delemp = a.Single(x => x.Id == id);
            if (delemp.RoleId != 1)
            {
                var res = _bal.DeleteEmployee(id);
                if (res)
                {
                    Success("Employee named " + delemp.FirstName + " deleted Successfully", true);
                    return RedirectToAction("ViewList");
                }
                else
                {
                    Danger("Employee named " + delemp.FirstName + " could not be deleted", true);
                    return RedirectToAction("ViewList");
                }
            }
            else
            {
                Danger("Employee named " + delemp.FirstName + " is a Admin, Naukri jayegi", true);
                return RedirectToAction("ViewList");
            }

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                id = (int)Session["Id"];

            }
            else
            {
                if (Session["Role"] as string != "Admin")
                {
                    id = 0;
                    return RedirectToAction("Index", "Login");
                }
            }
            //else if ((string)Session["Role"] != "Admin" && id != null)
            //{
            //    return RedirectToAction("Index", "Login");
            //}   

            var user = _bal.GetEmployeeDetails(id);
            return View(user);
        }

        public ActionResult Update(int id)
        {
            var employee = _bal.GetEmployeeDetails(id);
            if (Session["Role"] as string != "Admin")
            {
                TryUpdateModel(employee, null, null,
                    new[] { "Qualification", "Status", "UniqueEmpCode", "RoleId", "IsPasswordReset", "Password", "NewPass", "DateofJoining", "FullName" });
            }
            else
            {
                TryUpdateModel(employee, null, null, new[] { "IsPasswordReset", "Password", "NewPass", "FullName" });
            }


            var emp = _bal.UpdateEmployee(employee.User);
            if (emp)
            {
                if (Session["Role"] as string == "Admin")
                {

                    Success("Details Updated Successfully", true);
                    return RedirectToAction("ViewList");
                }
                else
                {
                    Success("Details Updated Successfully", true);
                    return RedirectToAction("Details");
                }
            }
            else
            {
                Danger("Update Faliure", true);
                return RedirectToAction("Dashboard", "Home");
            }


        }

        public ActionResult UpdateBank(Details bank)
        {
            bank.BankInfo.UserId = (int)Session["Id"];
            TryUpdateModel(bank.BankInfo, null, null, new[] { "" });
            if (ModelState.IsValid)
            {
                var res = _bal.UpdateBank(bank.BankInfo);
                if (res)
                {
                    Success("Bank Info Updated Successfully", true);
                    return RedirectToAction("Details");
                }
                else
                {
                    Danger("Cannot Update Bank Info", false);
                    return RedirectToAction("Details");
                }
            }
            else
            {
                return RedirectToAction("Details");
            }

        }


    }
}