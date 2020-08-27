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
        public string NewHorizontalLocation
        {
            get => _newHorizontalLocation;
            set => SetProperty(ref _newHorizontalLocation, value);
        }

        private string _newHorizontalLocation = string.Empty;

        ///<inheritdoc/>
        public string NewVerticalLocation
        {
            get => _newVerticalLocation;
            set => SetProperty(ref _newVerticalLocation, value);
        }

        private string _newVerticalLocation = string.Empty;

        ///<inheritdoc/>
        public List<Display> AvailableDisplays
        {
            get => _availableDisplays;
            set => SetProperty(ref _availableDisplays, value);
        }

        private List<Display> _availableDisplays = new List<Display>();

        ///<inheritdoc/>
        public Display SelectedDisplay
        {
            get => _selectedDisplay;
            set => SetProperty(ref _selectedDisplay, value);
        }

        private Display _selectedDisplay = new Display();

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
        public DelegateCommand<Location?> ApplyPresetCommand { get; }

        public LocatorViewModel(IEventAggregator eventAggregator, IWindowService windowService, IDisplayService displayService)
        {
            _displayService = displayService ?? throw new ArgumentNullException(nameof(displayService));
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Subscribe(ActiveWindowsSelectionChanged);

            // Loads windows
            AvailableDisplays = _displayService.GetAllDisplays();

            // Setup commands
            ApplyCustomLocationCommand = new DelegateCommand(ApplyCustomLocation);
            ApplyPresetCommand = new DelegateCommand<Location?>(ApplyPresetLocation);
        }

        /// <summary>
        /// Relocates all selected windows to the <see cref="NewXAxis"/> and <see cref="NewYAxis"/> location
        /// </summary>
        public void ApplyCustomLocation()
        {
            int.TryParse(NewHorizontalLocation, out var xAxis);
            int.TryParse(NewVerticalLocation, out var yAxis);

            RelocateWindows(new Rect { Left = xAxis, Top = yAxis });
        }

        /// <summary>
        /// Relocates all selected windows to a given preset
        /// </summary>
        /// <param name="location"><see cref="Location"/> containing a preset location</param>
        public void ApplyPresetLocation(Location? location)
        {
             if(location != null )
            {
                foreach (var window in SelectedWindows)
                {
                    var newLocation = new Rect();
                    switch (location?.HorizontalLocation)
                    {
                        case HorizontalLocationType.Left:
                            newLocation.Left = SelectedDisplay.WorkArea.Left;
                            break;
                        case HorizontalLocationType.Center:
                            newLocation.Left = SelectedDisplay.WorkArea.Right - ((SelectedDisplay.WorkArea.Right - SelectedDisplay.WorkArea.Left) / 2) - (window.Dimensions.Right - window.Dimensions.Left) / 2;
                            break;
                        case HorizontalLocationType.Right:
                            newLocation.Left = SelectedDisplay.WorkArea.Right - (window.Dimensions.Right - window.Dimensions.Left);
                            break;
                    }

                    switch (location?.VerticalLocation)
                    {
                        case VerticalLocationType.Top:
                            newLocation.Top = SelectedDisplay.WorkArea.Top;
                            break;
                        case VerticalLocationType.Center:
                            newLocation.Top = SelectedDisplay.WorkArea.Bottom - ((SelectedDisplay.WorkArea.Bottom - SelectedDisplay.WorkArea.Top) / 2) - (window.Dimensions.Bottom - window.Dimensions.Top) / 2;
                            break;
                        case VerticalLocationType.Bottom:
                            newLocation.Top = SelectedDisplay.WorkArea.Bottom - (window.Dimensions.Bottom - window.Dimensions.Top);
                            break;
                    }

                    _windowService.RelocateWindow(window, newLocation);
                }
            }
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
