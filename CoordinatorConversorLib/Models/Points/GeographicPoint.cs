using CoordinatorConversorLib.Models.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Points
{
    internal class GeographicPoint : IGeographicPoint
    {
        public IGeographicPointValue X { get; set; }
        public IGeographicPointValue Y { get; set; }

        public object Clone()
        {
            var result = new GeographicPoint();

            result.X = X.Clone() as IGeographicPointValue;

            result.Y = Y.Clone() as IGeographicPointValue;

            return result;
        }

        public override string ToString()
        {
            return $"{X};{Y}";

        }
    }
}
