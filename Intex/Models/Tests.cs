using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Tests")]
    public class Tests
    {
        [Key]
        public int TestID { get; set; }
        public string TestName { get; set; }
        public string Instructions { get; set; }
    }
}
