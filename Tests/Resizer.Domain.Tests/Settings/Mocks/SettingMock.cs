using Resizer.Domain.Settings;

namespace Resizer.Domain.Tests.Settings.Mocks
{
    public class SettingMock<TSetting> : ISetting<TSetting> where TSetting : new()
    {
        /// <summary>
        /// Indicates if the <see cref="Save"/> has been executed
        /// </summary>
        public bool SaveCalled { get; set; } = false;

        ///<inheritdoc/>
        ///
        public TSetting CurrentSetting => new TSetting();

        ///<inheritdoc/>
        public void Save()
        {
            SaveCalled = true;
        }
    }
}
