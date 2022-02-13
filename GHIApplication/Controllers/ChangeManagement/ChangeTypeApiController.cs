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
    public class ChangeTypeApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();


        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            return Ok(db.ChangeTypeInfo.Select(s => new { s.Id, s.ChangeTypeName, s.Active }).ToList().OrderBy(b => b.ChangeTypeName));
        }
        public IHttpActionResult Post(ChangeTypeInfo changeType)
        {
            var data = "";

            var checkDuplicate = db.ChangeTypeInfo.Where(f =>
                f.ChangeTypeName == changeType.ChangeTypeName.ToUpper() ||
                f.ChangeTypeName == changeType.ChangeTypeName.ToLower()).Any();

            if (checkDuplicate == false)
            {
                changeType.CreateBy = userName;
                changeType.CreateDate = DateTime.Now;
                db.ChangeTypeInfo.Add(changeType);
                db.SaveChanges();
                data = "Data Saved Successfully !";
            }
            else
            {
                data = "Change Type Name Already Exists !";
            }
            return Ok(data);
        }
      
        public IHttpActionResult Put(int id, ChangeTypeInfo changeType)
        {
            var update = db.ChangeTypeInfo.Find(id);
            if (update != null)
            {
                update.ChangeTypeName = changeType.ChangeTypeName;
                update.UpdateBy = userName;
                update.UpdateDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Data Updated Successfully !");
        }
        public IHttpActionResult Get(int id)
        {
            var status = db.ChangeTypeInfo.Find(id);
            status.Active = status.Active == 0 ? 1 : 0;
            db.Entry(status).State = EntityState.Modified;
            db.SaveChanges();
            return Ok("Status Changed Successfully !");
        }

        public IHttpActionResult Delete(int id)
        {
            var delete = db.ChangeTypeInfo.Where(s => s.Id == id).FirstOrDefault();
            db.ChangeTypeInfo.Remove(delete);
            db.SaveChanges();
            return Ok("Data Deleted Successfully !!");
        }
    }
}
