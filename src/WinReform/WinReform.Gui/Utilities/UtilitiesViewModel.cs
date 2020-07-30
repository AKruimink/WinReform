using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
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
        /// <see cref="IEventAggregator"/> used to be notified when the general setting have changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Changes the border style to a reziable one of all selected windows
        /// </summary>
        public DelegateCommand SetResizableBorderCommand { get; }

        public UtilitiesViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Subscribe(ActiveWindowsSelectionChanged);

            SetResizableBorderCommand = new DelegateCommand(SetResizableBorder);


        }

        
        /// <summary>
        /// 
        /// </summary>
        public void SetResizableBorder()
        {

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
