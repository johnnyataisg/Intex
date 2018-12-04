using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Assays")]
    public class Assays
    {
        [Key]
        public int AssayID { get; set; }
        public string AssayName { get; set; }
        public string EstimatedDuration { get; set; }
        public string Instructions { get;set; }
        [ForeignKey("TestTubes")]
        public int TestTubeNumber { get; set; }
        public virtual TestTubes testtubes { get; set; }
    }
}