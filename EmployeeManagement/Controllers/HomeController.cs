using System.Web.Mvc;
using BusinessAccessLayer;
using Helper;

namespace EmployeeManagement.Controllers
{
    public class HomeController : BaseController
    {
        private EmployeeBal _bal;
        // GET: Home
        public HomeController()
        {
            _bal = new EmployeeBal();
        }

        public ActionResult Index()
        {
            var list = _bal.GetAllEmployees();
            ViewBag.Employees = list;
            return View();
        }

        //public ActionResult Details(int id)
        //{
        //    //var empl = _bal.GetEmployeeDetails(id);
        //    //EmployeeContext employeeContext = new EmployeeContext();
        //    ////var emp = employeeContext.Employees.Single(x => x.EmployeeId == id);
        //    //return View(empl);
        //}

        
        public ActionResult Dashboard()
        {
            if (Session["Id"] != null)
            {
                return View();
            }
            else
            {
               return RedirectToAction("Index", "Login");
            }
            
        }
        
    }
}