using System.Reflection;
using Autofac;
using Resizer.Domain.Infrastructure.Messenger;

namespace Resizer.Gui
{
    /// <summary>
    /// Defines a class that registers all <see cref="Gui"/> dependencies
    /// </summary>
    public class UiModule : Autofac.Module
    {
        /// <summary>
        /// Register all <see cref="Gui"/> dependencies
        /// </summary>
        /// <param name="builder">Instance of the container builder to register the dependencies to</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventAggregator>().As<IEventAggregator>();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t => t.Name.EndsWith("ViewModel")).SingleInstance();
        }
    }
}
