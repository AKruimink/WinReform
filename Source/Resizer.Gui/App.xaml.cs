using System.Windows;
using Autofac;
using Resizer.Gui.Window;

namespace Resizer.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// All components  created by the <see cref="ContainerBuilder"/>
        /// </summary>
        private IContainer? _container;

        /// <summary>
        /// Creates all the required objects on startup
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ContainerBuilder();
            builder.RegisterModule(new UiModule());
            _container = builder.Build();

            using var scope = _container.BeginLifetimeScope();
            var window = new MainWindow()
            {
                DataContext = scope.Resolve<WindowViewModel>()
            };
            window.Show();
        }

        /// <summary>
        /// Disposes of all unmanaged objects on exit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            _container?.Dispose();
            base.OnExit(e);
        }
    }
}
