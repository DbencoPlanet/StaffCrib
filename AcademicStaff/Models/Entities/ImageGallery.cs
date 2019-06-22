using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicStaff.Models.Entities
{
    public class ImageGallery
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        [Display(Name = "Gallery Alt")]
        public string GalleryAlt { get; set; }
        [Display(Name = "Current Gallery")]
        public bool CurrentGallery { get; set; }
       
    }
}