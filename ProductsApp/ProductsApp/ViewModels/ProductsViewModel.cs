using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MonkeyCache;
using ProductsApp.Interfaces;
using ProductsApp.Models;
using ProductsApp.Views;
using TinyMvvm;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials.Interfaces;

namespace ProductsApp.ViewModels
{
    public class ProductsViewModel : ViewModelBase
    {
        readonly IProductFakeApi _productFakeApi;
        readonly IConnectivity _connectivity;
        readonly IBarrel _barrel;
        readonly INavigationHelper _navigationHelper;
        readonly ISecureStorage _secureStorage;

        bool _isRefreshing = true;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => Set(ref _isRefreshing, value);
        }

        ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        public ProductsViewModel(IProductFakeApi productFakeApi,
            IConnectivity connectivity,
            IBarrel barrel,
            INavigationHelper navigationHelper,
            ISecureStorage secureStorage)
		{
            _secureStorage = secureStorage;
            _barrel = barrel;
            _connectivity = connectivity;
			_productFakeApi = productFakeApi;
            _navigationHelper = navigationHelper;
        }

        public async override Task Initialize()
        {
            await base.Initialize().ConfigureAwait(false);
            await LoadProductsCommand.ExecuteAsync().ConfigureAwait(false);
        }

        public IAsyncCommand LoadProductsCommand => new AsyncCommand(LoadProducts, (_) => !IsBusy, allowsMultipleExecutions: false);
        public IAsyncCommand LogoutCommand => new AsyncCommand(Logout, (_) => !IsBusy, allowsMultipleExecutions: false);
        public IAsyncCommand<Product> ProductDetailCommand => new AsyncCommand<Product>(OpenDetail, (_) => !IsBusy, allowsMultipleExecutions: false);

        async Task LoadProducts()
        {
            IsBusy = IsRefreshing = true;
            
            if(_connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                var products = await _productFakeApi.Products();
                _barrel.Add(nameof(Product), products, TimeSpan.MaxValue);
            }

            if(_barrel.Exists(nameof(Product)))
                Products = new(_barrel.Get<List<Product>>(nameof(Product)));

            IsBusy = IsRefreshing = false;
        }

        async Task OpenDetail(Product product)
        {
            IsBusy = true;
            await Navigation.NavigateToAsync(nameof(ProductDetailView), product);
            IsBusy = false;
        }

        Task Logout()
        {
            _secureStorage.RemoveAll();
            _navigationHelper.SetRootView(nameof(LoginView));
            
            return Task.CompletedTask;
        }
    }
}

