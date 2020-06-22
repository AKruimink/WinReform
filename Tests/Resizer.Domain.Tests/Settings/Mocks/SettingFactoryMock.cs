using Resizer.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resizer.Domain.Tests.Settings.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="ISettingFactory"/>
    /// </summary>
    public class SettingFactoryMock : ISettingFactory
    {
        /// <summary>
        /// Indicates if the <see cref="Create{TSetting}"/> has been called
        /// </summary>
        public bool Called { get; set; } = false;

        /// <summary>
        /// <see cref="Type"/> of the setting expected to create
        /// </summary>
        public Type? SettingType { get; set; }

        ///<inheritdoc/>
        public ISetting<TSetting> Create<TSetting>() where TSetting : new()
        {
            Called = true;
            SettingType = typeof(TSetting);
            return new SettingMock<TSetting>();
        }
    }
}
