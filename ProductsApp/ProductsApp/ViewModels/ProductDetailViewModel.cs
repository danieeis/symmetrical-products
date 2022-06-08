using System;
using System.Threading.Tasks;
using ProductsApp.Models;
using TinyMvvm;

namespace ProductsApp.ViewModels
{
    public class ProductDetailViewModel : ViewModelBase
    {
        Product _product;
        public Product Product
        {
            get => _product;
            set => Set(ref _product, value);
        }

        public ProductDetailViewModel()
		{
		}

        public async override Task Initialize()
        {
            await base.Initialize().ConfigureAwait(false);

            if(NavigationParameter is Product product)
            {
                Product = product;
            }
        }
    }
}

