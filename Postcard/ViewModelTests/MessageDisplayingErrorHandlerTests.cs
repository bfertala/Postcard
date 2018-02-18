using System;
using NUnit.Framework;
using Postcard.ErrorHandlers;

namespace ViewModelTests
{
    public class MessageDisplayingErrorHandlerTests
    {
        private MessageDisplayingErrorHandler _errorHandler;

        [SetUp]
        public void SetUp()
        {
            _errorHandler = new MessageDisplayingErrorHandler();
        }

        [Test]
        public void ShouldRethrowUnsupportedException()
        {
            // Given
            Action actionWithUnsupportedException = () => throw new Exception();

            // When

            // Then
            Assert.Throws<Exception>(() => _errorHandler.Execute(actionWithUnsupportedException));
        }

        [Test]
        public void ShouldPreserveOriginalMessageIfExceptionIsNotSupported()
        {
            // Given
            string exceptionMessage = "Some very important message";
            Action actionWithUnsupportedException = () => throw new Exception(exceptionMessage);

            // When

            // Then
            var exception = Assert.Throws<Exception>(() => _errorHandler.Execute(actionWithUnsupportedException));
            Assert.AreEqual(exceptionMessage, exception.Message);
        }
    }
}