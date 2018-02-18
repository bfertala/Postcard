using System;
using System.Drawing;
using BusinessLogic.Exceptions;

namespace BusinessLogic.ImageLoaders
{
    public class ImageFileLoader : IImageFileLoader
    {
        public Image Load(string path)
        {
            try
            {
                return Image.FromFile(path);
            }
            catch (ArgumentException ex)
            {
                var exceptionMessage = $"Provided path: {path} is recognized as URI which is not allowed while loading an image file.";

                throw new UriInsteadOfLocalPathException(exceptionMessage, ex);
            }
        }
    }
}