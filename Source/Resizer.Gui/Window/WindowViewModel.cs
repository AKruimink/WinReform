using System.Diagnostics;
using System.Reflection;
using Resizer.Gui.Infrastructure.Common.Command;
using Resizer.Gui.Infrastructure.Common.ViewModel;

namespace Resizer.Gui.Window
{
    /// <summary>
    /// Defines a class that provides and handles application information
    /// </summary>
    public class WindowViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets the version of the application
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// Gets or Sets the state of the menu
        /// </summary>
        public bool MenuIsOpen
        {
            get => _menuIsOpen;
            set => SetProperty(ref _menuIsOpen, value);
        }

        private bool _menuIsOpen;

        /// <summary>
        /// Shows the project source code on Github
        /// </summary>
        public DelegateCommand ShowSourceOnGithubCommand { get; }

        /// <summary>
        /// Shows the project versions on Github
        /// </summary>
        public DelegateCommand ShowVersionsOnGithubCommand { get; }

        /// <summary>
        /// Create a new instance of the <see cref="WindowViewModel"/>
        /// </summary>
        public WindowViewModel()
        {
            Version = $"v:{Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString(3)}";

            ShowSourceOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WindowResizer",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });

            ShowVersionsOnGithubCommand = new DelegateCommand(() =>
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "https://github.com/AKruimink/WindowResizer/releases",
                    UseShellExecute = true
                };
                Process.Start(psi);
            });
        }
    }
}
