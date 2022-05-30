using HomeWork_26.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace HomeWork_26.Services
{
    internal static class LoadSave
    {
        public static ObservableCollection<Client> LoadDB(string Path)
        {
            ObservableCollection<Client> clients = new ObservableCollection<Client>();

            if (File.Exists(Path))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };


                using (StreamReader streamReader = new StreamReader(Path))
                {
                    string text = streamReader.ReadToEnd();
                    clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(text, settings);
                }

            }
            return clients;
        }

        public static void SaveDB(string Path, ObservableCollection<Client> clients)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All

            };

            using (StreamWriter streamWriter = new StreamWriter(Path))
            {
                string text = JsonConvert.SerializeObject(clients, Formatting.Indented, settings);
                streamWriter.WriteLine(text);
            }
        }

        public static void Log(string Text)
        {
            using (StreamWriter sr = new StreamWriter(@"log.txt", true))
            {
                sr.WriteLine($"\n{Text}");
            }
        }
    }
}
