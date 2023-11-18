using Monefy.Model;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Monefy.Services.Classes
{
    public class FileClient : IFileClient
    {
        private string filename = "Users.json";
        public string GetFile()
        {
            string json = "";
            
            if (File.Exists(filename))
            {
                json = File.ReadAllText(filename);
            }
            return json;
        }

        public void Serialize(ObservableCollection<User> users)
        {
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText(filename, json);
        }

        public ObservableCollection<User> GetUsers()
        {
            string json = GetFile();

            var result = JsonSerializer.Deserialize<ObservableCollection<User>>(json);

            return result;
        }

    }
}
