using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.Models;
using GHIApplication.ChangeManagement.Models;
using System.Globalization;
using GHIApplication.ViewModel;

namespace GHIApplication.ChangeManagement.Controllers
{
    public class IncidentReportApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


    
       

        public IHttpActionResult GetInvestigationReportData()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            var data = (from a in db.IncidentInvestigationReport
                        select new
                       {
                           a.Id,
                           a.IncidentId,
                           a.IncidentDate,
                           a.PlaceOfIncident,
                           a.DiscoverPerson,
                           a.FollowUpDate,
                           a.FollowUpConcernPerson,
                       }).ToList();

            return Ok(data);
        }

        public String ConvertTime(TimeSpan eventTime)
        {
            var hours = eventTime.Hours;
            var minutes = eventTime.Minutes;
            var amPmDesignator = "AM";

            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = "PM";
            else if (hours > 12)
            {
                hours -= 12;
                amPmDesignator = "PM";
            }
            var formattedTime = String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
            return formattedTime;
        }


        public IHttpActionResult PostInvestigationReportSave(ReportVM data)
        {
            string msg = "";
            IncidentInvestigationReport newdata = new IncidentInvestigationReport();
            if (data != null)
            {
                int id = data.IncidentInvestigationReport2.IncidentId;
                var update = db.EventLog.Find(id);

                if (update != null)
                {
                    update.ReportStatus = 1;
                    db.Entry(update).State = EntityState.Modified;
                }

                newdata.IncidentId         = data.IncidentInvestigationReport2.IncidentId;
                newdata.IncidentType       = data.IncidentInvestigationReport2.incidentType;
                newdata.IncidentDate       = data.IncidentInvestigationReport1.IncidentDate;
                newdata.PlaceOfIncident    = data.IncidentInvestigationReport2.Location;
                newdata.WhatHappened       = data.IncidentInvestigationReport2.WhatHappened;
                newdata.ImpactBusiness     = data.IncidentInvestigationReport1.ImpactBusiness;
                newdata.ImpactCost         = data.IncidentInvestigationReport1.ImpactCost;
                newdata.DiscoverPerson     = data.IncidentInvestigationReport2.DiscoverBy;
                newdata.DiscoverDate       = data.IncidentInvestigationReport1.DiscoverDate;
                newdata.RootCauseIndcident = data.IncidentInvestigationReport1.RootCauseIndcident;
                newdata.RecoveryCost       = data.IncidentInvestigationReport1.RecoveryCost;
                newdata.DetailsCorrectiveAction = data.IncidentInvestigationReport1.DetailsCorrectiveAction;
                newdata.CorrectiveActionTakenBy = data.IncidentInvestigationReport1.CorrectiveActionTakenBy;
                newdata.CorrectiveActionTakenBy = data.IncidentInvestigationReport1.CorrectiveActionTakenBy;
                newdata.CorrectiveActionTakenByDate = data.IncidentInvestigationReport1.CorrectiveActionTakenByDate;
                newdata.DetailsPreventiveAction = data.IncidentInvestigationReport1.DetailsPreventiveAction;
                newdata.PreventiveActionTakenBy = data.IncidentInvestigationReport1.PreventiveActionTakenBy;
                newdata.PreventiveActionByDate = data.IncidentInvestigationReport1.PreventiveActionByDate;
                newdata.LearningFromIncident  = data.IncidentInvestigationReport1.LearningFromIncident;
                newdata.FollowUpConcernPerson = data.IncidentInvestigationReport1.FollowUpConcernPerson;
                newdata.FollowUpDate = data.IncidentInvestigationReport1.FollowUpDate;
                newdata.IsmrComments = data.IncidentInvestigationReport1.IsmrComments;
                newdata.IsmrName = data.IncidentInvestigationReport1.IsmrName;
                newdata.IsmrDate = data.IncidentInvestigationReport1.IsmrDate;
                newdata.CreateBy      = userName;
                newdata.CreateDate    = DateTime.Now;
                db.IncidentInvestigationReport.Add(newdata);
                db.SaveChanges();
                msg = "Incident Investigation Report Successfully Completed !";
            }
            else
            {
                msg = "Something Went Wrong !";
            }
            return Ok(msg);
        }


        public IHttpActionResult PostInvestigationReportData(IncidentInvestigationReport filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var data = db.IncidentInvestigationReport.Where(f => f.IncidentId == filter.Id).Select(s => new {
                s.IncidentId,
                s.IncidentType,
                s.IncidentDate,
                s.PlaceOfIncident,
                s.WhatHappened,
                s.ImpactBusiness,
                s.ImpactCost,
                s.DiscoverPerson,
                s.DiscoverDate,
                s.Id,
            }).FirstOrDefault();

            return Ok(data);
        }




    }
}
