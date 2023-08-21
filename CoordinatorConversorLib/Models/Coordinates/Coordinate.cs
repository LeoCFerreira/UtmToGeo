using CoordinatorConversorLib.Models.Points;
using CoordinatorConversorLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Coordinates
{
    public class Coordinate : ICoordinate
    {
        public Coordinate(IUtmPoint utmPoint)
        {
            UTM = utmPoint;
        }

        public Coordinate(IUtmPoint utm, int zone, bool isSouthHemisphere) : this(utm)
        {
            Zone = zone;

            IsSouthHemisphere = isSouthHemisphere;

            var conversor = new CoordinatorConversor();

            Geographic = conversor.ToGeographic(this);
        }

        public IUtmPoint UTM { get; set; }

        public IGeographicPoint Geographic { get; set; }

        public int Zone { get; set; }

        public bool IsSouthHemisphere { get; set; }

        public object Clone()
        {
            var output = new Coordinate(UTM);

            output.UTM = UTM.Clone() as IUtmPoint;

            output.Geographic = Geographic.Clone() as IGeographicPoint;

            output.Zone = Zone;

            output.IsSouthHemisphere = IsSouthHemisphere;

            return output;
        }
    }
}
