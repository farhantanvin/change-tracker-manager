using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using GHIApplication.Models;
using System;
using System.Web;
using System.Web.UI.WebControls;
using GHIApplication.ChangeManagement.ViewModel;
using GHIApplication.ChangeManagement.Models;
using GHIApplication.ViewModel;

namespace GHIApplication.ChangeManagement.Controllers
{
    public class ChangeApiController : ApiController
    {
        readonly GHIDBContext db = new GHIDBContext();
        readonly string userName = HttpContext.Current.Session["userName"]?.ToString();
        readonly string fullName = HttpContext.Current.Session["fullName"]?.ToString();
        readonly string cardNo = HttpContext.Current.Session["cardNo"]?.ToString();
        readonly int employeeId = (int)HttpContext.Current.Session["employeeId"];


        //Change Request Save
        public IHttpActionResult PostSaveRequest(ChangeVM data)
        {
            var msg = "";

            int year = DateTime.Now.Year;
            int lastId = db.ChangeInfo.OrderByDescending(o => o.Id).Select(s => s.Id).FirstOrDefault();
            int autoNo = lastId + 1;

            ChangeInfo newdata = new ChangeInfo();
            if (data != null)
            {
                newdata.SerialNo = "CRF-" + year + "-00" + autoNo;
                newdata.RequestByCardNo = cardNo;
                newdata.RequestByName = fullName;
                newdata.RequestDate = data.RequestDate;
                newdata.Description = data.Description;
                newdata.ChangeType = data.ChangeType;
                newdata.ChangeOther = data.ChangeOther;
                newdata.ChangePriority = data.ChangePriority;
                newdata.ChangeImpact = data.ChangeImpact;
                newdata.RollBackPlan = data.RollBackPlan;
                db.ChangeInfo.Add(newdata);
                db.SaveChanges();
                msg = "Request Successfully Submitted !";
            }
            else {
                msg = "Something Went Wrong !";
            }
            return Ok(msg);
        }


        //Change Request Pending List
        public IHttpActionResult GetChangeRequestPendingList()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }

            var q = (from a in db.ChangeInfo
                     join b in db.EmployeeInfo on a.RequestByCardNo equals b.CardNo
                     join c in db.ChangeTypeInfo on a.ChangeType equals c.Id
                     where b.ReportingCardNo == cardNo && b.Status == 1 && a.ChangeStatus == 0 && a.RecommendedBy ==null
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         c.ChangeTypeName,
                         a.ChangeOther,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         RequestBy = a.RequestByName + ", " + a.RequestByCardNo
                     }).ToList();
            return Ok(q);
        }

        //Change Recommended
        public IHttpActionResult PutChangeRecommended(int id, ChangeVM data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var update = db.ChangeInfo.Find(id);
            if (update != null)
            {
                update.ChangeStatus = 1;
                update.RecommendedBy = cardNo;
                update.RecommendedDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Request Successfully Approved !");
        }

        //Change Recommended
        public IHttpActionResult PutChangeCancel(int id, ChangeVM data)
        {
            var update = db.ChangeInfo.Find(id);
            if (update != null)
            {
                update.ChangeStatus = 3;
                update.CancelComments = data.CancelComments;
                update.RecommendedBy = cardNo;
                update.RecommendedDate = DateTime.Now;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Ok("Request Successfully Canceled !");
        }

        //Risk Assessment Request Pending List
        public IHttpActionResult GetRiskAssessmentPendingList()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }


            var changeAuthority = db.RiskAuthorityInfo.Where(f => f.EmployeeId == employeeId && f.Active == 1).Select(s => s.ChangeType).ToList();
          

            var q = (from a in db.ChangeInfo.Where(f => f.ChangeStatus == 1 && f.RecommendedBy !=null && f.RiskAssessmentBy == null && changeAuthority.Contains(f.ChangeType)).ToList()
                     join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         b.ChangeTypeName,
                         a.ChangeOther,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         RequestBy = a.RequestByName + ", " + a.RequestByCardNo,
                         a.RecommendedBy,
                         a.RecommendedDate
                     }).ToList();

            return Ok(q);
        }

        //Risk Assessment Save 
        public IHttpActionResult PostRiskAssessment(RiskVM data)
        {
            var update = db.ChangeInfo.Find(data.Id);
            if (update != null)
            {
                update.RiskAssessmentBy = cardNo;
                update.RiskAssessmentDate = data.AssessmentDate;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }

            foreach (var row in data.RowData)
            {
                var newData = new RiskAssessmentInfo
                {
                    ChangeId = update.Id,
                    AssessmentDate = data.AssessmentDate,
                    EmployeeId = employeeId,
                    Service = row.Service,
                    Vulnerability = row.Vulnerability,
                    Threat = row.Threat,
                    Plan = row.Plan,
                    CreateBy = userName,
                    CreateDate = DateTime.Now,
                };
                db.RiskAssessmentInfo.Add(newData);
            }

            db.SaveChanges();
            return Ok("Assessment Successfully Completed !");
        }

        //Assign Pending List
        public IHttpActionResult GetAssignPendingList()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }


            var changeAuthority = db.AssignAuthorityInfo.Where(f => f.EmployeeId == employeeId && f.Active == 1).Select(s => s.ChangeType).ToList();


            var q = (from a in db.ChangeInfo.Where(f => f.ChangeStatus == 1 && f.RecommendedBy !=null && f.RiskAssessmentBy !=null && f.ApprovedBy == null && changeAuthority.Contains(f.ChangeType)).ToList()
                     join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         b.ChangeTypeName,
                         a.ChangeOther,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         RequestBy = a.RequestByName + ", " + a.RequestByCardNo,
                         a.RecommendedBy,
                         a.RecommendedDate,
                         a.RiskAssessmentBy,
                         a.RiskAssessmentDate
                     }).ToList();

            return Ok(q);
        }

        //Risk Assessment Data 
        public IHttpActionResult PostRiskAssessmentData(RiskVM data)
        {
            var q = db.RiskAssessmentInfo.Where(f => f.ChangeId == data.Id).Select(s => new { s.Service, s.Vulnerability, s.Threat, s.Plan }).ToList();
            return Ok(q);
        }

        //Assign Person Save 
        public IHttpActionResult PostAssignPerson(AssignPerson data)
        {
            var update = db.ChangeInfo.Find(data.Id);
            if (update != null)
            {
                update.ApprovedBy = data.ApprovedBy;
                update.ApprovedDate = data.ApprovedDate;
                update.ApprovedComments = data.ApprovedComments;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Ok("Assign Successfully Completed !");
        }

        //Change By Pending List
        public IHttpActionResult GetAssignByPendingList()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }
          

            var q = (from a in db.ChangeInfo.Where(f => f.ChangeStatus == 1 && f.RecommendedBy !=null && f.RiskAssessmentBy !=null && f.ApprovedBy !=null && f.ChangedBy == null && f.ApprovedBy == cardNo).ToList()
                     join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         b.ChangeTypeName,
                         a.ChangeOther,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         RequestBy = a.RequestByName + ", " + a.RequestByCardNo,
                         a.RecommendedBy,
                         a.RecommendedDate,
                         a.RiskAssessmentBy,
                         a.RiskAssessmentDate,
                         a.ApprovedBy,
                         a.ApprovedDate,
                         a.ApprovedComments
                     }).ToList();

            return Ok(q);
        }


        //Assign Person Save 
        public IHttpActionResult PostChangeBy(AssignBy data)
        {
            var update = db.ChangeInfo.Find(data.Id);
            if (update != null)
            {
                update.ChangedBy = cardNo;
                update.ChangedDate = DateTime.Now;
                update.ChangedCompleteDate = data.ChangedDate;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Ok("Change Successfully Completed !");
        }

        //Verification Pending List
        public IHttpActionResult GetVerificationPendingList()
        {
            if (HttpContext.Current.Session["userName"] == null)
            {
                return Redirect(new Uri("/Home/Logout", UriKind.Relative));
            }


            var changeAuthority = db.VerificationAuthorityInfo.Where(f => f.EmployeeId == employeeId && f.Active == 1).Select(s => s.ChangeType).ToList();


            var q = (from a in db.ChangeInfo.Where(f => f.ChangeStatus == 1 && f.RecommendedBy !=null && f.RiskAssessmentBy !=null && f.ApprovedBy !=null && f.ChangedBy != null && f.VerificationBy == null && changeAuthority.Contains(f.ChangeType)).ToList()
                     join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                     select new
                     {
                         a.Id,
                         a.SerialNo,
                         a.RequestDate,
                         a.Description,
                         b.ChangeTypeName,
                         a.ChangeOther,
                         ChangePriority = a.ChangePriority == 1 ? "High" : a.ChangePriority == 2 ? "Medium" : "Low",
                         a.ChangeStatus,
                         a.ChangeImpact,
                         a.RollBackPlan,
                         RequestBy = a.RequestByName + ", " + a.RequestByCardNo,
                         a.RecommendedBy,
                         a.RecommendedDate,
                         a.RiskAssessmentBy,
                         a.RiskAssessmentDate,
                         a.ApprovedBy,
                         a.ApprovedDate,
                         a.ApprovedComments,
                         a.ChangedBy,
                         a.ChangedDate
                     }).ToList();

            return Ok(q);
        }

        //Verification Save 
        public IHttpActionResult PostVerificationSave(VerificationBy data)
        {
            var update = db.ChangeInfo.Find(data.Id);
            if (update != null)
            {
                update.VerificationBy = cardNo;
                update.VerificationDate = DateTime.Now;
                update.VerificationComments = data.VerificationComments;
                db.Entry(update).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Ok("Verification Successfully Completed !");
        }


        //Change Report Data
        public IHttpActionResult PostChangeReportData(ReportVM data)
        {

            var q = (from a in db.ChangeInfo
                      join b in db.ChangeTypeInfo on a.ChangeType equals b.Id
                      where a.VerificationBy !=null && a.RequestDate.Year == data.Year
                      select new
                      {
                          a.Id,
                          a.SerialNo,
                          a.RequestDate,
                          a.Description,
                          a.ChangeType,
                          ChangeTypeName = b.ChangeTypeName == "Other" ? a.ChangeOther : b.ChangeTypeName,
                          a.ChangeOther,
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
                          RequestBy = a.RequestByName +", "+a.RequestByCardNo,
                          a.RequestByCardNo,
                          a.RecommendedDate
                      }).OrderByDescending(o => o.Id).ToList();

            if (data.Month > 0) {
                q = q.Where(f => f.RequestDate.Month == data.Month).ToList();
            }
            if (data.ChangeType > 0) {
                q = q.Where(f => f.ChangeType == data.ChangeType).ToList();
            }
            if (!string.IsNullOrEmpty(data.CardNo)) {
                q = q.Where(f => f.RequestByCardNo == data.CardNo).ToList();
            }
            if (data.FromDate != DateTime.MinValue && data.FromDate != DateTime.MinValue)
            {
                q = q.Where(s => s.RequestDate >= data.FromDate.AddHours(6).Date && s.RequestDate <= data.ToDate.AddHours(6).Date).ToList();
            }
            return Ok(q);
        }
    }
}
