using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace WinReform.Domain.Settings
{
    /// <summary>
    /// Defines a class that loads and saves settings
    /// TODO: Make Load and Save awaitable in SettingStore
    /// </summary>
    public class SettingStore : ISettingStore
    {
        /// <summary>
        /// Path to where the setting files are saved
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Create a new instance of the <see cref="SettingStore"/>
        /// </summary>
        public SettingStore()
        {
            _filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\{Assembly.GetEntryAssembly()?.GetName().Name}";

#if DEBUG
            _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
#endif
        }

        ///<inheritdoc/>
        public TSetting Load<TSetting>() where TSetting : new()
        {
            var file = Path.Combine(_filePath, $"{typeof(TSetting).Name}.json");
            var fileInfo = new FileInfo(file);

            // Create default settings if non exist
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                Save(new TSetting());
            }

            var settingString = File.ReadAllText(file);
            var serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Deserialize<TSetting>(settingString, serializerOptions);
        }

        ///<inheritdoc/>
        public void Save<TSetting>(TSetting settings)
        {
            var file = Path.Combine(_filePath, $"{typeof(TSetting).Name}.json");
            var fileInfo = new FileInfo(file);
            fileInfo.Directory.Create();

            var serializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(settings, serializerOptions);
            File.WriteAllText(file, json);
        }
    }
}
