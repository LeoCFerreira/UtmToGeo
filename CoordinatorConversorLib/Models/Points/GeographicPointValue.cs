using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Models.Points
{
    public class GeographicPointValue : IGeographicPointValue
    {
        public GeographicPointValue(decimal value)
        {
            Value = value;

            CalculateDegreesMinutesSeconds();

        }

        private void CalculateDegreesMinutesSeconds()
        {
            Degrees = (int)Value;//Math.Truncate(Value);

            var degreesRemains = Math.Abs(Value - Degrees);

            degreesRemains *= 60;

            Minutes = (int)degreesRemains;

            var minutesRemains = degreesRemains - Minutes;

            Seconds = Math.Round(minutesRemains * 60,4) ;
        }

        public decimal Value { get; set; }
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public decimal Seconds { get; set; }

        public object Clone()
        {
            var result = new GeographicPointValue(Value);

            return result;
        }
        public override string ToString()
        {
            return $"{Degrees}°{Minutes}'{Seconds}''";
        }

    }
}
