
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class VerificationAuthorityInfo
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ChangeType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        public int Active { get; set; }

        [StringLength(20)]
        public string CreateBy { get; set; }

        [StringLength(20)]
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}