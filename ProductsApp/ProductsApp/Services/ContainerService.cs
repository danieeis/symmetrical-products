using System;
using System.IO;
using System.Reflection;
using Autofac;
using MonkeyCache;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using ProductsApp.Interfaces;
using Refit;
using TinyMvvm;
using TinyMvvm.Autofac;
using TinyMvvm.Forms;
using TinyMvvm.IoC;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ProductsApp.Services
{
	public static class ContainerService
	{
        readonly static Lazy<IContainer> _containerHolder = new(CreateContainer);

        public static IContainer Container => _containerHolder.Value;

        static IContainer CreateContainer()
        {
            var navigationHelper = new ShellNavigationHelper();

            var currentAssembly = Assembly.GetExecutingAssembly();
            navigationHelper.RegisterViewsInAssembly(currentAssembly);
            var containerBuilder = new ContainerBuilder();

            string barrelPath = Device.RuntimePlatform switch
            {
                Device.iOS => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "..", "Library", "Databases"),
                Device.Android => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "databases"),
                _ => throw new NotSupportedException()
            };

            Barrel.ApplicationId = "com.danieeis.apps.oisproducts";
            BarrelUtils.SetBaseCachePath(barrelPath);
            containerBuilder.RegisterInstance(Barrel.Current);

            containerBuilder.RegisterInstance<INavigationHelper>(navigationHelper);


            containerBuilder.RegisterType<PreferencesImplementation>().As<IPreferences>().SingleInstance();
            containerBuilder.RegisterType<SecureStorageImplementation>().As<ISecureStorage>().SingleInstance();
            containerBuilder.RegisterType<ConnectivityImplementation>().As<IConnectivity>().SingleInstance();
            containerBuilder.RegisterType<MainThreadImplementation>().As<IMainThread>().SingleInstance();

            containerBuilder.RegisterType<App>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<MessageService>().AsSelf().SingleInstance();

            var appAssembly = typeof(App).GetTypeInfo().Assembly;
            containerBuilder.RegisterAssemblyTypes(appAssembly)
                   .Where(x => x.IsSubclassOf(typeof(Page)));

            containerBuilder.RegisterAssemblyTypes(appAssembly)
                   .Where(x => x.IsSubclassOf(typeof(ViewModelBase)));

            var viewModelAssembly = typeof(ProductsApp.ViewModels.LoginViewModel).GetTypeInfo().Assembly;

            navigationHelper.InitViewModelNavigation(viewModelAssembly);

            IProductFakeApi productApiClient = RestService.For<IProductFakeApi>(ApiService.UserInitiated, settings: new(new NewtonsoftJsonContentSerializer(new JsonSerializerSettings())));

            containerBuilder.RegisterInstance(productApiClient).SingleInstance();

            var container = containerBuilder.Build();

            Resolver.SetResolver(new AutofacResolver(container));

            return container;
        }
    }
}

