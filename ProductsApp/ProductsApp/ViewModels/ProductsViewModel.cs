using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MonkeyCache;
using ProductsApp.Interfaces;
using ProductsApp.Models;
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

        ObservableCollection<Product> _products = new();
        public ObservableCollection<Product> Products
        {
            get => _products;
            set => Set(ref _products, value);
        }

        public ProductsViewModel(IProductFakeApi productFakeApi, IConnectivity connectivity, IBarrel barrel)
		{
            _barrel = barrel;
            _connectivity = connectivity;
			_productFakeApi = productFakeApi;
		}

        public async override Task Initialize()
        {
            await base.Initialize().ConfigureAwait(false);
            await LoadProducts().ConfigureAwait(false);
        }

        public IAsyncCommand RefreshCommand => new AsyncCommand(LoadProducts, (_) => !IsBusy, allowsMultipleExecutions: false);

        async Task LoadProducts()
        {
            IsBusy = true;
            if(_connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                var products = await _productFakeApi.Products();
                _barrel.Add(nameof(Product), products, TimeSpan.MaxValue);
            }

            if(_barrel.Exists(nameof(Product)))
                Products = new(_barrel.Get<List<Product>>(nameof(Product)));

            IsBusy = false;
        }
    }
}

