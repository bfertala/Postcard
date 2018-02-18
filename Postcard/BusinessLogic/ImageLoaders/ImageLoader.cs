using System.Drawing;

namespace BusinessLogic.ImageLoaders
{
    public class ImageLoader : IImageLoader
    {
        private readonly IImageFileLoader _imageFileLoader;

        public ImageLoader(IImageFileLoader imageFileLoader)
        {
            _imageFileLoader = imageFileLoader;
        }

        ~ImageLoader()
        {
            Unload();
        }

        public Image Image { get; private set; }

        public bool IsImageLoaded => Image != null;

        public void Load(string path)
        {
            Unload();

            Image = _imageFileLoader.Load(path);
        }

        public void Unload()
        {
            if (IsImageLoaded)
            {
                Image.Dispose();
                Image = null;
            }
        }
    }
}