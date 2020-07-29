using Resizer.Gui.Settings;

namespace Resizer.Gui.Tests.Settings.Mocks
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

        ///<inheritdoc/>
        public bool AutoRefreshActiveWindows { get; set; }
    }
}