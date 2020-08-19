using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using ControlzEx.Theming;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;
using WinReform.Gui.Infrastructure.Common.Command;
using WinReform.Gui.Infrastructure.Common.ViewModel;
using WinReform.Gui.Settings;

namespace WinReform.Gui.Window
{
    /// <summary>
    /// Defines a class that provides and handles application information
    /// </summary>
    public class WindowViewModel : ViewModelBase, IWindowViewModel
    {
        ///<inheritdoc/>
        public string Version { get; }

        ///<inheritdoc/>
        public bool MenuIsOpen
        {
            get => _menuIsOpen;
            set => SetProperty(ref _menuIsOpen, value);
        }

        private bool _menuIsOpen;

        ///<inheritdoc/>
        public bool MinimizeOnClose { get; set; }

        /// <summary>
        /// <see cref="IEventAggregator"/> used to be notified when the general setting have changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Shows the project source code on Github
        /// </summary>
        public DelegateCommand ShowSourceOnGithubCommand { get; }

        /// <summary>
        /// Shows the project versions on Github
        /// </summary>
        public DelegateCommand ShowVersionsOnGithubCommand { get; }

        /// <summary>
        /// Create a new instance of the <see cref="WindowViewModel"/>
        /// </summary>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/> used to be notified when the general setting have changed</param>
        /// <param name="applicationSettings"><see cref="ISetting{ApplicationSettings}"/> of the current app settings</param>
        /// 
        public WindowViewModel(IEventAggregator eventAggregator, ISetting<ApplicationSettings> applicationSettings)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<SettingChangedEvent<ApplicationSettings>>().Subscribe(ApplicationSettingsChanged, ThreadOption.UIThread, false);
            ApplicationSettingsChanged(applicationSettings);

            // Setup commands
            Version = $"v:{Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString(3)}";
            ShowSourceOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WinReform",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
            ShowVersionsOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WinReform/releases",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
        }

        /// <summary>
        /// Invoked when the general application settings have changed
        /// </summary>
        private void ApplicationSettingsChanged(ISetting<ApplicationSettings> settings)
        {
            if(settings.CurrentSetting != null)
            {
                UpdateTheme(settings.CurrentSetting.UseDarkTheme);
                MinimizeOnClose = settings.CurrentSetting.MinimizeOnClose;
            }
        }

        /// <summary>
        /// Updates the current application theme
        /// </summary>
        /// <param name="useDarkTheme">Indicates if the current theme should be <see cref="ThemeManager.BaseColorDark"/></param>
        private void UpdateTheme(bool useDarkTheme)
        {
            var themeName = useDarkTheme ? ThemeManager.BaseColorDark : ThemeManager.BaseColorLight;

            if (ThemeManager.Current.DetectTheme()?.BaseColorScheme != themeName && Application.Current != null)
            {
                ThemeManager.Current.ChangeThemeBaseColor(Application.Current, themeName);
            }
        }
    }
}
