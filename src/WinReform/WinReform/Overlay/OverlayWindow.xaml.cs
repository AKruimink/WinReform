using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using MahApps.Metro.Controls;
using WinReform.Domain.Windows;

namespace WinReform.Overlay
{
    /// <summary>
    /// Represents an overlay window that visually highlights a selected window.
    /// </summary>
    public partial class OverlayWindow : MetroWindow
    {
        /// <summary>
        /// The target window that this overlay is tracking.
        /// </summary>
        private Domain.Windows.Window _targetWindow;

        /// <summary>
        /// The overlay window that visually represents the target window.
        /// </summary>
        private Domain.Windows.Window _overlayWindow;

        /// <summary>
        /// <see cref="IWindowService"/> used to get all active windows
        /// </summary>
        private readonly IWindowService _windowService;

        public OverlayWindow(Domain.Windows.Window targetWindow, IWindowService windowService)
        {
            InitializeComponent();

            _targetWindow = targetWindow ?? throw new ArgumentNullException(nameof(targetWindow));
            _windowService = windowService ?? throw new ArgumentNullException(nameof(windowService));

            Background = Brushes.Transparent;
            BorderBrush = Brushes.Red;
            BorderThickness = new Thickness(3);
            AllowsTransparency = true;
            Topmost = true;

            Loaded += OverlayWindow_Loaded;
        }

        /// <summary>
        /// Handles the <see cref="Loaded"/> event to initialize the overlay window.
        /// </summary>
        private void OverlayWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _overlayWindow = new Domain.Windows.Window
            {
                Description = $"{_targetWindow.Description} Overlay",
                WindowTitle = $"{_targetWindow.WindowTitle} Overlay",
                Dimensions = _targetWindow.Dimensions,
                WindowHandle = new WindowInteropHelper(this).Handle
            };

            HookWindowMoveEvent();
            EnableClickThrough();
            UpdatePosition();
        }

        /// <summary>
        /// Enables click-through behavior for the overlay, making it non-interactable.
        /// </summary>
        private void EnableClickThrough()
        {
            _windowService.EnableClickThrough(_overlayWindow);
        }

        /// <summary>
        /// Updates the overlay's position to match the target window.
        /// This method applies DPI scaling to ensure proper alignment.
        /// </summary>
        private void UpdatePosition()
        {
            // Retrieve the latest position and size of the target window
            _targetWindow = _windowService.UpdateWindow(_targetWindow);

            // Convert from WinAPI screen coordinates to WPF logical coordinates
            var transform = PresentationSource.FromVisual(this)?.CompositionTarget?.TransformFromDevice ?? Matrix.Identity;
            var topLeft = transform.Transform(new Point(_targetWindow.Dimensions.Left, _targetWindow.Dimensions.Top));
            var bottomRight = transform.Transform(new Point(_targetWindow.Dimensions.Right, _targetWindow.Dimensions.Bottom));

            var transformedRect = new Domain.WinApi.Rect((int)topLeft.X, (int)topLeft.Y, (int)(bottomRight.X - topLeft.X), (int)(bottomRight.Y - topLeft.Y));

            _windowService.UpdateOverlayPosition(_overlayWindow, transformedRect);
        }

        /// <summary>
        /// Registers a hook to track movement events of the target window.
        /// When the window moves, the overlay position is updated accordingly.
        /// </summary>
        private void HookWindowMoveEvent()
        {
            _windowService.HookWindowMoveEvent(_targetWindow, () => UpdatePosition());
        }

        /// <summary>
        /// Handles cleanup when the overlay window is closed.
        /// This ensures that the movement tracking hook is unregistered.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _windowService.UnhookWindowMoveEvent(_targetWindow);
        }
    }
}
