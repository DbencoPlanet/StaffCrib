using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AcademicStaff.Models.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        //[Required]
        [Display(Name = "Task Title")]
        public string TaskTitle { get; set; }
        [AllowHtml]
        public string TaskDiscription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? DateCreated { get; set; }
        //public int? ReportId { get; set; }
        //public Report Report { get; set; }
        public ICollection<Report> Report { get; set; }
        [UIHint("Enum")]
        public TaskStatus TaskStatus { get; set; }


    }
}