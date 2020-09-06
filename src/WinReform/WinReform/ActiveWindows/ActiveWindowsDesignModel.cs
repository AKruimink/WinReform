using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using WinReform.Domain.WinApi;
using WinReform.Infrastructure.Extensions;

namespace WinReform.ActiveWindows
{
    /// <summary>
    /// Defines a class that provides active windows running on the system and management of said windows during design time
    /// </summary>
    public class ActiveWindowsDesignModel : IActiveWindowsViewModel
    {
        ///<inheritdoc/>
        public bool DisplayLocation { get; set; }

        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> ActiveWindows { get; set; } = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> SelectedActiveWindows { get; set; } = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public string WindowFilter { get; set; } = string.Empty;

        ///<inheritdoc/>
        public ICollectionView FilteredActiveWindows { get; set; }

        /// <summary>
        /// Create a new instance of the <see cref="ActiveWindowsDesignModel"/>
        /// </summary>
        public ActiveWindowsDesignModel()
        {
            DisplayLocation = false;

            var windows = new List<Domain.Windows.Window>
            {
                new Domain.Windows.Window
                {
                    Id = 1,
                    WindowHandle = (IntPtr)1,
                    Description = "Window 1",
                    Dimensions = new Rect{ Left = 0, Top = 0, Right = 100, Bottom = 100}
                },
                new Domain.Windows.Window
                {
                    Id = 2,
                    WindowHandle = (IntPtr)2,
                    Description = "Window 2",
                    Dimensions = new Rect{ Left = 0, Top = 0, Right = 200, Bottom = 200}
                },
                new Domain.Windows.Window
                {
                    Id = 2,
                    WindowHandle = (IntPtr)2,
                    Description = "Trailing Window Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test Test",
                    Dimensions = new Rect{ Left = 0, Top = 0, Right = 999999999, Bottom = 999999999}
                },
            };

            ActiveWindows.UpdateCollection(windows);
            SelectedActiveWindows.UpdateCollection(new List<Domain.Windows.Window> { windows[1] });

            FilteredActiveWindows = CollectionViewSource.GetDefaultView(ActiveWindows);
        }
    }
}
