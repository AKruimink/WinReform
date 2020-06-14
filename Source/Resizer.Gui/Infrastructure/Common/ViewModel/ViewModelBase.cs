using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Resizer.Gui.Infrastructure.Common.ViewModel
{
    /// <summary>
    /// Base viewmodel that implements <see cref="INotifyPropertyChanged"/> to simplify viewmodels
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Occures when a property value changes
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the property changed event
        /// </summary>
        /// <param name="args"><see cref="PropertyChangedEventArgs"/></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        /// <summary>
        /// Sets a property and notifies it's listeners
        /// NOTE: only sets and notifies if the value doesnt match the desired value
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="storage">Reference to a property with getter and setter</param>
        /// <param name="value">Desired value for the property</param>
        /// <param name="propertyName">Name of the property used to notify listeners</param>
        /// <returns>Returns <see langword="true"/> if the value was changed, otherwise <see langword="false"/></returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Sets a property and notifies it's listeners
        /// NOTE: only sets and notifies if the value doesnt match the desired value
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="storage">Reference to a property with getter and setter</param>
        /// <param name="value">Desired value for the property</param>
        /// <param name="onChanged">Action to be called after the property value has changed</param>
        /// <param name="propertyName">Name of the property used to notify listeners</param>
        /// <returns>Returns <see langword="true"/> if the value was changed, otherwise <see langword="false"/></returns>
        protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property to notify the listeners of</param>
        protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

    }
}
