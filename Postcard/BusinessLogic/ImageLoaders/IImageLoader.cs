using System.Drawing;

namespace BusinessLogic.ImageLoaders
{
    public interface IImageLoader
    {
        Image Image { get; }

        bool IsImageLoaded { get; }

        void Load(string path);

        void Unload();
    }
}
