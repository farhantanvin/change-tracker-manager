
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class ChangeInfo
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string SerialNo { get; set; }
        public DateTime RequestDate { get; set; }

        [StringLength(100)]
        public string RequestByCardNo { get; set; }

        [StringLength(100)]
        public string RequestByName { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public int ChangeType { get; set; }
      
        [StringLength(200)]
        public string ChangeOther { get; set; }
        public int ChangePriority { get; set; }
        public int ChangeStatus { get; set; } // pending = 0, approved = 1, cancel = 3

        [StringLength(1000)]
        public string ChangeImpact { get; set; }

        [StringLength(1000)]
        public string RollBackPlan { get; set; }

        [StringLength(100)]
        public string RecommendedBy { get; set; }
        public Nullable<DateTime> RecommendedDate { get; set; }

        [StringLength(100)]
        public string RiskAssessmentBy { get; set; }
        public Nullable<DateTime> RiskAssessmentDate { get; set; }

        [StringLength(100)]
        public string ApprovedBy { get; set; }
        public Nullable<DateTime> ApprovedDate { get; set; }

        [StringLength(1000)]
        public string ApprovedComments { get; set; }

        [StringLength(100)]
        public string ChangedBy { get; set; }
        public Nullable<DateTime> ChangedDate { get; set; }
        public Nullable<DateTime> ChangedCompleteDate { get; set; }

        [StringLength(100)]
        public string VerificationBy { get; set; }
        public Nullable<DateTime> VerificationDate { get; set; }

        [StringLength(1000)]
        public string VerificationComments { get; set; }

        [StringLength(1000)]
        public string CancelComments { get; set; }
    }
}