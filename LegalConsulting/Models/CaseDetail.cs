using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LegalConsulting.Models
{
    public class CaseDetail
    {
        public int ID { get; set; }

        public int ClientID { get; set; }
 

        public int CaseID { get; set; }

        public int LawyerID { get; set; }

        public float Hours { get; set; }


        public virtual Lawyer Lawyer { get; set; }
        public virtual Case Case { get; set; }
        public virtual Client Client { get; set; }


    }
}