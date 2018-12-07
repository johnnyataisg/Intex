using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Customers")]
    public class Customers
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PaymentInfo { get; set; }
        public string TotalOrderVolume { get; set; }
        public bool? DiscountQualify { get; set; }
        [ForeignKey("customerusers")]
        public virtual string Username { get; set; }
        public virtual CustomerUsers customerusers { get; set; }
    }
}

