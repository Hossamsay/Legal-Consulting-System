using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LegalConsulting.Models
{
    public class Lawyer
    {
        public int LawyerID { get; set; }
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }

    


        [Display(Name = "Hiring Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? HiringDate { get; set; }
        [DataType(DataType.Currency)]
        public float PricePerHour { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        public virtual ICollection<Case> Cases { get; set; }
        public virtual OfficeLocation OfficeLocation { get; set; }


    }
}