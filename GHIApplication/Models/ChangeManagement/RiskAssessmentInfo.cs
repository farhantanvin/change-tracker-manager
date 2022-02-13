
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class RiskAssessmentInfo
    {
        public int Id { get; set; }
        public int ChangeId { get; set; }
        public DateTime AssessmentDate { get; set; }
        public int EmployeeId {get;set;}

        [StringLength(1000)]
        public string Service { get; set; }

        [StringLength(1000)]
        public string Vulnerability { get; set; }

        [StringLength(1000)]
        public string Threat { get; set; }

        [StringLength(1000)]
        public string Plan { get; set; }

        [StringLength(20)]
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}