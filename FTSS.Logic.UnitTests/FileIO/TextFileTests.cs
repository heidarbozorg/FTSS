using NUnit.Framework;
using Moq;

namespace FTSS.Logic.UnitTests.FileIO
{
    [TestFixture]
    class TextFileTests
    {        
        private string simpleMessage;
        private Logic.FileIO.TextFile _textFile;
        Mock<System.IO.StreamWriter> _writer;

        [SetUp]
        public void Setup()
        {
            simpleMessage = "This is a simple message.";
            
            var stream = new System.IO.MemoryStream();          //Write on the fly
            _writer = new Mock<System.IO.StreamWriter>(stream);    //Creating new textFile writer mock

            //Create TextFile object from the mock
            _textFile = new Logic.FileIO.TextFile(_writer.Object);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Constructor_WhenPathIsEmpty_ThrowsNullException(string path)
        {
            Assert.That(() => new Logic.FileIO.TextFile(path),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_WhenWriterIsNull_ThrowsNullException()
        {
            System.IO.StreamWriter Writer = null;
            Assert.That(() => new Logic.FileIO.TextFile(Writer),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Append_WhenPassingEmptyMessage_ThrowsNullException(string msg)
        {
            Assert.That(() => _textFile.Append(msg), Throws.ArgumentNullException);
        }

        [Test]
        public void Append_WhenPassingMessage_CallWriteLine()
        {
            _textFile.Append(simpleMessage);

            _writer.Verify(s => s.WriteLine(simpleMessage));
        }
    }
}
