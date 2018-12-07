using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.models
{
    public class NumberInput
    {
        public int Input { get; set; }
        public virtual WorkOrders wo { get; set; }
    }
}