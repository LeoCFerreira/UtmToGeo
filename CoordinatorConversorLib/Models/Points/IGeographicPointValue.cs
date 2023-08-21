using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Points
{
    public interface IGeographicPointValue
    {
        decimal Value { get; set; }

        int Degrees { get; set; }

        int Minutes { get; set; }

        decimal Seconds { get; set; }

        object Clone();
    }
}
