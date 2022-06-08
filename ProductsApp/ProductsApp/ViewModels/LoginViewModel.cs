using System;
using System.Threading.Tasks;
using MonkeyCache;
using ProductsApp.Models;
using ProductsApp.Services;
using ProductsApp.Views;
using TinyMvvm;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials.Interfaces;

namespace ProductsApp.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		readonly IBarrel _barrel;
		readonly ISecureStorage _secureStorage;
		readonly MessageService _messageService;
		public string Username { get; set; }
		public string Password { get; set; }

		public LoginViewModel(IBarrel barrel, ISecureStorage secureStorage,
			MessageService messageService)
		{
			_messageService = messageService;
			_barrel = barrel;
			_secureStorage = secureStorage;
		}

		public IAsyncCommand LoginCommand => new AsyncCommand(Login, (_) => !IsBusy, allowsMultipleExecutions: false);

        async Task Login()
        {
			IsBusy = true;
			if(	   !string.IsNullOrEmpty(Username)
				&& !string.IsNullOrEmpty(Password)
				&& _barrel.Exists(Username))
            {
				var user = _barrel.Get<User>(Username);
				var passwordHash = EncryptionService.Encrypt(Username, Password);
				if (passwordHash == user.Password)
				{
					await _secureStorage.SetAsync("LoginToken", Guid.NewGuid().ToString());
					Navigation.SetRootView(nameof(ProductsView));
				}
				else
				{
					_messageService.DisplayMessage("Mensaje", "Credenciales inválidas", "Aceptar");
				}
			}
            else
            {
				_messageService.DisplayMessage("Mensaje", "Usuario inválido", "Aceptar");
			}

			IsBusy = false;
		}
    }
}

