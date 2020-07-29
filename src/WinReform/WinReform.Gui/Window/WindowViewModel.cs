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
        /// <see cref="ISettingFactory"/> used to load the application settings on creation
        /// </summary>
        private readonly ISettingFactory _settingFactory;

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
        /// Setup the window after it's loaded in
        /// </summary>
        public DelegateCommand WindowLoadedCommand { get; }

        /// <summary>
        /// Create a new instance of the <see cref="WindowViewModel"/>
        /// //TODO fix the <see cref="WindowViewModel"/> summary
        /// </summary>
        /// <param name="generalSettings">Instance of the <see cref="ApplicationSettingsViewModel"/></param>
        public WindowViewModel(ISettingFactory settingFactory, IEventAggregator eventAggregator)
        {
            _settingFactory = settingFactory ?? throw new ArgumentNullException(nameof(settingFactory));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            Version = $"v:{Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString(3)}";
            ShowSourceOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WindowResizer",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
            ShowVersionsOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WindowResizer/releases",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
            WindowLoadedCommand = new DelegateCommand(WindowLoaded);
        }

        /// <summary>
        /// Loads all the settings once the window is loaded
        /// </summary>
        private void WindowLoaded()
        {
            ApplicationSettingsChanged(_settingFactory.Create<ApplicationSettings>()); // Manualy set the application settings once as we wont be notified until something changes
            _eventAggregator.GetEvent<SettingChangedEvent<ApplicationSettings>>().Subscribe(ApplicationSettingsChanged, ThreadOption.UIThread, false);
        }

        /// <summary>
        /// Invoked when the general application settings have changed
        /// </summary>
        private void ApplicationSettingsChanged(ISetting<ApplicationSettings> settings)
        {
            UpdateTheme(settings.CurrentSetting.UseDarkTheme);
            MinimizeOnClose = settings.CurrentSetting.MinimizeOnClose;
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
