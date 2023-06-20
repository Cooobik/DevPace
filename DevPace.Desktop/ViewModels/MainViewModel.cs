using DevPace.Desktop.Services.Interfaces;
using DevPace.Desktop.ViewModels.CustomerViewModels;
using System.Collections.Generic;

namespace DevPace.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ICustomerService _customerService;
        private BaseViewModel _currentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetField(ref _currentViewModel, value); }
        }

        public MainViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            var customersTableViewModel = new CustomersTableViewModel(_customerService)
            {
                MainViewModel = this
            };

            CurrentViewModel = customersTableViewModel;
        }

    }
}
