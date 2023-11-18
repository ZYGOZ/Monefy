using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public IFileClient FileClient { get; set; }

        public INavigationService NavigationtService { get; set; }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public LoginViewModel(IMessenger messenger, INavigationService navigationService, IFileClient fileClient)
        {
            FileClient = fileClient;
            Users = FileClient.GetUsers();

            NavigationtService = navigationService;

            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }

        public RelayCommand LoginButton
        {
            get => new RelayCommand(() =>
            {
                foreach (var user in Users)
                {
                    if (user.UserName.ToLower() == UserName.ToLower() && user.Password.ToLower() == Password.ToLower())  {
                        NavigationtService.NavigateTo<MainViewModel>();
                    }
                }
            });
        }
        public RelayCommand SignUpButton
        {
            get => new RelayCommand(() =>
            {
                NavigationtService.NavigateTo<SignUpViewModel>();
            });
        }
    }
}
