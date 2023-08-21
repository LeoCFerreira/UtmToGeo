using CoordinatorConversorLib.Models.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.Commands;

namespace WpfUI.ViewModels
{
    internal class MultipleConversionViewModel:ViewModelBase
    {
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set 
            { 
                _filePath = value;
            
                OnPropertyChanged(nameof(FilePath));
            }
        }

        private IEnumerable<IUtmPoint> _utmPoints = new List<IUtmPoint>();

        public IEnumerable<IUtmPoint> UtmPoints
        {
            get { return _utmPoints; }
            set 
            { 
                _utmPoints = value;
            
                OnPropertyChanged(nameof(UtmPointsText));
            }
        }

        private string _utmPointsText;

        public string UtmPointsText
        {
            get { return GetCoordinatesFromCollection(); }
            set { _utmPointsText = value; }
        }

        private string GetCoordinatesFromCollection()
        {
            var builder = new StringBuilder();

            foreach (var point in UtmPoints) 
            {
                builder.AppendLine(point.ToString());
            }

            return builder.ToString();
        }

        private int _zone = 23;

        public int Zone
        {
            get { return _zone; }
            set
            {
                _zone = value;

                OnPropertyChanged(nameof(Zone));
            }
        }

        private bool _isSouthHemisphere = true;

        public bool IsSouthHemisphere
        {
            get { return _isSouthHemisphere; }
            set
            {
                _isSouthHemisphere = value;

                OnPropertyChanged(nameof(IsSouthHemisphere));
            }
        }

        public ICommand? ConvertCommand { get; }

        public ICommand? OpenFileCommand { get; }

        public MultipleConversionViewModel()
        {
            ConvertCommand = new ConvertMultipleCoordinatesCommand(this);

            OpenFileCommand = new OpenFileCommand(this);
        }
    }
}
