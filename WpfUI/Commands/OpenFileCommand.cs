using CoordinatorConversorLib.Models.Coordinates;
using CoordinatorConversorLib.Models.Points;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfUI.ViewModels;
using WpfUI.Views;

namespace WpfUI.Commands
{
    internal class OpenFileCommand : CommandBase
    {
        private readonly MultipleConversionViewModel _viewModel;

        public OpenFileCommand(MultipleConversionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object? parameter)
        {
            try
            {
                //check if the file exists
                var filePath = GetFileName(_viewModel.FilePath);

                _viewModel.FilePath = filePath;

                //read file and get IEnumerable coordinates
                _viewModel.UtmPoints = GetCoordinates(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,
                    "Erro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private IEnumerable<IUtmPoint> GetCoordinates(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    
                    var values = line.Split(';');

                    var north = decimal.Parse(values[0]);

                    var east = decimal.Parse(values[1]);

                    var utmPoint = new UtmPoint()
                    {
                        X = east,

                        Y = north
                    };

                    yield return utmPoint;
                }
            }
        }

        private string GetFileName(string filePath)
        {
            var openDialog = new OpenFileDialog();
            
            openDialog.Filter = "Arquivos em lista *.csv|*.csv";

            openDialog.Multiselect = false;
            
            openDialog.CheckFileExists = true;
            
            openDialog.ShowDialog();

            return openDialog.FileName;
        }
    }
}
