using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Resizer.Gui.Infrastructure.Extensions
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
            if(newCollection == null || newCollection.Count == 0)
            {
                collection.Clear();
                return;
            }

            var i = 0;
            foreach(var item in newCollection)
            {
                if(collection.Count > i)
                {
                    var itemIndex = collection.IndexOf(collection.Where(i => Comparer<T>.Default.Compare(i, item) == 0).FirstOrDefault());

                    if (itemIndex < 0)
                    {
                        // Item doesn't exist
                        collection.Insert(i, item);
                    }
                    else if (itemIndex > i || itemIndex < i)
                    {
                        // Item exists, but has moved up or down
                        collection.Move(itemIndex, i);
                    }
                    else
                    {
                        if (Comparer<T>.Default.Compare(collection[itemIndex], item) == 1)
                        {
                            // Item has changed, replace it
                            collection.Insert(itemIndex, item);
                        }
                    }
                }
                else
                {
                    // Item doesn't exist
                    collection.Add(item);
                }

                i++;
            }

            // Remove all old items
            while(collection.Count > newCollection.Count)
            {
                collection.RemoveAt(i);
            }
        }
    }
}
