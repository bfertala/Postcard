using System.Drawing;

namespace BusinessLogic.ImageLoaders
{
    public interface IImageFileLoader
    {
        Image Load(string imagePath);
    }
}