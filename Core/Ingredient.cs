﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore
{
    //Нет конструктора класса - можно лучше
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int TypeId { get; set; }
    }
}
