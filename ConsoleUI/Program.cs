using CoordinatorConversorLib;
using CoordinatorConversorLib.Models.Coordinates;
using CoordinatorConversorLib.Models.Points;
using CoordinatorConversorLib.Services;

namespace ConsoleUI
{
    public class Program
    { 
        public static async Task Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information
            //await CoordinatesConverter.ConvertAsync();

            var conversor = new CoordinatorConversor();

            var utm = new UtmPoint();
            utm.X = 226133.66m;
            utm.Y = 9388738.876m;
            Console.WriteLine($"Coordenadas UTM:\n{utm}");

            var coord = new Coordinate(utm);
            coord.UTM = utm;
            
            var geo = conversor.ToGeographic(coord);

            Console.WriteLine($"Coordenadas Geograficas:\n{geo}");
        }
    }
}
