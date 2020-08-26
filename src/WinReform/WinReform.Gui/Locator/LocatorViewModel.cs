using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Displays;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using WinReform.Gui.Infrastructure.Common.Command;
using WinReform.Gui.Infrastructure.Common.ViewModel;

namespace WinReform.Gui.Locator
{
    /// <summary>
    /// Defines a class that provides data and functionality for the relocation of windows
    /// </summary>
    public class LocatorViewModel : ViewModelBase, ILocatorViewModel
    {
        ///<inheritdoc/>
        public string NewXAxis
        {
            get => _newXAxis;
            set => SetProperty(ref _newXAxis, value);
        }

        private string _newXAxis = string.Empty;

        ///<inheritdoc/>
        public string NewYAxis
        {
            get => _newYAxis;
            set => SetProperty(ref _newYAxis, value);
        }

        private string _newYAxis = string.Empty;

        ///<inheritdoc/>
        public List<Domain.Windows.Window> SelectedWindows { get; set; } = new List<Domain.Windows.Window>();

        /// <summary>
        /// <see cref="IEventAggregator"/> used to be notified when the selected windows has changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// <see cref="IWindowService"/> used to relocate the selected windows
        /// </summary>
        private readonly IWindowService _windowService;

        /// <summary>
        /// <see cref="IDisplayService"/> used to get the display information
        /// </summary>
        private readonly IDisplayService _displayService;

        /// <summary>
        /// Applies the custom <see cref="NewXAxis"/> and <see cref="NewYAxis"/> to all selected windows
        /// </summary>
        public DelegateCommand ApplyCustomLocationCommand { get; }

        /// <summary>
        /// Applies a preset of <see cref="Resolutions"/> to all selected windows
        /// </summary>
        public DelegateCommand ApplyPresetCommand { get; }

        public LocatorViewModel(IEventAggregator eventAggregator, IWindowService windowService, IDisplayService displayService)
        {
            _displayService = displayService ?? throw new ArgumentNullException(nameof(displayService));
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Subscribe(ActiveWindowsSelectionChanged);

            // Loads windows


            // Setup commands
            ApplyCustomLocationCommand = new DelegateCommand(ApplyCustomLocation);
        }

        /// <summary>
        /// Relocates all selected windows to the <see cref="NewXAxis"/> and <see cref="NewYAxis"/> location
        /// </summary>
        public void ApplyCustomLocation()
        {
            int.TryParse(NewXAxis, out var xAxis);
            int.TryParse(NewYAxis, out var yAxis);

            RelocateWindows(new Rect { Left = xAxis, Top = yAxis });
        }

        /// <summary>
        /// Relocates all selected windows
        /// </summary>
        /// <param name="newLocation"><see cref="Rect"/> containing the new location</param>
        private void RelocateWindows(Rect newLocation)
        {
            foreach(var window in SelectedWindows)
            {
                _windowService.RelocateWindow(window, newLocation);
            }
        }

        /// <summary>
        /// Invoked when the selection of windows in <see cref="ActiveWindows"/> changed
        /// </summary>
        /// <param name="windows"><see cref="List{Domain.Windows.Window}"/> containing all selected windows</param>
        private void ActiveWindowsSelectionChanged(List<Domain.Windows.Window> windows)
        {
            SelectedWindows = windows;
        }
    }
}
