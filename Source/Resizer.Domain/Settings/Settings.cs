using System;

namespace Resizer.Domain.Settings
{
    public class Settings<TSetting> : ISettings<TSetting>, IEquatable<TSetting> where TSetting : new()
    {
        /// <summary>
        /// <see cref="ISettingsStore"/> that handles reading and saving of setting files
        /// </summary>
        private readonly ISettingsStore _settingStore;

        /// <summary>
        /// The current settings
        /// </summary>
        public TSetting CurrentSettings
        {
            get => _currentSettings;
            set
            {
                _currentSettings = value;
                // Notify value change
            }
        }

        private TSetting _currentSettings;

        /// <summary>
        /// Create a new instance of the <see cref="Settings{TSetting}"/>
        /// </summary>
        /// <param name="settingStore"><see cref="ISettingsStore"/> used for loading and saving settings</param>
        public Settings(ISettingsStore settingsStore)
        {
            _settingStore = settingsStore;
            CurrentSettings = _settingStore.Load<TSetting>();

        }

        /// <inheritdoc/>
        public void Save(TSetting settings)
        {
            if(Equals(settings))
            {
                return; // No changes have occured
            }

            _settingStore.Save(settings);
            _currentSettings = settings;
        }

        /// <summary>
        /// Comapires the existing settings with new settings
        /// </summary>
        /// <param name="newSettings">The settings to compaire</param>
        /// <returns>Returns <see langword="true"/> if the settings are equal, otherwise returns <see langword="false"/></returns>
        public bool Equals(TSetting newSettings)
        {
            if(newSettings is null)
            {
                return false;
            }

            if(ReferenceEquals(_currentSettings, newSettings))
            {
                return true;
            }

            if(newSettings.GetType() != _currentSettings?.GetType())
            {
                return false;
            }

            return false;
        }
    }
}
