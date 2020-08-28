using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Threading;
using WinReform.Domain.Infrastructure.Messanger;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Settings;
using WinReform.Domain.Windows;
using WinReform.Gui.Infrastructure.Common.Command;
using WinReform.Gui.Infrastructure.Common.ViewModel;
using WinReform.Gui.Infrastructure.Extensions;

namespace WinReform.Gui.ActiveWindows
{
    /// <summary>
    /// Defines a class that provides active windows running on the system and management of said windows
    /// </summary>
    public class ActiveWindowsViewModel : ViewModelBase, IActiveWindowsViewModel
    {
        /// <summary>
        /// State that defines if the Active Windows list should automaticly refresh every 1000 miliseconds
        /// </summary>
        private bool _autoRefreshActiveWindows;

        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> ActiveWindows
        {
            get => _activeWindows;
            set => SetProperty(ref _activeWindows, value);
        }

        private ObservableCollection<Domain.Windows.Window> _activeWindows = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> SelectedActiveWindows
        {
            get => _selectedActiveWindows;
            set
            {
                SetProperty(ref _selectedActiveWindows, value);
            }
        }

        private ObservableCollection<Domain.Windows.Window> _selectedActiveWindows = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public string WindowFilter
        {
            get => _windowFilter;
            set
            {
                SetProperty(ref _windowFilter, value);
                FilteredActiveWindows.Refresh();
            }
        }

        private string _windowFilter = string.Empty;

        ///<inheritdoc/>
        public ICollectionView FilteredActiveWindows { get; set; }

        /// <summary>
        /// <see cref="DispatcherTimer"/> used to trigger the automatic refresh
        /// </summary>
        private readonly DispatcherTimer _autoRefreshTimer;

        /// <summary>
        /// <see cref="IEventAggregator"/> used to be notified when the general setting have changed and notify about selected windows
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// <see cref="IWindowService"/> used to get all active windows
        /// </summary>
        private readonly IWindowService _windowService;

        /// <summary>
        /// Refreshes the list of active windows
        /// </summary>
        public DelegateCommand RefreshActiveWindowsCommand { get; }

        /// <summary>
        /// Create a new instance of the <see cref="ActiveWindowsViewModel"/>
        /// </summary>
        /// <param name="eventAggregator">        /// <see cref="IEventAggregator"/> used to be notified when the general setting have changed and notify about selected windows</param>
        /// <param name="windowService"><see cref="IWindowService"/> used to get all active windows</param>
        /// <param name="applicationSettings"><see cref="ISetting{ApplicationSettings}"/> of the current app settings</param>
        public ActiveWindowsViewModel(IEventAggregator eventAggregator, IWindowService windowService, ISetting<ApplicationSettings> applicationSettings)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<SettingChangedEvent<ApplicationSettings>>().Subscribe(ApplicationSettingsChanged, ThreadOption.UIThread, false);
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _ = applicationSettings ?? throw new ArgumentNullException(nameof(windowService));

            // setup commands
            RefreshActiveWindowsCommand = new DelegateCommand(RefreshActiveWindows);

            // setup the filter view
            FilteredActiveWindows = CollectionViewSource.GetDefaultView(ActiveWindows);
            FilteredActiveWindows.Filter = w =>
            {
                if (string.IsNullOrEmpty(WindowFilter))
                {
                    return true;
                }

                var window = w as Domain.Windows.Window;
                return window?.Description.Contains(WindowFilter, StringComparison.OrdinalIgnoreCase) ?? false;
            };

            // Setup the refresh timer
            _autoRefreshTimer = new DispatcherTimer();
            _autoRefreshTimer.Tick += OnAutoRefreshEvent;
            _autoRefreshTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _autoRefreshTimer.Start();

            // Setup view
            ApplicationSettingsChanged(applicationSettings);
            SelectedActiveWindows.CollectionChanged += SelectedActiveWindowsChanged;
            ActiveWindows.UpdateCollection(_windowService.GetActiveWindows().ToList());
        }

        /// <summary>
        /// Refreshes the <see cref="ActiveWindows"/> with a new list of active windows
        /// </summary>
        public void RefreshActiveWindows()
        {
            ActiveWindows.UpdateCollection(_windowService.GetActiveWindows().ToList());
        }

        /// <summary>
        /// Occures when the Refresh timer is finished and updates the <see cref="ActiveWindows"/> with the latest items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAutoRefreshEvent(object? sender, EventArgs e)
        {
            _autoRefreshTimer.Stop();

            if (_autoRefreshActiveWindows)
            {
                RefreshActiveWindows();
            }

            _autoRefreshTimer.Start();
        }

        /// <summary>
        /// Invoked when the general application settings have changed
        /// </summary>
        private void ApplicationSettingsChanged(ISetting<ApplicationSettings> settings)
        {
            if (settings.CurrentSetting != null)
            {
                _autoRefreshActiveWindows = settings.CurrentSetting.AutoRefreshActiveWindows;
            }
        }

        /// <summary>
        /// Invoked when <see cref="SelectedActiveWindows"/> changed and notifies all event subscribers of said change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectedActiveWindowsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Publish(SelectedActiveWindows.ToList());
        }
    }
}
