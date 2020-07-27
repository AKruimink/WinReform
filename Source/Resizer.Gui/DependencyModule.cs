using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Settings;
using Resizer.Domain.Windows;
using Resizer.Gui.Infrastructure.Common.ViewModel;

namespace Resizer.Gui
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

            // Windows
            builder.RegisterType<WindowService>().As<IWindowService>().InstancePerDependency();

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
            foreach(var viewmodel in viewModels)
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
