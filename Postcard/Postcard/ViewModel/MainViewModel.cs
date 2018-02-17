using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Postcard.FileSeletctors;
using BusinessLogic;
using BusinessLogic.ImageLoaders;

namespace Postcard.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IImageSelector _imageSelector;
        private readonly IImageLoader _baseImageLoader;

        public MainViewModel(IImageSelector imageSelector, IImageLoader baseImageLoader)
        {
            _imageSelector = imageSelector;
            _baseImageLoader = baseImageLoader;

            LoadBaseImageCommand = new RelayCommand(LoadBaseImage);
        }

        public ICommand LoadBaseImageCommand { get; }

        private void LoadBaseImage()
        {
            _imageSelector.SelectFile();

            if (!_imageSelector.IsImageSelected)
            {
                _baseImageLoader.Unload();
            }

            _baseImageLoader.Load(_imageSelector.SelectedImagePath);
        }
    }
}