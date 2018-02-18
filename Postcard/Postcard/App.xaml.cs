using System.Windows;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Threading;

namespace Postcard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

        private void HandleUnhandledExceptions(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"An unexpected error occurred: {e.Exception.Message}");
            e.Handled = true;
        }
    }
}
