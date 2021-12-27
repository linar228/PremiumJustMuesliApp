using System;
using System.Collections.Generic;
using System.Text;

namespace MuesliCore.ViewModels
{
    public class MixModel
    {
        public int MixId { get; set; }
        public string Name { get; set; }
        public int[] Ingredients { get; set; } = new int[6];
        public List<Type> Types { get; set; } = DBConnect.GetTypes();    

    }
}
