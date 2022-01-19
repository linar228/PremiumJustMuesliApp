using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore
{
    //Нет конструктора класса - можно лучше
    public class MuesliMix
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Price { get; set; }
    }
}
