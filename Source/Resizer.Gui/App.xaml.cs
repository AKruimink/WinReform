using System;
using System.Windows;
using Autofac;
using Autofac.Core;
using Resizer.Gui.Infrastructure.Common.ViewModel;
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

            // Construct the DI
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DependencyModule());
            _container = builder.Build();

            // Setup the viewmodel locator
            ViewModelLocator.SetViewModelFactory((type) =>
            {
                if(!type.IsAssignableTo<ViewModelBase>())
                {
                    throw new InvalidOperationException("The type provided to the ViewModel factory resolver can not be cast to ViewModelBase");
                }

                return _container.ResolveKeyed<ViewModelBase>(type);
                //return _container.Resolve(type) as ViewModelBase;
            });

            // Setup the main window
            using var scope = _container.BeginLifetimeScope();
            var window = new MainWindow();
            //{
            //    DataContext = scope.Resolve(typeof(WindowViewModel))
            //};
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
