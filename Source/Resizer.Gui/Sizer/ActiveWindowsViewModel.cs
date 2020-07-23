using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Resizer.Domain.Infrastructure.Events;
using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Windows;
using Resizer.Gui.Infrastructure.Common.Command;
using Resizer.Gui.Infrastructure.Common.ViewModel;
using Resizer.Gui.Infrastructure.Extensions;

namespace Resizer.Gui.Sizer
{
    /// <summary>
    /// Defines a class that handles the active windows and managing of them
    /// </summary>
    public class ActiveWindowsViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or Sets the active windows
        /// </summary>
        public ObservableCollection<Domain.Windows.Window> ActiveWindows
        {
            get => _activeWindows;
            set => SetProperty(ref _activeWindows, value);
        }

        private ObservableCollection<Domain.Windows.Window> _activeWindows = new ObservableCollection<Domain.Windows.Window>();

        /// <summary>
        /// Gets or Sets the selected active windows
        /// </summary>
        public ObservableCollection<Domain.Windows.Window> SelectedActiveWindows
        {
            get => _selectedActiveWindows;
            set => SetProperty(ref _selectedActiveWindows, value);
        }

        private ObservableCollection<Domain.Windows.Window> _selectedActiveWindows = new ObservableCollection<Domain.Windows.Window>();

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
        }

        /// <summary>
        /// Refreshes the <see cref="ActiveWindows"/> with a new list of active windows
        /// TODO: <see cref="ActiveWindows"/> in <see cref="ActiveWindowsViewModel"/> should probably be updated instead of repalced by <see cref="RefreshActiveWindows"/>
        /// </summary>
        public void RefreshActiveWindows()
        {
            foreach(var item in ActiveWindows)
            {
                Debug.WriteLine(item.Description);
            }
            ActiveWindows.UpdateCollection(_windowService.GetActiveWindows().ToList());
            //SelectedActiveWindows = selectedItems;
        }
    }
}
