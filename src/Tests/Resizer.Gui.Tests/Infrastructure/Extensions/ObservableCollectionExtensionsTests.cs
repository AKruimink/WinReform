using Resizer.Domain.Tests.Mocks;
using Resizer.Gui.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Resizer.Gui.Tests.Infrastructure.Extensions
{
    /// <summary>
    /// Tests for the <see cref="ObservableCollectionExtensions"/>
    /// </summary>
    public class ObservableCollectionExtensionsTests
    {
        #region UpdateCollection Tests

        [Fact]
        public void UpdateCollection_NewItem_ShouldAddItemToList()
        {
            // Prepare
            var collection = new ObservableCollection<ModelMock>();
            var model1 = new ModelMock { Id = 1 };

            // Assert
            Assert.DoesNotContain(collection, item => item.Id == 1);

            // Act
            collection.UpdateCollection(new List<ModelMock> { model1 });

            // Assert
            Assert.Contains(collection, item => item.Id == 1);
        }

        [Fact]
        public void UpdateCollection_MovedItem_ShouldMoveTheItemToCorrectIndex()
        {
            // Prepare
            var collection = new ObservableCollection<ModelMock>();
            var model1 = new ModelMock { Id = 1 };
            var model2 = new ModelMock { Id = 2 };
            var model3 = new ModelMock { Id = 3 };

            collection.Add(model1);
            collection.Add(model2);
            collection.Add(model3);

            var newCollection = new List<ModelMock> { model2, model1, model3 };

            // Assert
            Assert.NotEqual(newCollection, collection);

            // Act
            collection.UpdateCollection(newCollection);

            // Assert
            Assert.Equal(newCollection, collection);
        }

        [Fact]
        public void UpdateCollection_RemovedItem_ShouldRemoveItemFromList()
        {
            // Prepare
            var collection = new ObservableCollection<ModelMock>();
            var model1 = new ModelMock { Id = 1 };
            var model2 = new ModelMock { Id = 2 };
            var model3 = new ModelMock { Id = 3 };

            collection.Add(model1);
            collection.Add(model2);
            collection.Add(model3);

            var newCollection = new List<ModelMock> { model1, model2 };

            // Assert
            Assert.NotEqual(newCollection, collection);

            // Act
            collection.UpdateCollection(newCollection);

            // Assert
            Assert.Equal(newCollection, collection);
        }

        [Fact]
        public void UpdateCollection_UpdatedItem_ShouldUpdateItemInList()
        {
            // Prepare
            var collection = new ObservableCollection<ModelMock>();
            var model1 = new ModelMock { Id = 1, SomeText = "Old Text" };

            collection.Add(model1);

            model1.SomeText = "New Text";

            // Act
            collection.UpdateCollection(new List<ModelMock> { model1 });

            // Assert
            Assert.Equal(model1, collection[0]);
        }

        #endregion UpdateCollection Tests
    }
}