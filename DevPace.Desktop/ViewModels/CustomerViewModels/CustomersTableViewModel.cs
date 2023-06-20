using DevPace.Desktop.Helpers.Commands;
using DevPace.Desktop.Helpers.Constants;
using DevPace.Desktop.Models.CustomerModels;
using DevPace.Desktop.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DevPace.Desktop.ViewModels.CustomerViewModels
{
    public class CustomersTableViewModel : BaseViewModel
    {
        #region Dependencies

        private readonly ICustomerService _customerService;

        #endregion

        private int _rowCount = CustomersTableConstants.rowCount;
        private int _currentPage = CustomersTableConstants.firstPage;
        private string _filterString = string.Empty;
        private CustomerModel _selectedCustomer;
        private ObservableCollection<CustomerModel> _items;
        private BaseViewModel _currentViewModel;
        private bool _isFilterStringValid = true;

        #region Commands

        private RelayCommand _editCustomerCommand;
        private RelayCommand _addCustomerCommand;
        private RelayCommandAsync _deleteCustomerCommand;
        private RelayCommandAsync _loadNextItemsCommand;
        private RelayCommandAsync _loadPreviousItemsCommand;
        private RelayCommandAsync _reloadItemsCommand;
        private RelayCommandAsync _reloadCurrentPageCommand;

        public ICommand EditCustomerCommand => _editCustomerCommand ?? (_editCustomerCommand = new RelayCommand(EditCustomer, CanExecuteCustomer));
        public ICommand AddCustomerCommand => _addCustomerCommand ?? (_addCustomerCommand = new RelayCommand(AddCustomer, CanExecuteCustomer));
        public ICommand DeleteCustomerCommand => _deleteCustomerCommand ?? (_deleteCustomerCommand = new RelayCommandAsync(DeleteCustomerAsync));
        public ICommand LoadNextItemsCommand => _loadNextItemsCommand ?? (_loadNextItemsCommand = new RelayCommandAsync(LoadNextItemsAsync));
        public ICommand LoadPreviousItemsCommand => _loadPreviousItemsCommand ?? (_loadPreviousItemsCommand = new RelayCommandAsync(LoadPreviousItemsAsync));
        public ICommand ReloadItemsCommand => _reloadItemsCommand ?? (_reloadItemsCommand = new RelayCommandAsync(ReloadItemsAsync));
        public ICommand ReloadCurrentPageCommand => _reloadCurrentPageCommand ?? (_reloadCurrentPageCommand = new RelayCommandAsync(ReloadCurrentPageAsync));
        
        #endregion

        #region Properties

        public MainViewModel MainViewModel { get; set; }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (_filterString != value)
                {
                    _filterString = value;
                    OnPropertyChanged(nameof(FilterString));
                    ValidateFilterStringAsync();
                }
            }
        }

        public int CurrentPage
        {
            get { return _currentPage; }
            set { SetField(ref _currentPage, value); }
        }

        public CustomerModel SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { SetField(ref _selectedCustomer, value); }
        }

        public ObservableCollection<CustomerModel> Items
        {
            get { return _items; }
            set { SetField(ref _items, value); }
        }

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetField(ref _currentViewModel, value); }
        }

        public bool IsFilterStringValid
        {
            get { return _isFilterStringValid; }
            set { SetField(ref _isFilterStringValid, value); }
        }

        #endregion

        #region Constructor

        public CustomersTableViewModel(ICustomerService customerService)
        {
            _customerService = customerService;

            Items = new ObservableCollection<CustomerModel>();

            ReloadItemsCommand.Execute(null);
        }

        public void SwitchToCustomersTablePage()
        {
            MainViewModel.CurrentViewModel = this;
        }

        #endregion

        #region Private methods

        private async Task LoadNextItemsAsync()
        {
            var customers = await _customerService.GetCustomersAsync(CurrentPage + 1, _rowCount, FilterString, string.Empty, false);
            if (customers.Any())
            {
                Items.Clear();

                foreach (var customer in customers)
                    Items.Add(customer);

                CurrentPage++;
            }
        }

        private async Task LoadPreviousItemsAsync()
        {
            if (CurrentPage > 1)
            {
                var customers = await _customerService.GetCustomersAsync(CurrentPage - 1, _rowCount, FilterString, string.Empty, false);

                Items.Clear();

                foreach (var customer in customers)
                    Items.Add(customer);

                CurrentPage--;
            }
        }

        private async Task ReloadItemsAsync()
        {
            var firstPage = CustomersTableConstants.firstPage;

            var customers = await _customerService.GetCustomersAsync(firstPage, firstPage * _rowCount, FilterString, string.Empty, false);

            Items.Clear();

            foreach (var customer in customers)
                Items.Add(customer);
        }

        private async Task ReloadCurrentPageAsync()
        {
            var customers = await _customerService.GetCustomersAsync(CurrentPage, _currentPage * _rowCount, FilterString, string.Empty, false);
            if (customers.Any())
            {
                Items.Clear();

                foreach (var customer in customers)
                    Items.Add(customer);
            }
            else
            {
                await ReloadItemsAsync();
            }
        }

        private void EditCustomer(object parameter)
        {
            if (_selectedCustomer != null)
            {
                MainViewModel.CurrentViewModel = new CustomerEditViewModel(_customerService, SelectedCustomer);
            }
        }

        private void AddCustomer(object parameter)
        {

            MainViewModel.CurrentViewModel = new CustomerAddViewModel(_customerService);
        }

        private async Task DeleteCustomerAsync()
        {
            if (SelectedCustomer != null)
            {
                await _customerService.DeleteCustomerAsync(new() { Id = SelectedCustomer.Id });
                await ReloadCurrentPageAsync();
            }
        }

        private async Task ValidateFilterStringAsync()
       {
            if (!Regex.IsMatch(FilterString, CustomersTableConstants.regExp))
            {
                IsFilterStringValid = false;
            }
            else
            {
                IsFilterStringValid = true;
                await ReloadItemsAsync();
            }
        }

        private bool CanExecuteCustomer(object parameter)
        {
            return true;
        }
        
        #endregion
    }
}