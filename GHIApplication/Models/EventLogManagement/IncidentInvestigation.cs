
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class IncidentInvestigation
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }

        public DateTime InvestigationDate { get; set; }

        [StringLength(500)]
        public string Location { get; set; }


        [StringLength(500)]
        public string InvestigationTeam { get; set; }

        [StringLength(1000)]
        public string WhatHappened { get; set; }

        [StringLength(1000)]
        public string WhatTask { get; set; }

        [StringLength(20)]
        public string WhatFactor { get; set; }

        [StringLength(500)]
        public string OtherFactor { get; set; }

        [StringLength(500)]
        public string Comments { get; set; }

        [StringLength(100)]
        public string DiscoverBy { get; set; }

        [StringLength(40)]
        public string incidentType { get; set; }

        [StringLength(20)]
        public string CreateBy { get; set; }

        [StringLength(20)]
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}