using CustomerManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CustomerManager.ViewModels
{
    public class CustomerListViewModel : BindableBase
    {
        public DelegateCommand RefreshCommand { get; }
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

        private bool _canRefresh = true;
        public bool CanRefresh
        {
            get => _canRefresh;
            set => SetProperty(ref _canRefresh, value);
        }

        private readonly CustomerService _service;

        public CustomerListViewModel()
        {
            RefreshCommand = new DelegateCommand(OnRefresh).ObservesCanExecute(() => CanRefresh);
            DeleteCommand = new DelegateCommand<CustomerInfo>((customerInfo => OnDelete(customerInfo)));

            _service = new CustomerService();
        }

        private void OnDelete(CustomerInfo customerInfo)
        {
            // TODO:選択行DB削除処理
        }

        private void OnRefresh()
        {
            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            CanRefresh = false;
            try
            {
                var datas = await _service.GetCustomersAsync();
                CustomerInfos = datas.Select(x => new CustomerInfo(x.Code, x.Name, x.NameKana, x.Prefecture));
            }
            catch (System.Exception e)
            {
                Debug.WriteLine($"Error LoadCustomers: {e.Message}");
            }

            CanRefresh = true;
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
