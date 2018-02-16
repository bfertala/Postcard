using Microsoft.Win32;

namespace Postcard.FileSeletctors
{
    public class ImageSelector : IImageSelector
    {
        public string SelectedImagePath { get; private set; }

        public bool IsImageSelected => !string.IsNullOrEmpty(SelectedImagePath);

        public ImageSelector()
        {
            SelectedImagePath = string.Empty;
        }

        public void SelectFile()
        {
            const string fileFilter = "Image Files|*.bmp;*.png;*.jpg;*.jpeg;*.gif;*.tif;*.tiff";

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = fileFilter,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SelectedImagePath = openFileDialog.FileName;
            }
            else
            {
                SelectedImagePath = string.Empty;
            }
        }

        
    }
}