
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.ViewModel
{
    public class ChangeVM
    {
        public int Id { get; set; }
        public string RequestBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string Description { get; set; }
        public int ChangeType { get; set; }
        public string ChangeOther { get; set; }
        public int ChangePriority { get; set; }
        public string ChangeImpact { get; set; }
        public string RollBackPlan { get; set; }
        public string RecommendedBy { get; set; }
        public string CancelComments { get; set; }
    }

    public class RiskVM {
        public int Id { get; set; }
        public DateTime AssessmentDate { get; set; }
        public List<RiskDetailVM> RowData { get; set; }
    }

    public class RiskDetailVM
    {
        public int Id { get; set; }
        public string Service { get; set; }
        public string Vulnerability { get; set; }
        public string Threat { get; set; }
        public string Plan { get; set; }
    }

    public class AssignPerson { 
        public int Id { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string ApprovedComments { get; set; }

    }
    public class AssignBy
    {
        public int Id { get; set; }
        public DateTime ChangedDate { get; set; }
    }
    public class VerificationBy
    {
        public int Id { get; set; }
        public DateTime VerificationDate { get; set; }
        public string VerificationComments { get; set; }
    }
}