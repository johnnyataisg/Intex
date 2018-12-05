using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("EmployeeUsers")]
    public class EmployeeUsers
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

