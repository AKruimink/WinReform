using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Domain.Infrastructure.Events;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using WinReform.Gui.Infrastructure.Common.Command;
using WinReform.Gui.Infrastructure.Common.ViewModel;

namespace WinReform.Gui.Resizer
{
    /// <summary>
    /// Defines a class that provides data and functionality for resizing of windows
    /// </summary>
    public class ResizerViewModel : ViewModelBase, IResizerViewModel
    {
        ///<inheritdoc/>
        public Dictionary<string, Rect> Resolutions { get; } = new Dictionary<string, Rect>()
        {
            { "640x480 (4/3)", new Rect() {Right = 640, Bottom = 480 } },
            { "720x480 (4/3)", new Rect() {Right = 720, Bottom = 480 } },
            { "720x576", new Rect() {Right = 720, Bottom = 576 } },
            { "800x600 (4/3)", new Rect() {Right = 800, Bottom = 600 } },
            { "1024x768 (4/3)", new Rect() {Right = 1024, Bottom = 768 } },
            { "1152x864 (4/3)", new Rect() {Right = 1152, Bottom = 864 } },
            { "1176x664", new Rect() {Right = 1176, Bottom = 664 } },
            { "1280x720 (16/9)", new Rect() {Right = 1280, Bottom = 720 } },
            { "1280x768 (16/10)", new Rect() {Right = 1280, Bottom = 768 } },
            { "1280x800 (16/10)", new Rect() {Right = 1280, Bottom = 800 } },
            { "1280x960 (4/3)", new Rect() {Right = 1280, Bottom = 960 } },
            { "1280x1024", new Rect() {Right = 1280, Bottom = 1024 } },
            { "1360x768", new Rect() {Right = 1360, Bottom = 768 } },
            { "1366x768 (16/9)", new Rect() {Right = 1366, Bottom = 768 } },
            { "1600x900 (16/9)", new Rect() {Right = 1600, Bottom = 900 } },
            { "1600x1024 (4/3)", new Rect() {Right = 1600, Bottom = 1024 } },
            { "1600x1200 (4/3)", new Rect() {Right = 1600, Bottom = 1200 } },
            { "1680x1050 (16/10)", new Rect() {Right = 1680, Bottom = 1050 } },
            { "1920x1080 (16/9)", new Rect() {Right = 1920, Bottom = 1080 } },
            { "1920x1200 (16/10)", new Rect() {Right = 1920, Bottom = 1200 } },
            { "1920x1440 (4/3)", new Rect() {Right = 1920, Bottom = 1440 } },
            { "2560x1440 (16/9)", new Rect() {Right = 2560, Bottom = 1440 } }
        };

        ///<inheritdoc/>
        public string NewWidth
        {
            get => _newWidth;
            set => SetProperty(ref _newWidth, value);
        }

        private string _newWidth = string.Empty;

        ///<inheritdoc/>
        public string NewHeight
        {
            get => _newHeight;
            set => SetProperty(ref _newHeight, value);
        }

        private string _newHeight = string.Empty;

        /// <summary>
        /// <see cref="List{Domain.Windows.Window}"/> containing all currently selected windows
        /// </summary>
        private List<Domain.Windows.Window> _selectedWindows = new List<Domain.Windows.Window>();

        /// <summary>
        /// <see cref="IEventAggregator"/> used to be notified when the selected windows has changed
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// <see cref="IWindowService"/> used to resize the selected windows
        /// </summary>
        private readonly IWindowService _windowService;

        /// <summary>
        /// Applies the custom <see cref="NewWidth"/> and <see cref="NewHeight"/> to all selected windows
        /// </summary>
        public DelegateCommand ApplyCustomResolutionCommand { get; }

        /// <summary>
        /// Applies a preset of <see cref="Resolutions"/> to all selected windows
        /// </summary>
        public DelegateCommand<Rect?> ApplyPresetCommand { get; }

        /// <summary>
        /// Create a new instance of <see cref="ResizerViewModel"/>
        /// </summary>
        /// <param name="eventAggregator"><see cref="IEventAggregator"/> used to be notified when the selected windows has changed</param>
        /// <param name="windowService"><see cref="IWindowService"/> used to resize the selected windows</param>
        public ResizerViewModel(IEventAggregator eventAggregator, IWindowService windowService)
        {
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<ActiveWindowsSelectionChangedEvent>().Subscribe(ActiveWindowsSelectionChanged);

            // Setup commands
            ApplyCustomResolutionCommand = new DelegateCommand(ApplyCustomResolution);
            ApplyPresetCommand = new DelegateCommand<Rect?>(ApplyPreset);
        }

        /// <summary>
        /// Resizes all selected window to the <see cref="NewWidth"/> and <see cref="NewHeight"/> resolution
        /// </summary>
        public void ApplyCustomResolution()
        {
            ResizeWindows(new Rect { Right = Convert.ToInt32(NewWidth), Bottom = Convert.ToInt32(NewHeight) });
        }

        /// <summary>
        /// Resizes all selected windows to the resolution of a given <see cref="Rect"/>
        /// </summary>
        /// <param name="preset"><see cref="Rect"/> containing the new resolution</param>
        public void ApplyPreset(Rect? preset)
        {
            if(preset.HasValue)
            {
                ResizeWindows(preset.Value);
            }
        }

        /// <summary>
        /// Resizes all selected windows 
        /// </summary>
        /// <param name="newResolution"><see cref="Rect"/> containing the new resolution</param>
        private void ResizeWindows(Rect newResolution)
        {
            foreach (var window in _selectedWindows)
            {
                //_windowService.ResizeWindow(window, newResolution);
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
