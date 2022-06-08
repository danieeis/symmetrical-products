using System;
using System.Threading.Tasks;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProductsApp.Services
{
	public class MessageService
	{
		Page MainPage => Application.Current.MainPage;

		readonly IMainThread _mainThread;
        public MessageService(IMainThread mainThread)
        {
			_mainThread = mainThread;
        }

		public void DisplayMessage(string title, string message, string buttonText) => _mainThread.BeginInvokeOnMainThread(async() => await MainPage.DisplayAlert(title, message, buttonText));
	}
}

