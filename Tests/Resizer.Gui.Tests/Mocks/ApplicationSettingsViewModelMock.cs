using Resizer.Gui.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resizer.Gui.Tests.Mocks
{
    /// <summary>
    /// Mock implementation of <see cref="IApplicationSettingsViewModel"/>
    /// </summary>
    public class ApplicationSettingsViewModelMock : IApplicationSettingsViewModel
    {
        ///<inheritdoc/>
        public bool UseDarkTheme { get; set; }

        ///<inheritdoc/>
        public bool MinimizeOnClose { get; set; }
    }
}
