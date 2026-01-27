using CustomerManager.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SharedDTOs.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManager.ViewModels
{
    public class InputCustomerViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand OKCommand { get; }
        public DelegateCommand CancelCommand { get; }

        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                SetProperty(ref _code, value);
                RaisePropertyChanged(nameof(CanOK));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                RaisePropertyChanged(nameof(CanOK));
            }
        }

        private string _nameKana;
        public string NameKana
        {
            get => _nameKana;
            set
            {
                SetProperty(ref _nameKana, value);
                RaisePropertyChanged(nameof(CanOK));
            }
        }

        private string _prefecture;
        public string Prefecture
        {
            get => _prefecture;
            set => SetProperty(ref _prefecture, value);
        }

        public bool CanOK => !string.IsNullOrEmpty(Code) &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(NameKana);

        private readonly CustomerService _customerService;

        public InputCustomerViewModel(CustomerService customerService)
        {
            _customerService = customerService;

            OKCommand = new DelegateCommand(OnOkAsync).ObservesCanExecute(() => CanOK); ;
            CancelCommand = new DelegateCommand(OnCancel);
        }

        private async void OnOkAsync()
        {
            try
            {
                await _customerService.InsertAsync(new CustomerDto
                { 
                    Code = Code,
                    Name = Name,
                    NameKana = NameKana,
                    Prefecture = Prefecture 
                });
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            catch (Exception e)
            {
                Debug.WriteLine($"InputCustomer Error: {e.Message}");
            }
        }

        private void OnCancel()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
        }

        public string Title => "取引先入力";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
