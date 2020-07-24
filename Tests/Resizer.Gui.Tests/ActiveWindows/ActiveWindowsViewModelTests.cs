using Resizer.Domain.Tests.Windows.Mocks;
using Resizer.Domain.Windows;
using Resizer.Gui.ActiveWindows;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Resizer.Gui.Tests.ActiveWindows
{
    /// <summary>
    /// Tests for the <see cref="ActiveWindowsViewModel"/>
    /// </summary>
    public class ActiveWindowsViewModelTests
    {
        #region Constructor Tests

        [Fact]
        public void Construct_NullWindowService_ShouldThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var viewModel = new ActiveWindowsViewModel(null!);
            });
        }

        [Fact]
        public void Construct_ValidArguments_ShouldConstructActiveWindowsViewModel()
        {
            // Prepare 
            var windowService = new WindowServiceMock();

            // Act
            var viewModel = new ActiveWindowsViewModel(windowService);

            // Assert
            Assert.NotNull(viewModel);
        }

        #endregion

        #region RefreshActiveWindows tests

        [Fact]
        public void RefreshActiveWindows_Execution_ShouldCallWindowService()
        {
            // Prepare 
            var windowService = new WindowServiceMock();
            var viewmodel = new ActiveWindowsViewModel(windowService);

            // Act
            windowService.GetActiveWindowsCalled = false;
            viewmodel.RefreshActiveWindows();

            // Assert
            Assert.True(windowService.GetActiveWindowsCalled);
        }

        #endregion
    }
}
