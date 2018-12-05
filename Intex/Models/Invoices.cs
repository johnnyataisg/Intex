using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("Invoices")]
    public class Invoices
    {
        [Key]
        public int InvoiceNumber { get; set; }
        public DateTime DateDue { get; set; }
        public DateTime EarlyDate { get; set; }
        public double AmountDue { get; set; }
        public double Discount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime DateSent { get; set; }
        [ForeignKey("workorders")]
        public int LTNumber { get; set; }
        public virtual WorkOrders workorders { get; set; }
    }
}

