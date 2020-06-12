using Resizer.Gui.Common.ViewModel;

namespace Resizer.Gui.Tests.Common.ViewModel.Mocks
{
    /// <summary>
    /// Defines a mock implementation of a view model 
    /// </summary>
    public class ViewModelMock : ViewModelBase
    {
        /// <summary>
        /// Random integer used for testing
        /// </summary>
        private int _intProperty;

        /// <summary>
        /// Gets or Sets the int property and triggers the <see cref="ViewModelBase"/> set property
        /// </summary>
        public int IntProperty
        {
            get => _intProperty;
            set => SetProperty(ref _intProperty, value);
        }

        /// <summary>
        /// Raises the <see cref="IntProperty"/>
        /// </summary>
        public void InvokeOnPropertyChanged()
        {
            RaisePropertyChanged(nameof(IntProperty));
        }
    }
}
