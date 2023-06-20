using DevPace.Desktop.Helpers.Commands;
using DevPace.Desktop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevPace.Desktop.ViewModels.CustomerViewModels
{
    public class CustomerAddViewModel : BaseViewModel
    {
        private readonly ICustomerService _customerService;
        
        private string _name = string.Empty;
        private string _email = string.Empty;
        private string _companyName = string.Empty;
        private string _phoneNumber = string.Empty;

        private BaseViewModel _currentViewModel;
        private RelayCommand _getBackCommand;
        private RelayCommandAsync _addCustomerCommand;

        public ICommand GetBackCommand => _getBackCommand ?? (_getBackCommand = new RelayCommand(GetBack));
        public ICommand AddCustomerCommand => _addCustomerCommand ?? (_addCustomerCommand = new RelayCommandAsync(AddCustomerAsync));

        public CustomerAddViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

        }
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetField(ref _currentViewModel, value); }
        }

        [Required]
        public string Name
        {
            get { return _name; }
            set { SetField(ref _name, value); }
        }

        [Required]
        public string CompanyName
        {
            get { return _companyName; }
            set { SetField(ref _companyName, value); }
        }

        [Required]
        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set { SetField(ref _email, value); }
        }

        [Required]
        [Phone]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetField(ref _phoneNumber, value); }
        }

        private async Task AddCustomerAsync()
        {
            //TODO:Add model validation
            //TODO:Add error handling
            //TODO:Add notifications
            await _customerService.CreateCustomerAsync(new()
            {
                Name = Name,
                CompanyName = CompanyName,
                Email = Email,
                PhoneNumber = PhoneNumber,
            });
        }

        private void GetBack(object parameter)
        {
            //TODO:Add route
        }

    }
    
}
