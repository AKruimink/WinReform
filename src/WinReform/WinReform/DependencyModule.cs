using System;
using System.Linq;
using System.Reflection;
using Autofac;
using WinReform.Domain.Displays;
using WinReform.Domain.Infrastructure.Messenger;
using WinReform.Domain.Process;
using WinReform.Domain.Settings;
using WinReform.Domain.WinApi;
using WinReform.Domain.Windows;
using WinReform.Infrastructure.Common.ViewModel;

namespace WinReform
{
    /// <summary>
    /// Defines a class that registers all <see cref="Gui"/> dependencies
    /// </summary>
    public class DependencyModule : Autofac.Module
    {
        /// <summary>
        /// Register all application dependencies
        /// </summary>
        /// <param name="builder">Instance of the container builder to register the dependencies to</param>
        protected override void Load(ContainerBuilder builder)
        {
            RegisterDomainDependencies(builder);
            RegisterGuiDependencies(builder);
        }

        /// <summary>
        /// Register all <see cref="Domain"/> dependencies
        /// </summary>
        /// <param name="builder">Instance of the container builder to register the dependencies to</param>
        /// <returns>Returns <see cref="ContainerBuilder"/> with the newly added <see cref="Domain"/> dependencies</returns>
        private ContainerBuilder RegisterDomainDependencies(ContainerBuilder builder)
        {
            // Events
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            // Settings
            builder.RegisterType<SettingStore>().As<ISettingStore>().InstancePerDependency();
            builder.RegisterType<SettingFactory>().As<ISettingFactory>().SingleInstance();
            // TODO: implement generic when Autofac v6 releases
            // https://github.com/autofac/Autofac/pull/1191
            builder.Register(x => x.Resolve<ISettingFactory>().Create<ApplicationSettings>()).As<ISetting<ApplicationSettings>>();

            // Windows
            builder.RegisterType<WindowService>().As<IWindowService>().InstancePerDependency();

            // Display
            builder.RegisterType<DisplayService>().As<IDisplayService>().InstancePerDependency();

            // WinApi
            builder.RegisterType<WinApiService>().As<IWinApiService>().InstancePerDependency();

            // Process
            builder.RegisterType<ProcessService>().As<IProcessService>().InstancePerDependency();

            return builder;
        }

        /// <summary>
        /// Register all <see cref="Gui"/> dependencies
        /// </summary>
        /// <param name="builder">Instance of the container builder to register the dependencies to</param>
        /// <returns>Returns <see cref="ContainerBuilder"/> with the newly added <see cref="Gui"/> dependencies</returns>
        private ContainerBuilder RegisterGuiDependencies(ContainerBuilder builder)
        {
            // Viewmodels
            var viewModels = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.Name.EndsWith("ViewModel"));
            foreach (var viewmodel in viewModels)
            {
                builder.RegisterType(viewmodel).Keyed<ViewModelBase>(viewmodel).InstancePerDependency();
            }

            builder.Register<Func<Type, ViewModelBase>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return (type) => context.ResolveKeyed<ViewModelBase>(type);
            });

            return builder;
        }
    }
}
