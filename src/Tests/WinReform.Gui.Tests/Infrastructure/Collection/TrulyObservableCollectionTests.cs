using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Gui.Infrastructure.Collection;
using WinReform.Tests.Fixtures;
using Xunit;

namespace WinReform.Gui.Tests.Infrastructure.Collection
{
    /// <summary>
    /// Tests for the <see cref="TrulyObservableCollection{T}"/>
    /// </summary>
    public class TrulyObservableCollectionTests
    {
        #region TrulyObservableCollectionCollectionChanged Tests

        [Fact]
        public void TrulyObservableCollectionCollectionChanged_NewItem_ShouldSubscribeToItemChange()
        {
            // Prepare
            var itemPropertyChangedFired = false;
            var modelListMock = new List<ModelFixture>
            {
                new ModelFixture { Id = 1, Number = 1, Text = "test" },
                new ModelFixture { Id = 2, Number = 2, Text = "test" },
            };

            var collection = new TrulyObservableCollection<ModelFixture>(modelListMock);
            collection.CollectionItemChanged += Collection_CollectionItemChanged;

            void Collection_CollectionItemChanged(object? sender, CollectionItemChangedEventArgs<ModelFixture> e)
            {
                itemPropertyChangedFired = true;
            }

            // Act
            modelListMock[1].Number = 10;

            // Assert
            Assert.True(itemPropertyChangedFired);
        }

        [Fact]
        public void TrulyObservableCollectionCollectionChanged_OldItem_ShouldUnsubscribeToItemChange()
        {
            // Prepare
            var itemPropertyChangedFired = false;
            var modelListMock = new List<ModelFixture>
            {
                new ModelFixture { Id = 1, Number = 1, Text = "test" },
                new ModelFixture { Id = 2, Number = 2, Text = "test" },
            };

            var collection = new TrulyObservableCollection<ModelFixture>(modelListMock);
            collection.CollectionItemChanged += Collection_CollectionItemChanged;

            void Collection_CollectionItemChanged(object? sender, CollectionItemChangedEventArgs<ModelFixture> e)
            {
                itemPropertyChangedFired = true;
            }

            // Act
            collection.Remove(modelListMock[1]);
            modelListMock[1].Number = 10;

            // Assert
            Assert.False(itemPropertyChangedFired);
        }

        #endregion
    }
}
