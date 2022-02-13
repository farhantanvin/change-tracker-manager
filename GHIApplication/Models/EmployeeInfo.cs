
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.Models
{
    public class EmployeeInfo
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string CardNo { get; set; }

        [StringLength(100)]
        public string EmployeeName { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(20)]
        public string UserPassword { get; set; }
        public int Status { get; set; }
        public int AdminStatus { get; set; }

        [StringLength(200)]
        public string ReportingCardNo { get; set; }

        [StringLength(200)]
        public string ReportingEmployeeName { get; set; }

    }
}