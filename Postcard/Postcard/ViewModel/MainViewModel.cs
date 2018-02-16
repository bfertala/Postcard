using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Postcard.FileSeletctors;

namespace Postcard.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IImageSelector _imageSelector;
        private string _selectedBaseImagePath;

        public MainViewModel(IImageSelector imageSelector)
        {
            _imageSelector = imageSelector;
            _selectedBaseImagePath = string.Empty;

            SelectBaseImageCommand = new RelayCommand(SelectBaseImage);
        }

        public ICommand SelectBaseImageCommand { get; }

        public ICommand GenerateImageCommand { get; }

        private void SelectBaseImage()
        {
            _imageSelector.SelectFile();

            if (!_imageSelector.IsImageSelected)
            {
                _selectedBaseImagePath = string.Empty;
            }

            _selectedBaseImagePath = _imageSelector.SelectedImagePath;
        }
    }
}