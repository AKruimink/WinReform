using Resizer.Gui.ActiveWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
}
