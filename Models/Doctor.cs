using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1263087.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Doctor
    {
        [Key]
        public int DoctorID { get; set; }
        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Name is Required!")]
        [StringLength(30, ErrorMessage = "Name Must Be in 20 Character")]
        public string DoctorName { get; set; }
        [DisplayName("Contact Address")]
        [Required(ErrorMessage = "Contact Address is Required!")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Email is Required!")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailExists", "Doctor", ErrorMessage = "This Email already Exist, Please Enter another Email!!")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Phone No is Required!")]
        [DisplayName("Phone")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        [Compare("PhoneNumber", ErrorMessage = "Phone Number Doesn't Match")]
        [DisplayName("Confirm Phone No")]
        public int ConfirmPhoneNumber { get; set; }
        [DisplayName("Joining Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public decimal Commission { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal TotalBill 
        { 
            get { return (Salary + Commission); }
            private set { } 
        }
        public string DoctorImage { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
        //[NotMapped]
        //[Compare("PhoneNumber", ErrorMessage = "Phone Number Doesn't Match")]
        //[DisplayName("Confirm Phone No")]
        //public int ConfirmPhoneNumber { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}