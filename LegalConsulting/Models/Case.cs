using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LegalConsulting.Models
{
    public enum Type
    {
        Criminal,robbery,Marriage,General
    }
    public class Case
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CaseID { get; set; }

        public int ClientID { get; set; }
        public string CaseName { get; set; }
        public Type Type { get; set; }
        [DisplayFormat(NullDisplayText = "None")]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }


        public virtual ICollection<Lawyer> Lawyers { get; set; }
        public virtual ICollection<CaseDetail> CaseDetails { get; set; }


    }
}