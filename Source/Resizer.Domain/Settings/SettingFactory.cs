using System;
using System.Collections.Generic;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Defines a class handles the construction of <see cref="ISetting{T}"/>
    /// </summary>
    public class SettingFactory : ISettingFactory
    {
        /// <summary>
        /// The <see cref="ISettingStore"/> used to load and save settings data from file
        /// </summary>
        private readonly ISettingStore _settingStore;

        /// <summary>
        /// <see cref="IEventAggregator"/> used to send notifications when settings have changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Cached settings
        /// // TODO: Ass some sort of contract to <see cref="Setting{TSetting}"/> that allows the cache to be set forcing an <see cref="ISetting{TSetting}"/> instance instead of any object
        /// </summary>
        private readonly Dictionary<Type, object> _settings = new Dictionary<Type, object>();

        /// <summary>
        /// Create a new instance of the <see cref="SettingFactory"/>
        /// </summary>
        /// <param name="settingStore">The <see cref="SettingStore"/> used by <see cref="Setting{TSetting}"/> to load and save settings</param>
        /// <param name="eventAggregator">The <see cref="EventAggregator"/> used to notify subscribers of setting changes</param>
        public SettingFactory(ISettingStore settingStore, IEventAggregator eventAggregator)
        {
            _settingStore = settingStore ?? throw new ArgumentNullException(nameof(settingStore));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
        }

        ///<inheritdoc/>
        public ISetting<TSetting> Create<TSetting>() where TSetting : new()
        {
            lock(_settings)
            {
                if(!_settings.TryGetValue(typeof(TSetting), out var existingSetting))
                {
                    var newSetting = new Setting<TSetting>(_settingStore, _eventAggregator);
                    _settings[typeof(TSetting)] = newSetting;

                    return newSetting;
                }
                else
                {
                    return (ISetting<TSetting>)existingSetting;
                }
            }
        }
    }
}
