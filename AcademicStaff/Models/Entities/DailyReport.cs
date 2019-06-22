using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Models.Entities
{
    public class DailyReport
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime? ReportDate { get; set; }
        public DateTime DateCreated { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}