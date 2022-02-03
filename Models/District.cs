using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class District
    {
        public int DistrictID { get; set; }
        [DisplayName("District")]
        public string DistrictName { get; set; }

        public int DivisionID { get; set; }
        public virtual Division Division { get; set; }

        public virtual ICollection<Upazila> Upazilas { get; set; }
    }
}