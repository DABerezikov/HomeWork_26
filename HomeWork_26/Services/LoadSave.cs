using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace HomeWork_26.Services
{
    internal static class LoadSave<T>
    {
        public static ObservableCollection<T>? LoadDb(string path)
        {
            var clients = new ObservableCollection<T>();

            if (File.Exists(path))
            {
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };


                using var streamReader = new StreamReader(path);
                string text = streamReader.ReadToEnd();
                clients = JsonConvert.DeserializeObject<ObservableCollection<T>>(text, settings);
            }
            return clients;
        }

        public static void SaveDb(string path, ObservableCollection<T>? clients)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All

            };

            using var streamWriter = new StreamWriter(path);
            string text = JsonConvert.SerializeObject(clients, Formatting.Indented, settings);
            streamWriter.WriteLine(text);
        }

        public static void Log(string text)
        {
            using var sr = new StreamWriter(@"log.txt", true);
            sr.WriteLine($"\n{text}");
        }
    }
}
