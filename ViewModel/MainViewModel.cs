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
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>()
        {
            new Category() { Name = "Balance",Costs =0,Icon = "https://i.pinimg.com/736x/ea/b5/4f/eab54f5090228f05174ce8695840495a.jpg" },
            new Category() { Name = "Food",Costs =0,Icon = "https://i.pinimg.com/736x/bb/29/83/bb298383f8e76e304bacb13df8a79380.jpg" },
            new Category() { Name = "House",Costs =0,Icon = "https://i.pinimg.com/564x/8a/25/bf/8a25bf77aa8109c3e3a31483527ca6f9.jpg" },
            new Category() { Name = "Communication",Costs =0,Icon = "https://i.pinimg.com/564x/35/34/28/353428a29d54f7b6001cb67a96fbdfcb.jpg" },
            new Category() { Name = "Transport",Costs =0,Icon = "https://i.pinimg.com/736x/b8/91/97/b891971408988b293daf94de51477b87.jpg" },
            new Category() { Name = "Taxi",Costs =0,Icon = "https://i.pinimg.com/564x/19/93/7a/19937ab36a222840cba5641d932abbf3.jpg"},
            new Category() { Name = "Car",Costs =0,Icon = "https://i.pinimg.com/564x/52/a3/dd/52a3ddb8b037617f8687f4c6503e636e.jpg" },
            new Category() { Name = "Sport",Costs =0,Icon = "https://i.pinimg.com/564x/34/af/44/34af4403628bee38605f9679410ff634.jpg" },
            new Category() { Name = "Clothes",Costs =0,Icon = "https://i.pinimg.com/564x/de/3f/05/de3f051ae1c00f2eb1937555aaf86e29.jpg" },
            new Category() { Name = "Gifts",Costs =0,Icon = "https://i.pinimg.com/736x/36/3a/fb/363afbdb5c5175b843b7d4f83869d421.jpg" },
            new Category() { Name = "Hygiene",Costs =0,Icon = "https://i.pinimg.com/564x/d2/b0/b0/d2b0b0fd77a67e12152edf4a3fa1374f.jpg" },
            new Category() { Name = "Pets",Costs =0,Icon = "https://i.pinimg.com/564x/66/31/1f/66311f4a905e8df39c2480c02bb29ca9.jpg" },
            new Category() { Name = "Health",Costs =0,Icon = "https://i.pinimg.com/564x/82/a1/78/82a1785cb1e9551ef40eb1814619ca45.jpg" },
        };

        public Category SelectedCategory { get; set; }

        public INavigationService NavigationtService { get; set; }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public MainViewModel(IMessenger messenger, INavigationService navigationService)
        {
            NavigationtService = navigationService;

            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }


        public RelayCommand AddButton
        {
            get => new RelayCommand(() =>
            {
                NavigationtService.NavigateTo<CategoryViewModel>();
            });
        }

        public RelayCommand SubtractButton
        {
            get => new RelayCommand(() =>
            {
                NavigationtService.NavigateTo<CategoryViewModel>();
            });
        }
    }
}
