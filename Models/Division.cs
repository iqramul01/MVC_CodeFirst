using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class Division
    {
        public int DivisionID { get; set; }
        [DisplayName("Division")]
        public string DivisionName { get; set; }


        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}