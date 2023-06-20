using DevPace.Desktop.Helpers.Commands;
using DevPace.Desktop.Models.CustomerModels;
using DevPace.Desktop.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevPace.Desktop.ViewModels.CustomerViewModels
{
    public class CustomerEditViewModel : BaseViewModel
    {
        private readonly ICustomerService _customerService;
        private readonly CustomerModel _selectedCustomer;
        
        private string _name;
        private string _email;
        private string _companyName;
        private string _phoneNumber;

        private BaseViewModel _currentViewModel;
        private RelayCommand _getBackCommand;
        private RelayCommandAsync _editCustomerCommand;

        public ICommand GetBackCommand => _getBackCommand ?? (_getBackCommand = new RelayCommand(GetBack));
        public ICommand EditCustomerCommand => _editCustomerCommand ?? (_editCustomerCommand = new RelayCommandAsync(EditCustomerAsync));
        
        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetField(ref _currentViewModel, value); }
        }

        public CustomerEditViewModel(ICustomerService customerService, CustomerModel selectedCustomer)
        {
            _customerService = customerService;
            _selectedCustomer = selectedCustomer;

            Name = _selectedCustomer.Name;
            CompanyName = _selectedCustomer.CompanyName;
            Email = _selectedCustomer.Email;
            PhoneNumber = _selectedCustomer.PhoneNumber;
        }

        public MainViewModel MainViewModel { get; set; }


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

        private async Task EditCustomerAsync()
        {
            //TODO:Add model validation
            //TODO:Add error handling
            //TODO:Add notifications
            await _customerService.UpdateCustomerAsync(new()
            {
                Id = _selectedCustomer.Id,
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
