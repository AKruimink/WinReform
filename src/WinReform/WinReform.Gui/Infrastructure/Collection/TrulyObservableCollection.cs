using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace WinReform.Gui.Infrastructure.Collection
{
    /// <summary>
    /// Defines a class that acts as an <see cref="ObservableCollection"/> that also responds to collection item changes
    /// </summary>
    /// <typeparam name="T"><see cref="Type"/> of the <see cref="ObservableCollection{T}"/></typeparam>
    public sealed class TrulyObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        /// <summary>
        /// Occures when an item inside the collection has changed
        /// </summary>
        public event EventHandler<CollectionItemChangedEventArgs<T>>? CollectionItemChanged;

        /// <summary>
        /// Create a new instance of <see cref="TrulyObservableCollection{T}"/>
        /// </summary>
        public TrulyObservableCollection()
        {
            CollectionChanged += TrulyObservableCollectionCollectionChanged;
        }

        /// <summary>
        /// Create a new instance of <see cref="TrulyObservableCollection{T}"/>
        /// </summary>
        /// <param name="newCollection"><see cref="IEnumerable{T}"/> containing the to be added to the new collection</param>
        public TrulyObservableCollection(IEnumerable<T> newCollection) : this()
        {
            foreach (var item in newCollection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Invoked when the collection changed
        /// </summary>
        /// <param name="sender"><see cref="object"/> that changed</param>
        /// <param name="e">Data about the event</param>
        private void TrulyObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is INotifyPropertyChanged inpc)
                    {
                        inpc.PropertyChanged += ItemPropertyChanged;
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is INotifyPropertyChanged inpc)
                    {
                        inpc.PropertyChanged -= ItemPropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Invoked when an item inside the collection cahanged
        /// </summary>
        /// <param name="sender">The item that changed</param>
        /// <param name="e">Data about the event</param>
        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var args = new CollectionItemChangedEventArgs<T>((T)sender, e.PropertyName);
            CollectionItemChanged?.Invoke(this, args);
        }
    }

    /// <summary>
    /// Defines a class that acts as the arguments passed when the item of a collection changed
    /// </summary>
    /// <typeparam name="T"><see cref="Type"/> of the item that changed</typeparam>
    public class CollectionItemChangedEventArgs<T>
    {
        /// <summary>
        /// Gets the instance of <see cref="T"/> that changed
        /// </summary>
        public T ChangedItem { get; }

        /// <summary>
        /// Gets the name of the Property that changed
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Create a new instance of <see cref="CollectionItemChangedEventArgs{T}"/>
        /// </summary>
        /// <param name="item"><see cref="T"/> of the item that changed</param>
        /// <param name="propertyName">Name of the property within the item that changed</param>
        public CollectionItemChangedEventArgs(T item, string propertyName)
        {
            ChangedItem = item;
            PropertyName = propertyName;
        }
    }
}
