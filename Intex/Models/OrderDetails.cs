using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex.models
{
    public class OrderDetails
    {
        public WorkOrders WorkOrder { get; set; }
        public List<Samples> SampleList { get; set; }
    }
}