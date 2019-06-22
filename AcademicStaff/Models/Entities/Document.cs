using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicStaff.Models.Entities
{
    public class Document
    {
        public int Id { get; set; }
        //[Required]
        public string DocName { get; set; }
        [NotMapped]
        public HttpPostedFileBase DocFile { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int? SenderId { get; set; }
        public Profile Sender { get; set; }
        public int? ReceiverId { get; set; }
        public Profile Receiver { get; set; }
        //[Required]
        [AllowHtml]
        public string Remark { get; set; }
        public string DocUrl { get; set; }
        public DocType DocType { get; set; }
        public DocType2 DocType2 { get; set; }
        public DateTime? DateUploaded { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateReceive { get; set; }
        [AllowHtml]
        public string Description { get; set; }


    }
}