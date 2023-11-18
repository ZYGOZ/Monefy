using Monefy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces
{
    public interface IFileClient
    {
        public string GetFile();

        public void Serialize(ObservableCollection<User> users);

        public ObservableCollection<User> GetUsers();

    }
}