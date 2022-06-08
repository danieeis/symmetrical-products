using System;
using System.Threading.Tasks;
using MonkeyCache;
using ProductsApp.Models;
using ProductsApp.Services;
using ProductsApp.Views;
using TinyMvvm;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProductsApp.ViewModels
{
	public class RegisterViewModel : ViewModelBase
	{
		readonly IBarrel _barrel;
		readonly MessageService _messageService;

		public string Username { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

		public RegisterViewModel(IBarrel barrel, MessageService messageService)
		{
			_messageService = messageService;
			_barrel = barrel;
		}

		public IAsyncCommand RegisterCommand => new AsyncCommand(Register, (_) => !IsBusy, allowsMultipleExecutions: false);

		async Task Register()
		{
			IsBusy = true;
			if(	   !string.IsNullOrEmpty(Username)
				&& !string.IsNullOrEmpty(Password)
				&& !string.IsNullOrEmpty(ConfirmPassword)
				&& Password == ConfirmPassword
				&& !_barrel.Exists(Username))
			{
				var passwordEncrypt = EncryptionService.Encrypt(Username, Password);
				var user = new User(Guid.NewGuid().ToString(), Username, passwordEncrypt);
				_barrel.Add(Username, user, TimeSpan.MaxValue);
				await Navigation.BackAsync();
            }
            else
            {
				_messageService.DisplayMessage("Mensaje", "Datos no válidos", "Aceptar");
			}
			IsBusy = false;
		}
	}
}

