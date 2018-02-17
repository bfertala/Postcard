using System;
using System.Drawing;
using BusinessLogic.ImageLoaders;
using Moq;
using NUnit.Framework;

namespace BusinessLogicTests
{
    [TestFixture]
    public class ImageLoaderTests
    {
        private Mock<IImageFileLoader> _imageFileLoaderMock;
        private ImageLoader _imageLoader;

        [SetUp]
        public void SetUp()
        {
            _imageFileLoaderMock = new Mock<IImageFileLoader>();
            
            _imageLoader = new ImageLoader(_imageFileLoaderMock.Object);
        }

        [Test]
        public void ShouldReturnTrueIfImageIsLoaded()
        {
            // Given
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(CreateSampleImage());

            // When
            _imageLoader.Load("some/path/to/image.png");

            // Then
            Assert.IsTrue(_imageLoader.IsImageLoaded);
        }

        [Test]
        public void ShouldReturnFalseIfImageIsNotLoadedYet()
        {
            // Given

            // When

            // Then
            Assert.IsFalse(_imageLoader.IsImageLoaded);
        }

        [Test]
        public void ShouldReturnFalseIfImageIsUnloaded()
        {
            // Given
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(CreateSampleImage());

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader.Unload();

            // Then
            Assert.IsFalse(_imageLoader.IsImageLoaded);
        }

        [Test]
        public void ShouldReturnTrueIfAnotherImageIsLoaded()
        {
            // Given
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(CreateSampleImage());

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader.Load("some/path/to/image2.png");

            // Then
            Assert.IsTrue(_imageLoader.IsImageLoaded);
        }

        [Test]
        public void ShouldCallImageDisposeMethodWhenImageIsUnloaded()
        {
            // Given
            var image = CreateSampleImage();
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(image);

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader.Unload();

            // Then
            Assert.IsTrue(IsImageDisposed(image));
        }

        [Test]
        public void ShouldCallImageDisposeMethodWhenAnotherImageIsLoaded()
        {
            // Given
            var image = CreateSampleImage();
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(image);

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader.Load("some/path/to/image2.png");

            // Then
            Assert.IsTrue(IsImageDisposed(image));
        }

        [Test]
        public void ShouldNotCallImageDisposeMethodWhenNoImageIsLoaded()
        {
            // Given
            var image = CreateSampleImage();
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(image);

            // When
            _imageLoader.Unload();

            // Then
            Assert.IsFalse(IsImageDisposed(image));
        }

        [Test]
        public void ShouldCallImageDisposeMethodWhenLoaderIsCollectedByGarbageCollectorAndImageIsLoaded()
        {
            // Given
            var image = CreateSampleImage();
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(image);

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader = null;

            RunGarbageCollector();

            // Then
            Assert.IsTrue(IsImageDisposed(image));
        }

        [Test]
        public void ShouldReturnNullIfNoImageIsLoaded()
        {
            // Given

            // When

            // Then
            Assert.IsNull(_imageLoader.Image);
        }

        [Test]
        public void ShouldReturnNullIfImageIsUnloaded()
        {
            // Given
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(CreateSampleImage());

            // When
            _imageLoader.Load("some/path/to/image.png");
            _imageLoader.Unload();

            // Then
            Assert.IsNull(_imageLoader.Image);
        }

        [Test]
        public void ShouldReturnLoadedImage()
        {
            // Given
            var image = CreateSampleImage();
            _imageFileLoaderMock.Setup(i => i.Load(It.IsAny<string>())).Returns(image);

            // When
            _imageLoader.Load("some/path/to/image.png");

            // Then
            Assert.AreSame(image, _imageLoader.Image);
        }

        [Test]
        public void ShouldReturnLatestLoadedImage()
        {
            // Given
            var image1 = CreateSampleImage();
            var image1Path = "image1.png";
            var image2 = CreateSampleImage();
            var image2Path = "image2.jpg";

            _imageFileLoaderMock.Setup(i => i.Load(image1Path)).Returns(image1);
            _imageFileLoaderMock.Setup(i => i.Load(image2Path)).Returns(image2);

            // When
            _imageLoader.Load(image1Path);
            _imageLoader.Load(image2Path);

            // Then
            Assert.AreSame(image2, _imageLoader.Image);
        }

        private Image CreateSampleImage()
        {
            return new Bitmap(2, 2);
        }

        private bool IsImageDisposed(Image image)
        {
            try
            {
                // If the image is disposed, it throws a random exception while executing operation below
                var x = image.FrameDimensionsList;

                return false;
            }
            catch (ArgumentException)
            {
                return true;
            }
        }

        private void RunGarbageCollector()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
