using CoordinatorConversorLib.Models.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Coordinates
{
    public interface ICoordinate
    {
        IUtmPoint UTM { get; set; }

        IGeographicPoint Geographic { get; set; }

        int Zone { get; }

        bool IsSouthHemisphere { get; }

        object Clone();
    }
}
