using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicStaff.Models.Entities
{
    public enum Status
    {
        [Description("None")]
        None = 0,

        [Description("Active")]
        Active = 1,

        [Description("Inactive")]
        Inactive = 2,

       
    }


    public enum TaskStatus
    {
        [Description("Pending")]
        Pending = 1,

        [Description("Active")]
        Active = 2,

        [Description("In progress")]
        [Display(Name = "In progress")]
        Inprogress = 3,

        [Description("Completed")]
        Completed = 4
    }



    public enum DocType
    {
        [Description("Official")]
        Official = 1,

        [Description("Personal")]
        Personal = 2



    }

    public enum DocType2
    {
        [Description("Send And Receive")]
        [Display(Name = "Send And Receive")]
        SendandReceive = 1


    }


    public enum StaffRank
    {
        [Description("None")]
        [Display(Name = "None")]
        NONE = 0,

        [Description("Professor")]
        [Display(Name = "Professor")]
        Professor = 1,

        [Description("Reader")]
        [Display(Name = "Reader")]
        Reader = 2,

        [Description("Snr Lecturer")]
        [Display(Name = "Snr Lecturer")]
        SnrLecturer = 3,

        [Description("Lecturer I")]
        [Display(Name = "Lecturer I")]
        LecturerI = 4,


        [Description("Lecturer II")]
        [Display(Name = "Lecturer II")]
        LecturerII = 5,

        [Description("Asst Lecturer")]
        [Display(Name = "Asst Lecturer")]
        AsstLecturer = 6,


    }


    public enum MaritalStatus
    {
        [Description("None")]
        NONE = 0,

        [Description("Single")]
        Single = 1,

        [Description("Married")]
        Married = 2,


    }


    public enum Gender
    {
        [Description("None")]
        NONE = 0,

        [Description("Male")]
        Male = 1,

        [Description("Female")]
        Female = 2,


    }
}