using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Resizer.Domain.Settings
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
            _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        ///<inheritdoc/>
        public TSetting Load<TSetting>() where TSetting : new()
        {
            var file = Path.Combine(_filePath, $"{typeof(TSetting).Name}.xml");
            var fileInfo = new FileInfo(file);

            // Create default settings if non exist
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                Save(new TSetting());
            }

            var serializer = new XmlSerializer(typeof(TSetting));

            using var reader = new StreamReader(file);
            return (TSetting)serializer.Deserialize(reader);
        }

        ///<inheritdoc/>
        public void Save<TSetting>(TSetting settings)
        {
            var file = Path.Combine(_filePath, $"{typeof(TSetting).Name}.xml");
            var settingsFile = new XmlDocument();
            var serializer = new XmlSerializer(typeof(TSetting));

            using var stream = new MemoryStream();
            serializer.Serialize(stream, settings);
            stream.Position = 0;
            settingsFile.Load(stream);
            settingsFile.Save(file);
            stream.Close();
        }
    }
}
