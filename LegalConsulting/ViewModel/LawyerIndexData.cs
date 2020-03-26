using LegalConsulting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalConsulting.ViewModel
{
    public class LawyerIndexData
    {

        public IEnumerable<Lawyer> Lawyers { get; set; }
        public IEnumerable<Case> Cases { get; set; }
        public IEnumerable<CaseDetail> CaseDetails { get; set; }
        public IEnumerable<OfficeLocation> officeLocations { get; set; }
    }
}