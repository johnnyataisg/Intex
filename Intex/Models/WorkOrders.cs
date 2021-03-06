﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Intex.models
{
    [Table("WorkOrders")]
    public class WorkOrders
    {
        [Key]
        public int LTNumber { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public DateTime? DateDue { get; set; }
        public string Comments { get; set; }
        public bool? HasDiscount { get; set; }
        [ForeignKey("customers")]
        public virtual int? CustomerID { get; set; }
        public virtual Customers customers { get; set; }
        [ForeignKey("compounds")]
        public virtual int? CompoundID { get; set; }
        public virtual Compounds compounds { get; set; }
    }
}

