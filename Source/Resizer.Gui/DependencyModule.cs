using System.Reflection;
using Autofac;
using Resizer.Domain.Infrastructure.Messenger;
using Resizer.Domain.Settings;

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

            return builder;
        }

        /// <summary>
        /// Register all <see cref="Gui"/> dependencies
        /// </summary>
        /// <param name="builder">Instance of the container builder to register the dependencies to</param>
        /// <returns>Returns <see cref="ContainerBuilder"/> with the newly added <see cref="Gui"/> dependencies</returns>
        private ContainerBuilder RegisterGuiDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("ViewModel")).InstancePerDependency();

            return builder;
        }
    }
}
