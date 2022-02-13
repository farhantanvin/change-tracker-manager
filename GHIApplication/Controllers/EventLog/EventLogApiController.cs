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
    public class EventLogApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


        //Event Log Save
        public IHttpActionResult PostSaveEventLog(EventLog data)
        {
            var msg = "";

            int year = DateTime.Now.Year;
            int lastId = db.EventLog.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault();
            int autoNo = lastId + 1;

            EventLog newdata = new EventLog();
            if (data != null)
            {
                newdata.SerialNo = "EVL-" + year + "-00" + autoNo;
                newdata.EventDate = data.EventDate;
                newdata.EventTime = data.EventTime;
                newdata.EventType = data.EventType;
                newdata.EventDetails = data.EventDetails;
                newdata.EventCause = data.EventCause;
                newdata.DiscoverBy = data.DiscoverBy;
                newdata.CreateBy = userName;
                newdata.CreateDate = DateTime.Now;
                db.EventLog.Add(newdata);
                db.SaveChanges();
                msg = "Event Log Successfully Submitted !";
            }
            else
            {
                msg = "Something Went Wrong !";
            }
            return Ok(msg);
        }

        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            var data = (from a in db.EventLog.Where(f => f.IncidentForward == null).ToList()
                     join b in db.EventType on a.EventType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.EventDate,
                         EventTime = ConvertTime(a.EventTime),
                         b.EventTypeName,
                         a.EventDetails,
                         a.EventCause,
                         a.DiscoverBy
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


        public IHttpActionResult Put(int id)
        {
            var update = db.EventLog.Find(id);
            if (update != null)
            {
                update.IncidentSerialNo = IncidentSerial();
                update.IncidentForward = "Yes";
                update.IncidentForwardDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Incident Forwarded Successfully !");
        }

        public String IncidentSerial() {            
            int countLast = db.EventLog.Where(f => f.IncidentForward == "Yes").Count();
            int year = DateTime.Now.Year;
            int autoNo = countLast + 1;
            return "INL-" + year + "-00" + autoNo;
        }

        public IHttpActionResult Delete(int id)
        {
            var delete = db.EventLog.Where(s => s.Id == id).FirstOrDefault();
            db.EventLog.Remove(delete);
            db.SaveChanges();
            return Ok("Data Deleted Successfully !!");
        }
        public IHttpActionResult PostEventUpdateData(EventLog data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var update = db.EventLog.Find(data.Id);
            if (update != null)
            {
                update.EventDate = data.EventDate;
                update.EventTime = data.EventTime;
                update.EventType = data.EventType;
                update.EventDetails = data.EventDetails;
                update.DiscoverBy = data.DiscoverBy;
                update.EventCause = data.EventCause;
                update.UpdateBy = userName;
                update.UpdateDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Event Log Successfully Updated !");
        }
        public IHttpActionResult PostEventFilterData(EventLog filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var data = db.EventLog.Select(s => new { s.Id, s.EventDate, s.EventTime, s.EventType, s.EventDetails, s.EventCause, s.DiscoverBy}).Where(f => f.Id == filter.Id).FirstOrDefault();
            return Ok(data);
        }
    }
}
