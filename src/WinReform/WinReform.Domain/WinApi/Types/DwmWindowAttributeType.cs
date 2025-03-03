using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinReform.Domain.WinApi.Types
{
    /// <summary>
    /// Defines attributes for window properties retrieved using
    /// <see href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/nf-dwmapi-dwmgetwindowattribute">DwmGetWindowAttribute</see>.
    /// </summary>
    public enum DwmWindowAttributeType
    {
        /// <summary>
        /// Retrieves the visible window bounds, excluding invisible borders and shadows.
        /// </summary>
        ExtendedFrameBounds = 9,

        /// <summary>
        /// Retrieves the non-client rendering policy for the window.
        /// </summary>
        NonClientRenderingPolicy = 2,

        /// <summary>
        /// Retrieves whether non-client rendering is enabled.
        /// </summary>
        NonClientRenderingEnabled = 1,

        /// <summary>
        /// Retrieves the caption button bounds.
        /// </summary>
        CaptionButtonBounds = 5,

        /// <summary>
        /// Retrieves whether the window is cloaked (hidden for rendering purposes).
        /// </summary>
        Cloaked = 14,

        /// <summary>
        /// Retrieves the cloaking reasons for a window (if any).
        /// </summary>
        CloakReason = 15,

        /// <summary>
        /// Retrieves the DWM transition duration override.
        /// </summary>
        TransitionDurationOverride = 20,

        /// <summary>
        /// Retrieves whether the window has a non-client area that should be rendered.
        /// </summary>
        HasIconicBitmap = 10,

        /// <summary>
        /// Retrieves whether the window should be treated as a force iconic representation.
        /// </summary>
        ForceIconicRepresentation = 7,

        /// <summary>
        /// Retrieves the flip 3D policy for the window.
        /// </summary>
        Flip3DPolicy = 8,

        /// <summary>
        /// Retrieves whether DWM transitions are disabled.
        /// </summary>
        DisallowTransitions = 17,

        /// <summary>
        /// Retrieves the last active frame bounds.
        /// </summary>
        LastActiveFrameBounds = 16,

        /// <summary>
        /// Retrieves the window’s DWM-rendered thumbnail icon.
        /// </summary>
        ThumbnailIcon = 19,

        /// <summary>
        /// Retrieves the force disable transitions flag.
        /// </summary>
        ForceDisableTransitions = 18,

        /// <summary>
        /// Retrieves the width of the borders that DWM extends around a window.
        /// </summary>
        BorderWidth = 13
    }
}
