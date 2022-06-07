using System;
using System.Reflection;
using Autofac;
using TinyMvvm;
using TinyMvvm.Autofac;
using TinyMvvm.Forms;
using TinyMvvm.IoC;
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

            containerBuilder.RegisterInstance<INavigationHelper>(navigationHelper);


            //containerBuilder.RegisterType<PreferencesImplementation>().As<IPreferences>().SingleInstance();
            //containerBuilder.RegisterType<SecureStorageImplementation>().As<ISecureStorage>().SingleInstance();
            //containerBuilder.RegisterType<MainThreadImplementation>().As<IMainThread>().SingleInstance();
            //containerBuilder.RegisterType<ConnectivityImplementation>().As<IConnectivity>().SingleInstance();

            containerBuilder.RegisterType<App>().AsSelf().SingleInstance();

            var appAssembly = typeof(App).GetTypeInfo().Assembly;
            containerBuilder.RegisterAssemblyTypes(appAssembly)
                   .Where(x => x.IsSubclassOf(typeof(Page)));

            containerBuilder.RegisterAssemblyTypes(appAssembly)
                   .Where(x => x.IsSubclassOf(typeof(ViewModelBase)));

            var viewModelAssembly = typeof(ProductsApp.ViewModels.LoginViewModel).GetTypeInfo().Assembly;

            navigationHelper.InitViewModelNavigation(viewModelAssembly);

            var container = containerBuilder.Build();

            Resolver.SetResolver(new AutofacResolver(container));

            return container;
        }
    }
}

