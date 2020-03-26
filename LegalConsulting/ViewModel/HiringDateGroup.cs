using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LegalConsulting.ViewModel
{
    public class HiringDateGroup
    {

        [DataType(DataType.Date)]
        public DateTime? HiringDate { get; set; }
        public int LawyerCount{get;set;}
}
}