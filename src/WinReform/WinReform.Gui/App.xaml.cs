using System;
using System.Windows;
using Autofac;
using WinReform.Gui.Infrastructure.Common.ViewModel;

namespace WinReform.Gui
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

            // Construct the DI
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DependencyModule());
            _container = builder.Build();

            // Setup the viewmodel locator
            ViewModelLocator.SetViewModelFactory(_container.Resolve<Func<Type, ViewModelBase>>());

            // Setup the main window
            var window = new MainWindow();
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
