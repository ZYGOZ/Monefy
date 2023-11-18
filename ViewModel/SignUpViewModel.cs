using GalaSoft.MvvmLight;
using System;
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
using System.Windows;

namespace Monefy.ViewModel
{
    public class SignUpViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; }

        public string UserName { get; set; } = "Name";
        public string Password { get; set; } = "Password";
        public string PasswordVerify { get; set; } = "Verify Password";


        public IFileClient FileClient { get; set; }

        public INavigationService NavigationtService { get; set; }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public SignUpViewModel(IMessenger messenger, INavigationService navigationService, IFileClient fileClient)
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

        public RelayCommand SignUpButton
        {
            get => new RelayCommand(() =>
            {
                if (Password == PasswordVerify)
                {
                    foreach (var user in Users)
                    {
                        if (user.UserName.ToLower() == UserName.ToLower())
                        {
                            MessageBox.Show("Account with this name is exist");
                            return;
                        }
                    }
                    User newUser = new User() { UserName = UserName, Password = Password };
                    Users.Add(newUser);
                    FileClient.Serialize(Users);
                    NavigationtService.NavigateTo<MainViewModel>();

                }
                else
                {
                    MessageBox.Show("Passwords are not coincide");
                    return;
                }
            });
        }
    }
}
