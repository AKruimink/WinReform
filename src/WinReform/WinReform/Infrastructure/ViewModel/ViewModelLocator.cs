using System;
using System.ComponentModel;
using System.Windows;

namespace WinReform.Infrastructure.Common.ViewModel
{
    public static class ViewModelLocator
    {
        /// <summary>
        /// <see cref="Func{T, TResult}"/> used instance a viewmodel
        /// </summary>
        private static Func<Type, ViewModelBase>? s_viewmodelFactory;

        /// <summary>
        /// Sets the viewmodel factory used to instance viewmodels
        /// </summary>
        /// <param name="factory"><see cref="Func{T, TResult}"/> used to instance new <see cref="ViewModelBase"/> objects</param>
        public static void SetViewModelFactory(Func<Type, ViewModelBase> factory)
        {
            s_viewmodelFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// The Wire ViewModel attached property
        /// </summary>
        public static DependencyProperty WireViewModelProperty = DependencyProperty.RegisterAttached("WireViewModel", typeof(Type), typeof(ViewModelLocator),
            new PropertyMetadata(defaultValue: null, propertyChangedCallback: WireViewModelChanged));

        /// <summary>
        /// Gets the <see cref="WireViewModelProperty"/> attached proprty value
        /// </summary>
        /// <param name="obj">The target element to set the attached property of</param>
        /// <returns>Returns <see cref="Type"/> of the currently set attached property value</returns>
        public static Type GetWireViewModel(DependencyObject obj)
        {
            return (Type)obj.GetValue(WireViewModelProperty);
        }

        /// <summary>
        /// Sets the <see cref="WireViewModelProperty"/> attached property value
        /// </summary>
        /// <param name="obj">The target element to set the attached property of</param>
        /// <param name="value">The attached property value to be set</param>
        public static void SetWireViewModel(DependencyObject obj, Type value)
        {
            obj.SetValue(WireViewModelProperty, value);
        }

        /// <summary>
        /// Raised when <see cref="WireViewModelProperty"/> changed and attempts to set the datacontext
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void WireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(d))
            {
                if ((Type)e.NewValue != (Type)e.OldValue)
                {
                    if (s_viewmodelFactory == null)
                    {
                        throw new NullReferenceException("The ViewModelLocator did not have it's factory method set");
                    }

                    var viewmodel = s_viewmodelFactory(GetWireViewModel(d));
                    Bind(d, viewmodel);
                }
            }
        }

        /// <summary>
        /// Sets the DataContext of a view
        /// </summary>
        /// <param name="view">The view to set the DataContext of</param>
        /// <param name="viewModel">The object to be set as the DataContext</param>
        private static void Bind(object view, object viewModel)
        {
            if (view is FrameworkElement element)
            {
                element.DataContext = viewModel;
            }
        }
    }
}
