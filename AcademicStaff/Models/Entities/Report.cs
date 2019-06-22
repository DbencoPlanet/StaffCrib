using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicStaff.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AcademicStaff.Models.Entities
{

   
    public class Report
    {
       

        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? TaskId { get; set; }
        public Tasks Task { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        [AllowHtml]
        public string Comment { get; set; }

        public DateTime? DateReported { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateCommented { get; set; }
        [UIHint("Enum")]
        public TaskStatus TaskStatus { get; set; }


    }
}