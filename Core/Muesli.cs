﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore
{
    public class Muesli
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Basics { get; set; }
        public int Cereal { get; set; }
        public int Fruit { get; set; }
        public int Nuts { get; set; }
        public int Choco { get; set; }
        public int Specials { get; set; }
        public float Price { get; set; }
    }
}
