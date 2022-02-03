using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _1263087.Models
{
    public class Admit
    {
        [Key]
        public int AdmitID { get; set; }
        [DisplayName("Admit Fee")]
        public decimal AdmitFee { get; set; }
        [DisplayName("Total Bill")]
        public decimal TotalBill { get; set; }
        public decimal Discount { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal NetBill
        {
            get { return (TotalBill-(TotalBill* Discount)); }
            private set { }
        }
        [DisplayName("Patient Name")]
        public int? PatientID { get; set; }
        public virtual Patient Patient { get; set; }



    }
}