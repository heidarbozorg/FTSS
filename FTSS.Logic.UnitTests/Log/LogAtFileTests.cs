using NUnit.Framework;
using Moq;

namespace FTSS.Logic.UnitTests.Log
{
    [TestFixture]    
    class LogAtFileTests
    {
        private string simpleMessage;
        Mock<System.IO.StreamWriter> _writer;
        private Logic.FileIO.TextFile _textFile;
        Logic.Log.LogAtFile _logger;

        [SetUp]
        public void Setup()
        {
            simpleMessage = "This is a simple message.";

            var stream = new System.IO.MemoryStream();          //Write on the fly
            _writer = new Mock<System.IO.StreamWriter>(stream);    //Creating new textFile writer mock

            //Create TextFile object from the mock
            _textFile = new Logic.FileIO.TextFile(_writer.Object);
            _logger = new Logic.Log.LogAtFile(_textFile);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Constructor_WhenPathIsEmpty_ThrowsNullException(string path)
        {
            Assert.That(() => new Logic.Log.LogAtFile(path),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_WhenWriterIsNull_ThrowsNullException()
        {
            Logic.FileIO.IFileOperation FileOperation = null;
            Assert.That(() => new Logic.Log.LogAtFile(FileOperation),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Add_WhenPassingNullException_ThrowsNullException()
        {
            System.Exception e = null;
            Assert.That(() => _logger.Add(e),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Append_WhenPassingEmptyMessage_ThrowsNullException(string msg)
        {
            Assert.That(() => _logger.Add(msg), Throws.ArgumentNullException);
        }

        [Test]
        public void Append_WhenPassingMessage_CallWriteLineOnce()
        {
            _logger.Add(simpleMessage);

            _writer.Verify(s => s.WriteLine(It.IsAny<string>()), Times.Once());
        }
    }
}
