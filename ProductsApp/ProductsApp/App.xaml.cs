using System;
using ProductsApp.Views;
using TinyMvvm;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsApp
{
    public partial class App : Application
    {
        readonly ISecureStorage _secureStorage;
        readonly INavigationHelper _navigationHelper;
        public App (INavigationHelper navigationHelper, ISecureStorage secureStorage)
        {
            _navigationHelper = navigationHelper;
            _secureStorage = secureStorage;
            InitializeComponent();
        }

        protected async override void OnStart ()
        {
            var token = await _secureStorage.GetAsync("LoginToken");
            if (!string.IsNullOrEmpty(token))
            {
                _navigationHelper.SetRootView(nameof(ProductsView));
            }
            else
            {
                _navigationHelper.SetRootView(nameof(LoginView));
            }
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

