using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using GHIApplication.ViewModel;
using GHIApplication.Models;

namespace GHIApplication.Controllers
{
    public class EventLogManagementController : Controller
    {
        readonly GHIDBContext db = new GHIDBContext();
        public ActionResult EventType()
        {
            ViewBag.Title = "Event Type";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        public ActionResult EventLog()
        {
            ViewBag.Title = "Event Log - Entry Form";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        
        public ActionResult AllEventLogList()
        {
            ViewBag.Title = "Event Log - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult IncidentLogList()
        {
            ViewBag.Title = "Incident Log - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        public ActionResult IncidentInvestigationList()
        {
            ViewBag.Title = "Incident Investigation - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        public ActionResult IncidentInvestigationReportList()
        {
            ViewBag.Title = "Incident Investigation Report List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
    }
}
