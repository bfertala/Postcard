using System;
using System.IO;
using BusinessLogic.Exceptions;
using Moq;
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
            var messageDisplayerMock = new Mock<IMessageDisplayer>();
            messageDisplayerMock.Setup(m => m.Display(It.IsAny<string>()));

            _errorHandler = new MessageDisplayingErrorHandler(messageDisplayerMock.Object);
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

        [Test]
        public void ShouldSupportOutOfMemoryException()
        {
            // Given
            Action actionWithOutOfMemoryException = () => throw new OutOfMemoryException();

            // When

            // Then
            Assert.DoesNotThrow(() => _errorHandler.Execute(actionWithOutOfMemoryException));
        }

        [Test]
        public void ShouldSupportFileNotFoundException()
        {
            // Given
            Action actionWithFileNotFoundException = () => throw new FileNotFoundException();

            // When

            // Then
            Assert.DoesNotThrow(() => _errorHandler.Execute(actionWithFileNotFoundException));
        }

        [Test]
        public void ShouldSupportUriInsteadOfLocalPathException()
        {
            // Given
            Action actionWithUriInsteadOfLocalPathException = () => throw new UriInsteadOfLocalPathException();

            // When

            // Then
            Assert.DoesNotThrow(() => _errorHandler.Execute(actionWithUriInsteadOfLocalPathException));
        }
    }
}