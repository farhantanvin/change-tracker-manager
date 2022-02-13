using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GHIApplication.Models;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.ChangeManagement.Models;

namespace GHIApplication.ChangeManagement.Controllers
{
    public class AssignAuthorityApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }
          
            return Ok(db.AssignAuthorityInfo.Select(s => new { s.Id, s.ChangeType, s.Description, s.Active, s.EmployeeId }).ToList());
        }
        public IHttpActionResult Post(AssignAuthorityInfo changeAuthority)
        {
            int data = 0;

            var checkDuplicate = db.AssignAuthorityInfo.Where(f => f.EmployeeId == changeAuthority.EmployeeId && f.ChangeType == changeAuthority.ChangeType).Any();

            if (checkDuplicate == false)
            {
                changeAuthority.CreateBy = userName;
                changeAuthority.CreateDate = DateTime.Now;
                db.AssignAuthorityInfo.Add(changeAuthority);
                db.SaveChanges();
                data = 1;
            }
            else
            {
                data = 0;
            }
            return Ok(data);
        }
      
        public IHttpActionResult Put(int id, AssignAuthorityInfo changeAuthority)
        {
            var update = db.AssignAuthorityInfo.Find(id);
            if (update != null)
            {
                update.EmployeeId = changeAuthority.EmployeeId;
                update.ChangeType = changeAuthority.ChangeType;
                update.Description = changeAuthority.Description;
                update.UpdateBy = userName;
                update.UpdateDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Data Updated Successfully !");
        }
        public IHttpActionResult Get(int id)
        {
            var status = db.AssignAuthorityInfo.Find(id);
            status.Active = status.Active == 0 ? 1 : 0;
            db.Entry(status).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("Status Changed Successfully !");
        }

        public IHttpActionResult Delete(int id)
        {
            var delete = db.AssignAuthorityInfo.Where(s => s.Id == id).FirstOrDefault();
            db.AssignAuthorityInfo.Remove(delete);
            db.SaveChanges();
            return Ok("Data Deleted Successfully !!");
        }
    }
}
