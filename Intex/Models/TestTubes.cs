using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("TestTubes")]
    public class TestTubes
    {
        [Key]
        public int TestTubeNumber { get; set; }
        public double Concentration { get; set; }
        public bool IsRequired { get; set; }
        [ForeignKey("test")]
        public int TestID { get; set; }
        public virtual Tests test { get; set; }
    }
}

