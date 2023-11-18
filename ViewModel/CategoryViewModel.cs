using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Commands;
using Monefy.Messages;
using Monefy.Model;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Monefy.ViewModel
{
    public class CategoryViewModel : ViewModelBase
    {
        public MainViewModel mainViewModel { get; set; }

        private string text = "0";
        public string Text
        {
            get { return text; }
            set { Set(ref text, value); }
        }
        
        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        public INavigationService NavigationtService { get; set; }

        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

        public CategoryViewModel(IMessenger messenger, INavigationService navigationService,MainViewModel mainViewModel1)
        {
            mainViewModel = mainViewModel1;
            NavigationtService = navigationService;

            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Container.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }


        private ICommand command;
        public ICommand Command
        {
            get
            {
                if (command == null)
                {
                    command = new ButtonCommand<object>(NumAndOperationClick);
                }

                return command;
            }
        }
        private void NumAndOperationClick(object parameter)
        {
            if(Text == "0") 
            {
                Text = "";
            }
            Text += parameter.ToString();
        }
        public RelayCommand ResultButton
        {
            get => new RelayCommand(() =>
            {
                double tmp  = Evaluate(Text);
                if (tmp < 0)
                {
                    Text = "0";
                }
                else
                {
                    Text = tmp.ToString();
                }
            });
        }

        public RelayCommand FinalButton
        {
            get => new RelayCommand(() =>
            {
                foreach (var item in mainViewModel.Categories)
                {
                    if(item == mainViewModel.SelectedCategory)
                    {
                        item.Costs += Convert.ToDouble(Text);
                        if(item.Name != mainViewModel.Categories[0].Name)
                        {
                            mainViewModel.Categories[0].Costs -= Convert.ToDouble(Text);
                        }

                    }
                }
                NavigationtService.NavigateTo<MainViewModel>();
            });
        }

        public RelayCommand DeleteButton
        {
            get => new RelayCommand(() =>
            {
                if (Text.Length > 0)
                {
                    Text = Text.Substring(0, Text.Length - 1);
                }
            });
        }

    }
}
