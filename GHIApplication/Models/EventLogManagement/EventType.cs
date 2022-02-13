
using System;
using System.ComponentModel.DataAnnotations;


namespace GHIApplication.ChangeManagement.Models
{
    public class EventType
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string EventTypeName { get; set; }

        public int Active { get; set; }

        [StringLength(20)]
        public string CreateBy { get; set; }

        [StringLength(20)]
        public string UpdateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public Nullable<DateTime> UpdateDate { get; set; }

    }
}