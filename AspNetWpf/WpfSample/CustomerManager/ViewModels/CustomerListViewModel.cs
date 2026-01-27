using CustomerManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.ViewModels
{
    public class CustomerListViewModel : BindableBase
    {
        public DelegateCommand RefreshCommand { get; }
        public DelegateCommand InsertCommand { get; }
        public DelegateCommand<CustomerInfo> DeleteCommand { get; }

        private IEnumerable<CustomerInfo> _customerInfos;
        public IEnumerable<CustomerInfo> CustomerInfos
        {
            get => _customerInfos;
            set => SetProperty(ref _customerInfos, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterCustomers();
            }
        }

        private bool _isActive = true;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }

        private readonly CustomerService _customerService;
        private readonly IDialogService _dialogService;

        public CustomerListViewModel(CustomerService customerService, IDialogService dialogService)
        {
            RefreshCommand = new DelegateCommand(OnRefresh).ObservesCanExecute(() => IsActive);
            InsertCommand = new DelegateCommand(OnInsert).ObservesCanExecute(() => IsActive);
            DeleteCommand = new DelegateCommand<CustomerInfo>((customerInfo => OnDeleteAsync(customerInfo))).ObservesCanExecute(() => IsActive);

            _customerService = customerService;
            _dialogService = dialogService;
        }

        private void OnInsert()
        {
            _dialogService.ShowDialog(
                nameof(Views.InputCustomerView),
                null,
                result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        LoadCustomers();
                    }
                });
        }

        private void OnRefresh()
        {
            LoadCustomers();
        }

        private async void OnDeleteAsync(CustomerInfo customerInfo)
        {
            IsActive = false;
            try
            {
                await _customerService.DeleteAsync(customerInfo.Code);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"Error LoadCustomers: {e.Message}");
            }
            IsActive = true;

            // DB再読み込み
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            IsActive = false;
            try
            {
                var datas = await _customerService.GetAsync();
                CustomerInfos = datas.Select(x => new CustomerInfo(x.Code, x.Name, x.NameKana, x.Prefecture));
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"Error LoadCustomers: {e.Message}");
            }

            IsActive = true;
        }

        private void FilterCustomers()
        {
            CustomerInfos = CustomerInfos.Where(x =>
            x.Name.Contains(SearchText) ||
            x.NameKana.Contains(SearchText) ||
            x.Prefecture.Contains(SearchText));
        }
    }
}
