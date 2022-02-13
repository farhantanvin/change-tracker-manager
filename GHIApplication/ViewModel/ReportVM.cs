
using GHIApplication.ChangeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ViewModel
{
    public class ReportVM
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ChangeType { get; set; }
        public string CardNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public IncidentInvestigationReport IncidentInvestigationReport1 { get; set; }
        public IncidentInvestigation IncidentInvestigationReport2 { get; set; }
    }

}