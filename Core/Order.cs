using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore
{
    //Нет конструктора класса - можно лучше
    public class Order
    {
        public int ID { get; set; }
        public int MixID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
    }
}
