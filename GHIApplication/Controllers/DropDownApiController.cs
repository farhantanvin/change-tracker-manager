using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.Models;

namespace GHIApplication.Controllers
{
    public class DropDownApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        

        public IHttpActionResult GetChangeTypeData()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            return Ok(db.ChangeTypeInfo.Where(f => f.Active == 1).Select(s => new { s.Id, s.ChangeTypeName }).OrderBy(b => b.ChangeTypeName).ToList());
        }
        public IHttpActionResult GetCardNoData()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            return Ok(db.EmployeeInfo.Where(f => f.Status == 1 && f.CardNo !=null).Select(s => new { s.Id, CardNo = s.CardNo.ToUpper() +" ("+ s.EmployeeName + ")", s.EmployeeName, ChangeCardNo = s.CardNo}).OrderBy(b => b.CardNo).ToList());
        }

        public IHttpActionResult GetEventTypeData()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            return Ok(db.EventType.Where(f => f.Active == 1).Select(s => new { s.Id, s.EventTypeName }).OrderBy(b => b.EventTypeName).ToList());
        }
    }
}
