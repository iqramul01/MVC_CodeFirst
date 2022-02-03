using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class Staff
    {
        public int StaffID { get; set; }

        [DisplayName("Staff Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, ErrorMessage = "Name must be in 30 Charcter!!")]
        public string StaffName { get; set; }


        [DisplayName("Contact Address")]
        [Required(ErrorMessage = "Contact Address is Required")]
        public string ContactAddress { get; set; }

        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Phone Number")]

        //[Required(ErrorMessage = "Phone Number is Required")]
        //[Remote("IsPhoneNumberExist", "Staff", ErrorMessage = "This Phone Number already Exist, Please Enter another Phone Number!!")]
        public int PhoneNumber { get; set; }
        [DisplayName("Department Name")]
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }
    }
}