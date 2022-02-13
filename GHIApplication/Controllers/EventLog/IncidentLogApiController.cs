using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.Models;
using GHIApplication.ChangeManagement.Models;
using System.Globalization;

namespace GHIApplication.ChangeManagement.Controllers
{
    public class IncidentLogApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


    
        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            var data = (from a in db.EventLog.Where(f => f.IncidentForward == "Yes" && f.InvestigationStatus == 0).ToList()
                     join b in db.EventType on a.EventType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.IncidentSerialNo,
                         a.IncidentForwardDate,
                         a.EventDate,
                         EventTime = ConvertTime(a.EventTime),
                         b.EventTypeName,
                         a.EventDetails,
                         a.EventCause,
                         a.DiscoverBy
                     }).ToList();

            return Ok(data);
        }

        public IHttpActionResult GetInvestigationData()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            var data = (from a in db.EventLog.Where(f => f.InvestigationStatus == 1 && f.ReportStatus == 0).ToList()
                        join b in db.EventType on a.EventType equals b.Id
                        select new
                        {
                            a.Id,
                            a.SerialNo,
                            a.IncidentSerialNo,
                            a.IncidentForwardDate,
                            a.EventDate,
                            EventTime = ConvertTime(a.EventTime),
                            b.EventTypeName,
                            a.EventDetails,
                            a.EventCause,
                            a.DiscoverBy,
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


        public IHttpActionResult PostInvestigationSave(IncidentInvestigation data)
        {
            string msg = "";
            IncidentInvestigation newdata = new IncidentInvestigation();
            if (data != null)
            {
                int id = data.IncidentId;
                var update = db.EventLog.Find(id);

                if (update != null)
                {
                    update.InvestigationStatus = 1;
                    db.Entry(update).State = EntityState.Modified;
                }

                newdata.IncidentId = data.IncidentId;
                newdata.InvestigationDate = data.InvestigationDate;
                newdata.Location = data.Location;
                newdata.InvestigationTeam = data.InvestigationTeam;
                newdata.WhatHappened = data.WhatHappened;
                newdata.WhatTask = data.WhatTask;
                newdata.WhatFactor = data.WhatFactor;
                newdata.OtherFactor = data.OtherFactor;
                newdata.Comments = data.Comments;
                newdata.DiscoverBy = data.DiscoverBy;
                newdata.incidentType = data.incidentType;
                newdata.CreateBy = userName;
                newdata.CreateDate = DateTime.Now;
                db.IncidentInvestigation.Add(newdata);
                db.SaveChanges();
                msg = "Incident Investigation Successfully Completed !";
            }
            else
            {
                msg = "Something Went Wrong !";
            }
            return Ok(msg);
        }

        public IHttpActionResult PostInvestigationData(EventLog filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var data = db.IncidentInvestigation.Where(f=> f.IncidentId == filter.Id).Select(s => new { 
                s.InvestigationDate, 
                s.IncidentId,
                s.Location, 
                s.InvestigationTeam,
                s.WhatHappened,
                s.WhatTask,
                s.WhatFactor, 
                s.OtherFactor,
                s.Comments,
                s.Id,
                s.DiscoverBy,
                s.incidentType
            }).FirstOrDefault();

            return Ok(data);
        }

        public IHttpActionResult PostInvestigationUpdateData(IncidentInvestigation data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var update = db.IncidentInvestigation.Find(data.Id);
            if (update != null)
            {
                update.InvestigationDate = data.InvestigationDate;
                update.Location = data.Location;
                update.InvestigationTeam = data.InvestigationTeam;
                update.WhatHappened = data.WhatHappened;
                update.WhatTask = data.WhatTask;
                update.WhatFactor = data.WhatFactor;
                update.OtherFactor = data.OtherFactor;
                update.Comments = data.Comments;
                update.DiscoverBy = data.DiscoverBy;
                update.incidentType = data.incidentType;
                update.UpdateBy = userName;
                update.UpdateDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Incident Investigation Successfully Updated !");
        }

    }
}
