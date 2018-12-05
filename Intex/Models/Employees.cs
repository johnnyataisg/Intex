using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Employees")]
    public class Employees
    {
        [Key]
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficeLocation { get; set; }
        [ForeignKey("employeeusers")]
        public virtual string Username { get; set; }
        public virtual EmployeeUsers employeeusers { get; set; }
    }
}

