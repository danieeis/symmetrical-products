using System;
using System.Threading.Tasks;
using TinyMvvm;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProductsApp.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
        public string Username { get; set; }
		public string Password { get; set; }

		public LoginViewModel()
		{
		}

		public IAsyncCommand LoginCommand => new AsyncCommand(Login, (_) => IsBusy, allowsMultipleExecutions: false);

        Task Login()
        {
			return Task.CompletedTask;
        }
    }
}

