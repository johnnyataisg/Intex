using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Samples")]
    public class Samples
    {
        [Key]
        public int SequenceNumber { get; set; }
        public double Quantity { get; set; }
        public string Appearance { get; set; }
        public double IndicatedWeight { get; set; }
        public double ActualWeight { get; set; }
        public double MolecularMass { get; set; }
        [ForeignKey("assay")]
        public int AssayID { get; set; }
        public virtual Assays assay { get; set; }
        [ForeignKey("workorders")]
        public int LTNumber { get; set; }
        public virtual WorkOrders workorders { get; set; }
    }
}

