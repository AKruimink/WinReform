using Resizer.Domain.Settings;
using Resizer.Gui.Infrastructure.Common.ViewModel;

namespace Resizer.Gui.Settings
{
    /// <summary>
    /// Defines a class that provides general application settings data
    /// </summary>
    public class ApplicationSettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or Sets an idicator that defines if dark theme should be used
        /// </summary>
        public bool UseDarkTheme
        {
            get => _useDarkTheme;
            set
            {
                SetProperty(ref _useDarkTheme, value);
                _settings.CurrentSetting.UseDarkTheme = value;
                SaveSettings();
            }
        }

        private bool _useDarkTheme = true;

        /// <summary>
        /// Gets or Sets an idicator that defines if the window should be minimized when closed
        /// </summary>
        public bool MinimizeOnClose
        {
            get => _minimizeOnClose;
            set
            {
                SetProperty(ref _minimizeOnClose, value);
                _settings.CurrentSetting.MinimizeOnClose = value;
                SaveSettings();
            }
        }

        private bool _minimizeOnClose;

        /// <summary>
        /// <see cref="ISetting{TSetting}"/> containing the <see cref="ApplicationSettings"/>
        /// </summary>
        private readonly ISetting<ApplicationSettings> _settings;

        /// <summary>
        /// Create a new instance of the <see cref="ApplicationSettingsViewModel"/>
        /// </summary>
        public ApplicationSettingsViewModel(ISettingFactory settingfactory)
        {
            _settings = settingfactory.Create<ApplicationSettings>();

            UseDarkTheme = _settings.CurrentSetting.UseDarkTheme;
            MinimizeOnClose = _settings.CurrentSetting.MinimizeOnClose;
        }

        /// <summary>
        /// Save the settings
        /// </summary>
        private void SaveSettings()
        {
            _settings.Save();
        }
    }
}
