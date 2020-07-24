using Resizer.Gui.Settings;

namespace Resizer.Gui.Tests.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IApplicationSettingsViewModel"/>
    /// </summary>
    public class ApplicationSettingsViewModelMock : IApplicationSettingsViewModel
    {
        ///<inheritdoc/>
        public bool UseDarkTheme { get; set; }

        ///<inheritdoc/>
        public bool MinimizeOnClose { get; set; }
    }
}