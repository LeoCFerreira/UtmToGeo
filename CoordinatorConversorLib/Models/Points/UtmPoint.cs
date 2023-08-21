using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Points
{
    public class UtmPoint : IUtmPoint
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }

        public object Clone()
        {
            var output = new UtmPoint();

            output.X = X;

            output.Y = Y;

            return output;
        }

        public override string ToString()
        {
            return $"Y:{Y.ToString("#,##0.000")}\tX:{X.ToString("#,##0.000")}";
        }
    }
}
