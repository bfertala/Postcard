using System.Windows;

namespace Postcard.ErrorHandlers
{
    public class ErrorMessageDisplayer : IMessageDisplayer
    {
        public void Display(string message)
        {
            const string error = "Error";

            MessageBox.Show(message, error, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}