using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace WinReform.Infrastructure.Extensions
{
    /// <summary>
    /// Defines a class that contains all the extensions for <see cref="ObservableCollection{T}"/>
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Updates a collection with a new list of items
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of the <see cref="ObservableCollection{T}"/></typeparam>
        /// <param name="collection"><see cref="ObservableCollection{T}"/> to be updated</param>
        /// <param name="newCollection"><see cref="IList"/> containing the items and order for the collection to be updated to</param>
        public static void UpdateCollection<T>(this ObservableCollection<T> collection, IList<T> newCollection)
        {
            if (newCollection == null || newCollection.Count == 0)
            {
                collection.Clear();
                return;
            }

            var i = 0;
            foreach (var item in newCollection)
            {
                if (collection.Count > i)
                {
                    var existingItem = collection.FirstOrDefault(existing => Comparer<T>.Default.Compare(existing, item) == 0);

                    if (existingItem == null! || existingItem.Equals(default(T)))
                    {
                        // Item doesn't exist in the collection, so insert it
                        collection.Insert(i, item);
                    }
                    else
                    {
                        // Get the index of the existing item, ensuring it's not null
                        var itemIndex = collection.IndexOf(existingItem);

                        if (itemIndex != i)
                        {
                            // Item exists but is in the wrong place, so move it
                            collection.Move(itemIndex, i);
                        }
                        else
                        {
                            // Check if the item has changed (considering nullability)
                            if (collection[i] is not null && item is not null && !collection[i]!.Equals(item))
                            {
                                // If the item has changed, copy properties
                                foreach (var sourceProperty in item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                                {
                                    var targetProperty = collection[i]?.GetType().GetProperty(sourceProperty.Name);

                                    if (targetProperty != null && targetProperty.CanWrite)
                                    {
                                        targetProperty.SetValue(collection[i], sourceProperty.GetValue(item, null), null);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Item doesn't exist in the collection, so add it
                    collection.Add(item);
                }

                i++;
            }

            // Remove any items left in the original collection that aren't in the new collection
            while (collection.Count > newCollection.Count)
            {
                collection.RemoveAt(i);
            }
        }
    }
}
