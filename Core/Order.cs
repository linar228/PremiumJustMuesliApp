using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
    }
}
