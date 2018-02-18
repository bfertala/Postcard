using System;
using System.Collections.Generic;
using System.IO;
using BusinessLogic.Exceptions;
using Postcard.Properties;

namespace Postcard.ErrorHandlers
{
    public class MessageDisplayingErrorHandler : IErrorHandler
    {
        private readonly IMessageDisplayer _messageDisplayer;
        private Dictionary<string, string> _supportedExceptions;

        public MessageDisplayingErrorHandler(IMessageDisplayer messageDisplayer)
        {
            _messageDisplayer = messageDisplayer;
            _supportedExceptions = new Dictionary<string, string>
            {
                {GetExceptionTypeName<OutOfMemoryException>(), Resources.OutOfMemoryMessage},
                {GetExceptionTypeName<FileNotFoundException>(), Resources.FileNotFoundMessage},
                {GetExceptionTypeName<UriInsteadOfLocalPathException>(), Resources.UriInsteadOfLocalPathMessage}
            };
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

            _messageDisplayer.Display(exceptionMessage);
        }
    }
}