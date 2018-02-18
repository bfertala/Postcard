using System;

namespace Postcard.ErrorHandlers
{
    public interface IErrorHandler
    {
        void Execute(Action action);
    }
}