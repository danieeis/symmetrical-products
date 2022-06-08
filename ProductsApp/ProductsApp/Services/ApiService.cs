using System;
using System.Net.Http;
using Fusillade;

namespace ProductsApp.Services
{
	public abstract class ApiService
	{
        public static HttpClient UserInitiated
        {
            get
            {
                return new Lazy<HttpClient>(() => CreateHttpClient(
              new RateLimitedHttpMessageHandler(new HttpClientHandler(), Priority.UserInitiated))).Value;
            }
        }

        static HttpClient CreateHttpClient(in HttpMessageHandler httpMessageHandler)
        {
            HttpClient client;
            client = new HttpClient(httpMessageHandler);

            client.BaseAddress = new Uri("https://fakestoreapi.com");
            client.Timeout = TimeSpan.FromSeconds(9999);
            return client;
        }
    }
}

