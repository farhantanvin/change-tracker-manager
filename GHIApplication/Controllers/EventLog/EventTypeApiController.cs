using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.Models;
using GHIApplication.ChangeManagement.Models;

namespace GHIApplication.ChangeManagement.Controllers
{
    public class EventTypeApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }
          
            return Ok(db.EventType.Select(s => new { s.Id, s.EventTypeName, s.Active }).ToList());
        }
        public IHttpActionResult Post(EventType eventType)
        {
            int data = 0;

            var checkDuplicate = db.EventType.Where(f => f.EventTypeName == eventType.EventTypeName).Any();

            if (checkDuplicate == false)
            {
                eventType.Active = 1;
                eventType.CreateBy = userName;
                eventType.CreateDate = DateTime.Now;
                db.EventType.Add(eventType);
                db.SaveChanges();
                data = 1;
            }
            else
            {
                data = 0;
            }
            return Ok(data);
        }
      
        public IHttpActionResult Put(int id, EventType eventType)
        {
            var update = db.EventType.Find(id);
            if (update != null)
            {
                update.EventTypeName = eventType.EventTypeName;
                update.UpdateBy = userName;
                update.UpdateDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Data Updated Successfully !");
        }
        public IHttpActionResult Get(int id)
        {
            var status = db.EventType.Find(id);
            status.Active = status.Active == 0 ? 1 : 0;
            db.Entry(status).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("Status Changed Successfully !");
        }

        public IHttpActionResult Delete(int id)
        {
            var delete = db.EventType.Where(s => s.Id == id).FirstOrDefault();
            db.EventType.Remove(delete);
            db.SaveChanges();
            return Ok("Data Deleted Successfully !!");
        }
    }
}
