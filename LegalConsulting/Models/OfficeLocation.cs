using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LegalConsulting.Models
{
    public class OfficeLocation
    {
        [Key]
        [ForeignKey("Lawyer")]
        public int LawyerID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public virtual Lawyer Lawyer { get; set; }
    }
}