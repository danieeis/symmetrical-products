using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsApp.Models;
using Refit;

namespace ProductsApp.Interfaces
{
	public interface IProductFakeApi
	{
		[Get("https://fakestoreapi.com/products")]
		Task<IEnumerable<Product>> Products();
	}
}

