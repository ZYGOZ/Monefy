using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.View;
using Monefy.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Monefy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            StartMain<LoginViewModel>();

            base.OnStartup(e);
        }

        public void Register()
        {
            Container = new Container();


            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<IFileClient, FileClient>();

            Container.RegisterSingleton<LoginViewModel>();
            Container.RegisterSingleton<SignUpViewModel>();
            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<CategoryViewModel>();

            Container.Verify();
        }

        public void StartMain<T>() where T : ViewModelBase
        {
            Window window = new LoginView();
            var viewModel1 = Container.GetInstance<T>();

            window.DataContext = viewModel1;
            window.ShowDialog();

        }
    }
}
