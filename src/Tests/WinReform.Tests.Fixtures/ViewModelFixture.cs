using System;
using System.Collections.Generic;
using System.Text;
using WinReform.Gui.Infrastructure.Common.ViewModel;

namespace WinReform.Tests.Fixtures
{
    /// <summary>
    /// Defines a class that represents a viewmodel fixture that provides fake viewmodel properties for testing puprose
    /// </summary>
    public class ViewModelFixture : ViewModelBase
    {
        /// <summary>
        /// Gets or Sets a test text 
        /// </summary>
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private string _text;

        /// <summary>
        /// Gets or Sets a test number
        /// </summary>
        public int Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }

        private int _number;
    }
}
