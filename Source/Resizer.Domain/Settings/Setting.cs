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

        /// <inheritdoc/>
        public TSetting CurrentSetting { get; set; }
        /// <summary>
        /// Create a new instance of the <see cref="Setting{TSetting}"/>
        /// </summary>
        /// <param name="settingStore"><see cref="ISettingStore"/> used for loading and saving settings</param>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/> used to notify subscribers about settings changes</param>
        public Setting(ISettingStore settingStore, IEventAggregator eventAggregator)
        {
            _settingStore = settingStore ?? throw new ArgumentNullException(nameof(settingStore));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            CurrentSetting = _settingStore.Load<TSetting>();
        }

        /// <inheritdoc/>
        public void Save(TSetting setting)
        {
            try
            {
                _settingStore.Save(setting);
                CurrentSetting = setting;

                NotifySettingsChanged();
            }
            catch(Exception ex)
            {
                // TODO replace Console.Writeline with some form of logging
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Compair a <see cref="Setting{TSetting}"/> to the current <see cref="Setting{TSetting}"/>
        /// </summary>
        /// <param name="other">The <see cref="Setting{TSetting}"/> to compaire</param>
        /// <returns>Returns <see langword="true"/> if the settings are equal, otherwise returns <see langword="false"/></returns>
        public bool Equals(TSetting other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(CurrentSetting, other))
            {
                return true;
            }

            if (other.GetType() != GetType())
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Compair a object to <see cref="Setting{TSetting}"/>
        /// </summary>
        /// <param name="obj">The object to compair</param>
        /// <returns>Returns <see langword="true"/> if the settings are equal, otherwise returns <see langword="false"/></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(CurrentSetting, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Setting<TSetting>) obj);
        }

        /// <summary>
        /// Get the hashcode of the current object
        /// </summary>
        /// <returns>Returns the hashcode of the current object</returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Notifies all subscribers of a settings change
        /// </summary>
        private void NotifySettingsChanged()
        {
            _eventAggregator.GetEvent<SettingChangedEvent>().Publish(typeof(TSetting));
        }
    }
}
