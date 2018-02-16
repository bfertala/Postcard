namespace Postcard.FileSeletctors
{
    public interface IImageSelector
    {
        string SelectedImagePath { get; }

        bool IsImageSelected { get; }

        void SelectFile();
    }
}