using System.Drawing;

namespace BusinessLogic.ImageLoaders
{
    public class ImageFileLoader : IImageFileLoader
    {
        public Image Load(string path)
        {
            return Image.FromFile(path);
        }
    }
}