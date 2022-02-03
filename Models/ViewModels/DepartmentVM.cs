using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1263087.Models.ViewModels
{
    public class DepartmentVM
    {
        public int DepartmentID { get; set; }
        [DisplayName("Department Name")]
        [Required(ErrorMessage = "Department Name is Required")]
        public string DepartmentName { get; set; }
        [DisplayName("Available Seat")]
        [Range(10, 500, ErrorMessage = "Seat Number Must be in 10 to 500")]
        public int AvailableSeat { get; set; }
        [DisplayName("Status")]
        public bool IsActive { get; set; }
    }
}