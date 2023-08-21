using CoordinatorConversorLib.Models.Coordinates;
using CoordinatorConversorLib.Models.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorConversorLib.Services
{
    public class CoordinatorConversor
    {
        private const decimal _utmScaleFactor = 0.9996m;
        private const double sm_a = 6378137d;
        private const double sm_b = 6356752.314d;
        private const double sm_EccSquared = .00669437999013d;

        
        public IGeographicPoint ToGeographic(ICoordinate coordinate)
        {
            //Validar x, y, e zona
            
            if (IsCoordinateInvalid(coordinate))
            {
                throw new ArgumentException("Zona inválida! Deve estar entre 1 e 60.");
            }

            var result = UTMToGeographic(coordinate);

            return result;
        }
        private IGeographicPoint UTMToGeographic(ICoordinate coordinate)
        {
            var x = coordinate.UTM.X;

            x -= 500000m;

            x /= _utmScaleFactor;

            var y = coordinate.UTM.Y;
            
            if (coordinate.IsSouthHemisphere)
            {
                y -= 10000000m;
            }

            y /= _utmScaleFactor;

            var centralMeridian = DegreesToRadians(-183 + coordinate.Zone * 6);

            var geographicPoint_Radians = MapUtmToGeo((double)x, (double)y, (double)centralMeridian);

            var geographicPoint = new GeographicPoint();

            var degreesX = RadiansToDegrees(geographicPoint_Radians.X);

            geographicPoint.X= new GeographicPointValue(degreesX);

            var degreesY = RadiansToDegrees(geographicPoint_Radians.Y);

            geographicPoint.Y = new GeographicPointValue(degreesY);

            return geographicPoint;
        }
        private IUtmPoint MapUtmToGeo(double x, double y, double centralMeridian)
        {
            var phif = FootpointLatitude((double)y);

            var ep2 = (Math.Pow(sm_a, 2) - Math.Pow(sm_b, 2))/Math.Pow(sm_b, 2);

            var cf = Math.Cos(phif);

            var nuf2 = ep2 * Math.Pow(cf, 2);

            var Nf = Math.Pow(sm_a, 2) / (sm_b * Math.Sqrt(1 + nuf2));

            var Nfpow = Nf;

            var tf = Math.Tan(phif);

            var tf2 = tf * tf;

            var tf4 = tf2 * tf2;

            var x1frac = 1 / (Nfpow * cf);

            Nfpow *= Nf;   /* now equals Nf**2) */
            var x2frac = tf / (2 * Nfpow);

            Nfpow *= Nf;   /* now equals Nf**3) */
            var x3frac = 1 / (6 * Nfpow * cf);

            Nfpow *= Nf;   /* now equals Nf**4) */
            var x4frac = tf / (24 * Nfpow);

            Nfpow *= Nf;   /* now equals Nf**5) */
            var x5frac = 1 / (120 * Nfpow * cf);

            Nfpow *= Nf;   /* now equals Nf**6) */
            var x6frac = tf / (720 * Nfpow);

            Nfpow *= Nf;   /* now equals Nf**7) */
            var x7frac = 1 / (5040 * Nfpow * cf);

            Nfpow *= Nf;   /* now equals Nf**8) */
            var x8frac = tf / (40320 * Nfpow);

            var x2poly = -1 - nuf2;

            var x3poly = -1 - 2 * tf2 - nuf2;

            var x4poly = 5 + 3 * tf2 + 6 * nuf2 - 6 * tf2 * nuf2 - 3 * (nuf2 * nuf2) - 9 * tf2 * (nuf2 * nuf2);

            var x5poly = 5 + 28 * tf2 + 24 * tf4 + 6 * nuf2 + 8 * tf2 * nuf2;

            var x6poly = -61 - 90 * tf2 - 45 * tf4 - 107 * nuf2 + 162 * tf2 * nuf2;

            var x7poly = -61 - 662 * tf2 - 1320 * tf4 - 720 * (tf4 * tf2);

            var x8poly = 1385 + 3633 * tf2 + 4095 * tf4 + 1575 * (tf4 * tf2);

            var output = new UtmPoint();

            var latitude = phif + x2frac * x2poly * Math.Pow(x, 2)
            + x4frac * x4poly * Math.Pow(x, 4)
            + x6frac * x6poly * Math.Pow(x, 6)
            + x8frac * x8poly * Math.Pow(x, 8);

            output.Y = (decimal)latitude;

            var longitude = centralMeridian + x1frac * x
            + x3frac * x3poly * Math.Pow(x, 3)
            + x5frac * x5poly * Math.Pow(x, 5)
            + x7frac * x7poly * Math.Pow(x, 7);

            output.X = (decimal)longitude;

            return output;
        }
        private double FootpointLatitude(double y)
        {
            var n = (double)((sm_a - sm_b) / (sm_a + sm_b));

            var alpha_ = ((sm_a + sm_b) / 2) * (1 + (Math.Pow(n, 2) / 4) + (Math.Pow(n, 4) / 64));

            var y_ = y / alpha_;

            var beta_ = (3 * n / 2) + (-27 * Math.Pow(n, 3.0) / 32.0) + (269.0 * Math.Pow(n, 5.0) / 512);

            var gamma_ = (21 * Math.Pow(n, 2) / 16) + (-55 * Math.Pow(n, 4) / 32);

            var delta_ = (151 * Math.Pow(n, 3) / 96) + (-417 * Math.Pow(n, 5) / 128);

            var epsilon_ = (1097 * Math.Pow(n, 4) / 512);

            var result = y_ + (beta_ * Math.Sin(2 * y_))
            + (gamma_ * Math.Sin(4 * y_))
            + (delta_ * Math.Sin(6 * y_))
            + (epsilon_ * Math.Sin(8 * y_));

            return result;
        }
        private decimal DegreesToRadians(decimal degrees)
        {
            var output = (degrees / 180) * (decimal)Math.PI;

            return output;
        }
        private decimal RadiansToDegrees(decimal radians)
        {
            var output = (radians / ((decimal)Math.PI)) * 180;

            return output;
        }

        private bool IsCoordinateInvalid(ICoordinate coordinate)
        {
            bool isZoneValid=true;

            if(coordinate.Zone < 1 || coordinate.Zone > 60)
            {
                isZoneValid=false;
            }

            return ! isZoneValid;
        }

        

    }
}
