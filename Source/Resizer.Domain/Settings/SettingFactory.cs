using System;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Domain.Settings
{
    /// <summary>
    /// Defines a class handles the construction of <see cref="ISetting{T}"/>
    /// </summary>
    public class SettingFactory : ISettingFactory
    {
        /// <summary>
        /// The <see cref="SettingStore"/> used to load and save settings data from file
        /// </summary>
        private readonly ISettingStore _settingStore;

        /// <summary>
        /// <see cref="IEventAggregator"/> used to send notifications when settings have changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

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
            return new Setting<TSetting>(_settingStore, _eventAggregator);
        }
    }
}
