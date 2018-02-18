using System;
using System.Collections.Generic;

namespace Postcard.ErrorHandlers
{
    public class MessageDisplayingErrorHandler : IErrorHandler
    {
        private Dictionary<string, string> _supportedExceptions;

        public MessageDisplayingErrorHandler()
        {
            _supportedExceptions = new Dictionary<string, string>();
        }

        public void Execute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (!IsExceptionSupported(ex))
                {
                    throw;
                }

                DisplayErrorMessage(ex);
            }
        }

        private bool IsExceptionSupported(Exception exception)
        {
            return _supportedExceptions.ContainsKey(GetExceptionTypeName(exception));
        }

        private string GetExceptionTypeName(Exception exception)
        {
            return exception.GetType().Name;
        }

        private string GetExceptionTypeName<T>() where T : Exception
        {
            return typeof(T).Name;
        }

        private void DisplayErrorMessage(Exception exception)
        {
            var exceptionMessage = _supportedExceptions[GetExceptionTypeName(exception)];
        }
    }
}