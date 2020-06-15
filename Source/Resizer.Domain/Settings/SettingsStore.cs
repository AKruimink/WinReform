using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Defines a class that loads and saves settings
    /// </summary>
    public class SettingsStore : ISettingsStore
    {
        /// <summary>
        /// Path to where the setting files are saved
        /// </summary>
        private readonly string _filePath;

        /// <summary>
        /// Create a new instance of the <see cref="SettingsStore"/>
        /// </summary>
        public SettingsStore()
        {
            _filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        ///<inheritdoc/>
        public TSettings Load<TSettings>() where TSettings : new()
        {
            var file = Path.Combine(_filePath, $"{typeof(TSettings).Name}.xml");
            var fileInfo = new FileInfo(file);

            // Create default settings if non exist
            if(!fileInfo.Exists || fileInfo.Length == 0)
            {
                Save(new TSettings());
            }

            var serializer = new XmlSerializer(typeof(TSettings));

            using var reader = new StreamReader(file);
            return (TSettings)serializer.Deserialize(reader);
        }

        ///<inheritdoc/>
        public void Save<TSettings>(TSettings settings)
        {
            var file = Path.Combine(_filePath, $"{typeof(TSettings).Name}.xml");
            var settingsFile = new XmlDocument();
            var serializer = new XmlSerializer(typeof(TSettings));

            using var stream = new MemoryStream();
            serializer.Serialize(stream, settings);
            stream.Position = 0;
            settingsFile.Load(stream);
            settingsFile.Save(file);
            stream.Close();
        }
    }
}
