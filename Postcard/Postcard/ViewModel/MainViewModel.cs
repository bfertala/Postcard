using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Postcard.FileSeletctors;
using BusinessLogic.ImageLoaders;
using Postcard.ErrorHandlers;

namespace Postcard.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IImageSelector _imageSelector;
        private readonly IImageLoader _baseImageLoader;
        private readonly IErrorHandler _errorHandler;

        public MainViewModel(IImageSelector imageSelector, IImageLoader baseImageLoader, IErrorHandler errorHandler)
        {
            _imageSelector = imageSelector;
            _baseImageLoader = baseImageLoader;
            _errorHandler = errorHandler;

            LoadBaseImageCommand = new RelayCommand(() => _errorHandler.Execute(LoadBaseImage));
        }

        public ICommand LoadBaseImageCommand { get; }

        private void LoadBaseImage()
        {
            _imageSelector.SelectFile();

            if (!_imageSelector.IsImageSelected)
            {
                return;
            }

            _baseImageLoader.Load(_imageSelector.SelectedImagePath);
        }
    }
}