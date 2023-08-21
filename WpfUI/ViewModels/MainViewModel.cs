using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Store;

namespace WpfUI.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private bool isSingleConversion=true;

        public bool IsSingleConversion
        {
            get { return isSingleConversion; }
            set 
            {
                isSingleConversion = value;
            
                OnPropertyChanged(nameof(IsSingleConversion));

                if (isSingleConversion)
                {
                    _navigationStore.CurrentViewModel= new SingleConversionViewModel();
                }
            }
        }

        private bool isMultipleConversion;

        public bool IsMultipleConversion
        {
            get { return isMultipleConversion; }
            set 
            {
                isMultipleConversion = value;
            
                OnPropertyChanged(nameof(IsMultipleConversion));

                if (isMultipleConversion)
                {
                    _navigationStore.CurrentViewModel = new MultipleConversionViewModel();
                }
            }
        }




        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentModelChanged;
        }

        private void OnCurrentModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
