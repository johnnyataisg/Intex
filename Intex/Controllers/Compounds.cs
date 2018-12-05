using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Compounds")]
    public class Compounds
    {
        [Key]
        public int CompoundID { get; set; }
        public string CompoundName { get; set; }
        public double MaxTolerateDose { get; set; }
    }
}

