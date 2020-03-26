using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LegalConsulting.Models
{
    public class Client
    {
        public int ID { get; set; }
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string JobTitle { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public virtual ICollection<CaseDetail> CaseDetails { get; set; }

    }
}