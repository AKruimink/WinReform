using Resizer.Domain.Settings;
using System;

namespace Resizer.Domain.Tests.Settings.Mocks
{
    public class SettingStoreMock : ISettingStore
    {
        /// <summary>
        /// Indicates if the request has been executed
        /// </summary>
        public bool Executed { get; set; } = false;

        /// <summary>
        /// Arguments passed to the mock method
        /// </summary>
        public object? Arguments { get; set; }

        /// <summary>
        /// <see cref="Type"/> of the setting passed
        /// </summary>
        public Type? SettingType { get; set; }

        ///<inheritdoc/>
        public TSetting Load<TSetting>() where TSetting : new()
        {
            SettingType = typeof(TSetting);
            return new TSetting();
        }

        ///<inheritdoc/>
        public void Save<TSetting>(TSetting settings)
        {
            Executed = true;
            Arguments = settings;
            SettingType = typeof(TSetting);
        }
    }
}
