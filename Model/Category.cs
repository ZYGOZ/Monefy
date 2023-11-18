using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Model
{
    public class Category
    {
        public double Costs { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return $"Name : {Name} Costs : {Costs}";
        }
    }
}
