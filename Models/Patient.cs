using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }
        
        [Required(ErrorMessage = "Name is Required!")]
        [DisplayName("Patient Name")]
        public string PatientName { get; set; }
        
        [Required(ErrorMessage = "Emergency Contact is Required!")]
        [DisplayName("Emergency Contact")]
        public int EmergencyContact { get; set; }
        [DisplayName("Blood Group")]
        public int? BloodGroupID { get; set; }
        public virtual BloodGroup BloodGroup { get; set; }
        [DisplayName("Division Name")]
        public int? DivisionID { get; set; }
        public virtual Division Division { get; set; }
        [DisplayName("District Name")]
        public int? DistrictID { get; set; }
        public virtual District District { get; set; }
        [DisplayName("Upazila Name")]
        public int? UpazilaID { get; set; }
        public virtual Upazila Upazila { get; set; }
        [DisplayName("Department Name")]
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        [DisplayName("Doctor Name")]
        public int? DoctorID { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Admit> Admit { get; set; }
    }
}