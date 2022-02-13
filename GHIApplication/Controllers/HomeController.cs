using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Web.Mvc;
using System.Linq;
using System.Security.Cryptography;
using GHIApplication.Models;
using GHIApplication.ViewModel;

namespace GHIApplication.Controllers
{
    public class HomeController : Controller
    {
        readonly GHIDBContext db = new GHIDBContext();

        public ActionResult Index(int flag = 0)
        {
            ViewBag.Title = "GHITL - Login";
            if (flag == 1)
            {
                ViewBag.Message = "Invalid user id or password !!";
            }

            return View();
        }

        

        public ActionResult Login(EmployeeVM user)
        {

            var checkUser = db.EmployeeInfo
                         .Where(c => c.UserName == user.UserName && c.UserPassword == user.UserPassword && c.Status == 1)
                         .ToList();

            if (checkUser.Count > 0)
            {
                int empId = checkUser[0].Id; 

                var verificationAuth = db.VerificationAuthorityInfo
                        .Where(f => f.EmployeeId == empId)
                        .ToList();

                var riskAuth = db.RiskAuthorityInfo
                      .Where(f => f.EmployeeId == empId)
                      .ToList();

                var assignAuth = db.AssignAuthorityInfo
                    .Where(f => f.EmployeeId == empId)
                    .ToList();

                System.Web.HttpContext.Current.Session["userName"] = checkUser[0].UserName;
                System.Web.HttpContext.Current.Session["fullName"] = checkUser[0].EmployeeName;
                System.Web.HttpContext.Current.Session["cardNo"] = checkUser[0].CardNo;
                System.Web.HttpContext.Current.Session["employeeId"] = checkUser[0].Id;
                System.Web.HttpContext.Current.Session["admin"] = checkUser[0].AdminStatus;

                if (verificationAuth.Count > 0) {
                    System.Web.HttpContext.Current.Session["verificationAuth"] = verificationAuth[0].EmployeeId;
                }
                if (riskAuth.Count > 0) {
                    System.Web.HttpContext.Current.Session["riskAuth"] = riskAuth[0].EmployeeId;
                }
                if (assignAuth.Count > 0)
                {
                    System.Web.HttpContext.Current.Session["assignAuth"] = assignAuth[0].EmployeeId;
                }


                return RedirectToAction("Dashboard", "Dashboard");
            }

            int flag = 1;
            return RedirectToAction("Index", "Home", new { flag });
        }
        public ActionResult Logout()
        {
            Session["userName"] = null;
            Session["fullName"] = null;
            Session["cardNo"] = null;
            Session["employeeId"] = null;
            Session["admin"] = null;
            Session["verificationAuth"] = null;
            Session["riskAuth"] = null;
            Session["assignAuth"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
