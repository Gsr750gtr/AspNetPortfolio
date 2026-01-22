using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManager
{
    using CustomerManager.Models;
    using CustomerManager.ViewModels;
    using CustomerManager.Views;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;

    public class CustomerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public CustomerModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate("MainRegion", nameof(Views.CustomerListView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // DI に View を登録
            containerRegistry.RegisterForNavigation<CustomerListView>();

            // HttpClientをシングルトンに
            containerRegistry.RegisterSingleton<CustomerService>();

           
        }
    }

}
