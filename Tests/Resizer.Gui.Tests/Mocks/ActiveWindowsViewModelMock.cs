using Resizer.Gui.ActiveWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace Resizer.Gui.Tests.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IActiveWindowsViewModel"/>
    /// </summary>
    public class ActiveWindowsViewModelMock : IActiveWindowsViewModel
    {
        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> ActiveWindows { get; set; } = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public ObservableCollection<Domain.Windows.Window> SelectedActiveWindows { get; set; } = new ObservableCollection<Domain.Windows.Window>();

        ///<inheritdoc/>
        public string WindowFilter { get; set; } = string.Empty;

        ///<inheritdoc/>
        public ICollectionView FilteredActiveWindows { get; set; } 

        /// <summary>
        /// Create a new instance of <see cref="ActiveWindowsViewModelMock"/>
        /// </summary>
        public ActiveWindowsViewModelMock()
        {
            FilteredActiveWindows = CollectionViewSource.GetDefaultView(ActiveWindows);
        }
    }
}
