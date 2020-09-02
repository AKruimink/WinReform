using System;
using System.Collections.Generic;
using System.Text;

namespace WinReform.Domain.Infrastructure.Attributes
{
    /// <summary>
    /// Defines a class that acts as an attributes that defines the property name that is relied on for the <see cref="INotifyPropertyChanged"/> invocation
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DependsOnPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property that should be relied on
        /// </summary>
        public string DependencyProperty { get; }

        /// <summary>
        /// Create a new instance of <see cref="DependsOnPropertyAttribute"/>
        /// </summary>
        /// <param name="dependencyProperty">Name of the property that is relied on for the <see cref="INotifyPropertyChanged"/> invocation</param>
        public DependsOnPropertyAttribute(string dependencyProperty)
        {
            DependencyProperty = dependencyProperty;
        }
    }
}
