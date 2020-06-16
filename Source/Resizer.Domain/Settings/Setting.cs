using System;
using Resizer.Domain.Infrastructure.Events;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Defines a class that represents a setting
    /// </summary>
    /// <typeparam name="TSetting">The type of the setting to represent</typeparam>
    public class Setting<TSetting> : ISetting<TSetting>, IEquatable<TSetting> where TSetting : new()
    {
        /// <summary>
        /// <see cref="ISettingStore"/> that handles reading and saving of setting files
        /// </summary>
        private readonly ISettingStore _settingStore;

        /// <summary>
        /// <see cref="IEventAggregator"/> that notifies subscribers when the current settings have been changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// The current settings
        /// </summary>
        public TSetting CurrentSetting
        {
            get => _currentSetting;
            private set
            {
                _currentSetting = value;
                _eventAggregator.GetEvent<SettingChangedEvent>().Publish(typeof(TSetting));
            }
        }

        private TSetting _currentSetting;

        /// <summary>
        /// Create a new instance of the <see cref="Setting{TSetting}"/>
        /// </summary>
        /// <param name="settingStore"><see cref="ISettingStore"/> used for loading and saving settings</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/> used to notify subscribers about settings changes</param>
        public Setting(ISettingStore settingsStore, IEventAggregator eventAggregator)
        {
            _settingStore = settingsStore;
            _eventAggregator = eventAggregator;

            //TODO: fix the need for double assignment of CurrentSettings to avoid Null Reference complain of a generic type in Settings
            _currentSetting = _settingStore.Load<TSetting>();
            CurrentSetting = _currentSetting;

        }

        /// <inheritdoc/>
        public void Save(TSetting settings)
        {
            if (Equals(settings))
            {
                return; // No changes have occured
            }

            _settingStore.Save(settings);
            _currentSetting = settings;
        }

        /// <summary>
        /// Comapires the existing settings with new settings
        /// </summary>
        /// <param name="newSettings">The settings to compaire</param>
        /// <returns>Returns <see langword="true"/> if the settings are equal, otherwise returns <see langword="false"/></returns>
        public bool Equals(TSetting newSettings)
        {
            if (newSettings is null)
            {
                return false;
            }

            if (ReferenceEquals(_currentSetting, newSettings))
            {
                return true;
            }

            if (newSettings.GetType() != _currentSetting?.GetType())
            {
                return false;
            }

            return false;
        }
    }
}
