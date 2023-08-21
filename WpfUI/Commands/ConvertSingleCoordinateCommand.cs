using CoordinatorConversorLib.Models.Coordinates;
using CoordinatorConversorLib.Models.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfUI.ViewModels;

namespace WpfUI.Commands
{
    public class ConvertSingleCoordinateCommand : CommandBase
    {
        private readonly SingleConversionViewModel _viewModel;

        public ConvertSingleCoordinateCommand(SingleConversionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                var utmPoint = new UtmPoint();

                utmPoint.X = _viewModel.X;

                utmPoint.Y = _viewModel.Y;

                var coordinate = new Coordinate(utmPoint, _viewModel.Zone, _viewModel.IsSouthHemisphere);

                _viewModel.Longitude = coordinate.Geographic.X.ToString();

                _viewModel.Latitude = coordinate.Geographic.Y.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
