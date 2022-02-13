
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class IncidentInvestigationReport
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }

        [StringLength(500)]
        public string IncidentType { get; set; }

        public DateTime IncidentDate { get; set; }

        [StringLength(500)]
        public string PlaceOfIncident { get; set; }

        [StringLength(1000)]
        public string WhatHappened { get; set; }

        [StringLength(1000)]
        public string ImpactBusiness { get; set; }

        [StringLength(1000)]
        public string ImpactCost { get; set; }

        [StringLength(40)]
        public string DiscoverPerson { get; set; }

        public Nullable<DateTime> DiscoverDate { get; set; }

        [StringLength(1000)]
        public string RootCauseIndcident { get; set; }

        public int RecoveryCost { get; set; }

        [StringLength(1000)]
        public string DetailsCorrectiveAction { get; set; }

        [StringLength(100)]
        public string CorrectiveActionTakenBy { get; set; }

        public Nullable<DateTime> CorrectiveActionTakenByDate { get; set; }

        [StringLength(1000)]
        public string DetailsPreventiveAction { get; set; }

        [StringLength(100)]
        public string PreventiveActionTakenBy { get; set; }


        public Nullable<DateTime> PreventiveActionByDate { get; set; }

        [StringLength(1000)]
        public string LearningFromIncident { get; set; }

        [StringLength(100)]
        public string FollowUpConcernPerson { get; set; }

        public Nullable<DateTime> FollowUpDate { get; set; }

        [StringLength(1000)]
        public string IsmrComments { get; set; }

        [StringLength(100)]
        public string IsmrName { get; set; }

        public Nullable<DateTime> IsmrDate { get; set; }

        [StringLength(20)]
        public string CreateBy { get; set; }

        [StringLength(20)]
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}