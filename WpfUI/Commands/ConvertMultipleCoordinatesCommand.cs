using CoordinatorConversorLib.Models.Coordinates;
using CoordinatorConversorLib.Models.Points;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Commands
{
    internal class ConvertMultipleCoordinatesCommand : CommandBase
    {
        public readonly MultipleConversionViewModel _viewModel;

        public ConvertMultipleCoordinatesCommand(MultipleConversionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                var result = GetGeographicPoints();

                WriteFile(result);

                MessageBox.Show("Pontos convertidos para GMT. O novo arquivo se encontra no mesmo diretório do .csv de entrada.",
                    "Conversão concluída com sucesso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private IEnumerable<IGeographicPoint> GetGeographicPoints()
        {
            foreach (var utmPoint in _viewModel.UtmPoints)
            {
                var zone = _viewModel.Zone;

                var isSouthHemisphere = _viewModel.IsSouthHemisphere;

                var coordinate = new Coordinate(utmPoint, zone, isSouthHemisphere);

                yield return coordinate.Geographic;
            }
        }
        private void WriteFile(IEnumerable<IGeographicPoint> gmtPoints)
        {
            var fileInfo = new FileInfo(_viewModel.FilePath);

            var path = fileInfo.Directory?.FullName ?? "";

            var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");

            fileName += " ";

            fileName += Guid.NewGuid()
                .ToString("N")
                .Substring(0,10);

            fileName += " GMT.csv";


            var newFileName = Path.Combine(path, fileName);

            using (var writer = new StreamWriter(newFileName))
            {
                foreach (var point in gmtPoints)
                {
                    var line = $"{point.X};{point.Y}";

                    writer.WriteLine(line);
                }
            }

            //System.Diagnostics.Process.Start(newFileName);
        }
    }
}
