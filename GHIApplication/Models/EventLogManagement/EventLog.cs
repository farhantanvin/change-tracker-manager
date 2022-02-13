
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class EventLog
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string SerialNo { get; set; }

        [StringLength(20)]
        public string IncidentSerialNo { get; set; }

        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set;}
        public int EventType { get; set; }
        public string EventDetails { get; set; }
        public string EventCause { get; set; }

        [StringLength(5)]
        public string IncidentForward { get; set; }
        public Nullable<DateTime> IncidentForwardDate { get; set; }

        public int InvestigationStatus { get; set; }

        public int ReportStatus { get; set; }

        [StringLength(100)]
        public string DiscoverBy { get; set; }


        [StringLength(20)]
        public string CreateBy { get; set; }

        [StringLength(20)]
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}