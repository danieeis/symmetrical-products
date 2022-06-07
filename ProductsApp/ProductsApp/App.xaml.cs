using System;
using ProductsApp.Views;
using TinyMvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsApp
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

