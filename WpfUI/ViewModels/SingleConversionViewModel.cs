using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfUI.Commands;

namespace WpfUI.ViewModels
{
    public class SingleConversionViewModel:ViewModelBase
    {
        private decimal _x=215955.833m;

        public decimal X
        {
            get { return _x; }
            set 
            {
                _x = value;

                OnPropertyChanged(nameof(X));
            }
        }

        private decimal _y=9155312.745m;

        public decimal Y
        {
            get { return _y; }
            set 
            { 
                _y = value;

                OnPropertyChanged(nameof(Y));
            }
        }

        private string _latitude;

        public string Latitude
        {
            get { return _latitude; }
            set 
            { 
                _latitude = value;

                OnPropertyChanged(nameof(Latitude));
            }
        }

        private string _longitude;

        public string Longitude
        {
            get { return _longitude; }
            set 
            { 
                _longitude = value;

                OnPropertyChanged(nameof(Longitude));
            }
        }

        private int _zone=23;

        public int Zone
        {
            get { return _zone; }
            set 
            { 
                _zone = value;

                OnPropertyChanged(nameof(Zone));
            }
        }

        private bool _isSouthHemisphere=true;

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

        public SingleConversionViewModel()
        {
            ConvertCommand = new ConvertSingleCoordinateCommand(this);
        }



    }
}
