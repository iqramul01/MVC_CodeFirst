using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class Upazila
    {
        public int UpazilaID { get; set; }
        [DisplayName("Upazila")]

        public string UpazilaName { get; set; }


        public int DistrictID { get; set; }
        public virtual District District { get; set; }
    }
}