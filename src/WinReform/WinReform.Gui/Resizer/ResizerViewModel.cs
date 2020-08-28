using System;
using System.Collections.Generic;
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

        ///<inheritdoc/>
        public List<Domain.Windows.Window> SelectedWindows { get; set; } = new List<Domain.Windows.Window>();

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
            int.TryParse(NewWidth, out var width);
            int.TryParse(NewHeight, out var height);

            ResizeWindows(new Rect { Right = width, Bottom = height });
        }

        /// <summary>
        /// Resizes all selected windows to the resolution of a given <see cref="Rect"/>
        /// </summary>
        /// <param name="preset"><see cref="Rect"/> containing the new resolution</param>
        public void ApplyPreset(Rect? preset)
        {
            if (preset.HasValue)
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
            foreach (var window in SelectedWindows)
            {
                _windowService.ResizeWindow(window, newResolution);
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
