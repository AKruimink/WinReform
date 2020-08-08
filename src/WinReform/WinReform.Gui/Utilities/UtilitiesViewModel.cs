using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Windows;
using WinReform.Gui.Infrastructure.Common.Command;
using WinReform.Gui.Infrastructure.Common.ViewModel;

namespace WinReform.Gui.Utilities
{
    /// <summary>
    /// Defines a class that provides window resizing utility data
    /// </summary>
    public class UtilitiesViewModel : ViewModelBase, IUtilitiesViewModel
    {
        /// <summary>
        /// <see cref="List{Domain.Windows.Window}"/> containing all currently selected windows
        /// </summary>
        private List<Domain.Windows.Window> _selectedWindows = new List<Domain.Windows.Window>();

        /// <summary>
        /// <see cref="IEventAggregator"/> used to be notified when the selected windows has changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// <see cref="IWindowService"/> used manage all active windows
        /// </summary>
        private readonly IWindowService _windowService;

        /// <summary>
        /// Changes the border style to a reziable one of all selected windows
        /// </summary>
        public DelegateCommand SetResizableBorderCommand { get; }

        /// <summary>
        /// Changes the border style to a reziable one of all selected windows
        /// </summary>
        public DelegateCommand RedrawWindowCommand { get; }

        /// <summary>
        /// Create a new instance of <see cref="UtilitiesViewModel"/>
        /// </summary>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/> used to be notified when the selected windows has changed</param>
        /// <param name="windowService"><see cref="IWindowService"/> used manage all active windows</param>
        public UtilitiesViewModel(IEventAggregator eventAggregator, IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Subscribe(ActiveWindowsSelectionChanged);

            SetResizableBorderCommand = new DelegateCommand(SetResizableBorder);
            RedrawWindowCommand = new DelegateCommand(RedrawWindow);
        }

        
        /// <summary>
        /// Sets the window style of all selected windows to a resizable border
        /// </summary>
        public void SetResizableBorder()
        {
            foreach(var window in _selectedWindows)
            {
                _windowService.SetResizableBorder(window);
            }
        }

        /// <summary>
        /// Redraws all selected windows
        /// </summary>
        public void RedrawWindow()
        {
            foreach(var window in _selectedWindows)
            {
                _windowService.RedrawWindow(window);
            }
        }

        /// <summary>
        /// Invoked when the selection of windows in <see cref="ActiveWindows"/> changed
        /// </summary>
        /// <param name="windows"><see cref="List{Domain.Windows.Window}"/> containing all selected windows</param>
        private void ActiveWindowsSelectionChanged(List<Domain.Windows.Window> windows)
        {
            _selectedWindows = windows;
        }
    }
}
