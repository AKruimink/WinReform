using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Threading;
using Resizer.Domain.Infrastructure.Events;
using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Windows;
using Resizer.Gui.Infrastructure.Common.Command;
using Resizer.Gui.Infrastructure.Common.ViewModel;
using Resizer.Gui.Infrastructure.Extensions;

namespace Resizer.Gui.ActiveWindows
{
    /// <summary>
    /// Defines a class that provides active windows running on the system and management of said windows
    /// </summary>
    public class ActiveWindowsViewModel : ViewModelBase, IActiveWindowsViewModel
    {
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
            set => SetProperty(ref _selectedActiveWindows, value);
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

        ///<inheritdoc/>
        public bool ShouldAutomaticallyRefresh { get; set; }

        /// <summary>
        /// <see cref="DispatcherTimer"/> used to trigger the automatic refresh
        /// </summary>
        private DispatcherTimer _autoRefreshTmer;

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
        /// <param name="windowService"><see cref="IWindowService"/> used to get all active windows</param>
        public ActiveWindowsViewModel(IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));

            RefreshActiveWindowsCommand = new DelegateCommand(RefreshActiveWindows);
            ActiveWindows.UpdateCollection(_windowService.GetActiveWindows().ToList());

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

            _autoRefreshTmer = new DispatcherTimer();
            _autoRefreshTmer.Tick += OnAutoRefreshEvent;
            _autoRefreshTmer.Interval = TimeSpan.FromMilliseconds(1000);
            _autoRefreshTmer.Start();
        }

        /// <summary>
        /// Occures when the Refresh timer is finished and updates the <see cref="ActiveWindows"/> with the latest items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnAutoRefreshEvent(object? sender, EventArgs e)
        {
            _autoRefreshTmer.Stop();

            if(ShouldAutomaticallyRefresh)
            {
                RefreshActiveWindows();
            }

            _autoRefreshTmer.Start();
        }

        /// <summary>
        /// Refreshes the <see cref="ActiveWindows"/> with a new list of active windows
        /// </summary>
        public void RefreshActiveWindows()
        {
            ActiveWindows.UpdateCollection(_windowService.GetActiveWindows().ToList());
            // TODO send message out containing selected items, for the resizer and locator to pick up, and use 
        }
    }
}
