using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class BloodGroup
    {
        public int BloodGroupID { get; set; }
        [DisplayName("Blood Group")]
        public string BloodGroupName { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}