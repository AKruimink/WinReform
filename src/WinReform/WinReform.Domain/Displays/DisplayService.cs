using System;
using System.Collections.Generic;
using WinReform.Domain.WinApi;

namespace WinReform.Domain.Displays
{
    /// <summary>
    /// Defines a class that acts as a service for managing displays
    /// </summary>
    public class DisplayService : IDisplayService
    {
        /// <summary>
        /// <see cref="IWinApiService"/> used to manage displays
        /// </summary>
        private readonly IWinApiService _winApiService;

        /// <summary>
        /// Create a new instance of <see cref="WindowService"/>
        /// </summary>
        /// <param name="winApiService">Instance of <see cref="IWinApiService"/> used to manage displays</param>
        public DisplayService(IWinApiService winApiService)
        {
            _winApiService = winApiService ?? throw new ArgumentNullException(nameof(winApiService));
        }

        /// <inheritdoc/>
        public List<Display> GetAllDisplays()
        {
            var displays = new List<Display>();

            foreach (var monitor in _winApiService.GetAllMonitors())
            {
                var display = new Display
                {
                    Id = displays.Count + 1, // ID also functions as the monitor order
                    DisplayHandle = monitor.MonitorHandle,
                    Primary = monitor.Flags != 0,
                    WorkArea = monitor.WorkArea
                };

                displays.Add(display);
            }

            return displays;
        }
    }
}
