using System.Collections.Generic;
using WinReform.Domain.WinApi;

namespace WinReform.Resizer
{
    /// <summary>
    /// Defines a class that represents a preset resolution for a window
    /// </summary>
    public class Resolution
    {
        /// <summary>
        /// Gets <see cref="Dictionary{string, Rect}"/> containing all resolution presets
        /// </summary>
        public static Dictionary<string, Rect> Resolutions { get; } = new Dictionary<string, Rect>()
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
    }
}
