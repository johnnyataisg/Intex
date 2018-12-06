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
        public string TestTubeID { get; set; }
        public int TestTubeNumber { get; set; }
        public double Concentration { get; set; }
        public bool IsRequired { get; set; }
        [ForeignKey("sample")]
        public virtual string SampleID { get; set; }
        public virtual Samples sample { get; set; }
        [ForeignKey("test")]
        public virtual int? TestID { get; set; }
        public virtual Tests test { get; set; }
    }
}

