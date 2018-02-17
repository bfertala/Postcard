using BusinessLogic;
using BusinessLogic.ImageLoaders;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Postcard.FileSeletctors;

namespace Postcard.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IImageSelector, ImageSelector>();
            SimpleIoc.Default.Register<IImageFileLoader, ImageFileLoader>();
            SimpleIoc.Default.Register<IImageLoader, ImageLoader>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        public static void Cleanup()
        {
        }
    }
}