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
    public class ChangeManagementController : Controller
    {
        readonly GHIDBContext db = new GHIDBContext();
        public ActionResult ChangeRequest()
        {
            ViewBag.Title = "Change Request - Form";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        public ActionResult ChangePendingList()
        {
            ViewBag.Title = "Change Request - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult RiskAssessmentList()
        {
            ViewBag.Title = "Risk Assessment - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult ChangeType()
        {
            ViewBag.Title = "Change Type - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult RiskAuthority()
        {
            ViewBag.Title = "Risk Authority - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult AssignAuthority()
        {
            ViewBag.Title = "Work Assign Authority - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult VerificationAuthority()
        {
            ViewBag.Title = "Work Assign Authority - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult ChangeAssignList()
        {
            ViewBag.Title = "Change Assign - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult ChangeByList()
        {
            ViewBag.Title = "Change By - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }
        public ActionResult VerificationByList()
        {
            ViewBag.Title = "Change Verification - List";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        public ActionResult ChangeReport()
        {
            ViewBag.Title = "Change - Report";
            if (Session["userName"] == null)
            {
                return RedirectToAction("Logout", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ChangeReport(ReportVM data)
        {
            ViewBag.Title = "Change - Report";

            var q1 = (from a in db.ChangeInfo
                     join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                     where a.Id == data.Id
                     select new
                     {
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         ChangeTypeName = b.ChangeTypeName=="Other" ?a.ChangeOther : b.ChangeTypeName,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         a.RecommendedBy,
                         a.RiskAssessmentBy,
                         a.RiskAssessmentDate,
                         a.ApprovedBy,
                         a.ApprovedDate,
                         a.ApprovedComments,
                         a.ChangedBy,
                         a.ChangedDate,
                         a.ChangedCompleteDate,
                         a.VerificationBy,
                         a.VerificationDate,
                         a.VerificationComments,
                         a.RequestByCardNo,
                         a.RequestByName,
                         a.RecommendedDate
                     }).ToList();

            var q2 = db.RiskAssessmentInfo.Where(f => f.ChangeId == data.Id).Select(s => new
            {
                s.AssessmentDate,
                s.Service,
                s.Vulnerability,
                s.Threat,
                s.Plan
            }).ToList();

            ReportViewer reportViewer = new ReportViewer
            {
                ProcessingMode = ProcessingMode.Local,
                ShowPrintButton = true,
                SizeToReportContent = true,
                Width = Unit.Percentage(100),
                Height = Unit.Percentage(100)
            };

            ReportDataSource data1 = new ReportDataSource("ReportMaster", q1);
            ReportDataSource data2 = new ReportDataSource("ReportDetail", q2);

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(data1);
            reportViewer.LocalReport.DataSources.Add(data2);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Report\ChangeReport.rdlc";
            reportViewer.LocalReport.DisplayName = q1[0].SerialNo + "_change_report";

            ViewBag.ReportViewer = reportViewer;
            return View("_ReportViewer");
        }
    }
}
