using WinReform.Gui.ActiveWindows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace WinReform.Gui.Tests.ActiveWindows.Mocks
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

        ///<inheritdoc/>
        public bool ShouldAutomaticallyRefresh { get; set; }

        /// <summary>
        /// Create a new instance of <see cref="ActiveWindowsViewModelMock"/>
        /// </summary>
        public ActiveWindowsViewModelMock()
        {
            FilteredActiveWindows = CollectionViewSource.GetDefaultView(ActiveWindows);
        }
    }
}