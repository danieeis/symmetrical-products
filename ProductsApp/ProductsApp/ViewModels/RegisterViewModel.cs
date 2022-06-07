using System;
using System.Threading.Tasks;
using TinyMvvm;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProductsApp.ViewModels
{
	public class RegisterViewModel : ViewModelBase
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

		public RegisterViewModel()
		{
		}

		public IAsyncCommand RegisterCommand => new AsyncCommand(Register, (_) => IsBusy, allowsMultipleExecutions: false);

		Task Register()
		{
			return Task.CompletedTask;
		}
	}
}

