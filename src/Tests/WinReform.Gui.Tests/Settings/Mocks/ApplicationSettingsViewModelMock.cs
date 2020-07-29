using WinReform.Gui.Settings;

namespace WinReform.Gui.Tests.Settings.Mocks
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