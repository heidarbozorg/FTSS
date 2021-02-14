using NUnit.Framework;
using Moq;

namespace FTSS.Logic.UnitTests.FileIO
{
    [TestFixture]
    class TextFileTests
    {
        private Logic.FileIO.TextFile textFile;
        private string simpleMessage;
        private string path;
        Mock<System.IO.StreamWriter> file;

        [SetUp]
        public void Setup()
        {
            path = "c:\t.txt";
            simpleMessage = "This is a simple message.";

            //Creating new textFile writer mock
            var stream = new System.IO.MemoryStream();
            file = new Mock<System.IO.StreamWriter>(stream);

            //Create TextFile object from the mock
            textFile = new Logic.FileIO.TextFile(path, file.Object);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Constructor_WhenPathIsEmpty_ThrowsNullException(string path)
        {
            Assert.That(() => new Logic.FileIO.TextFile(path),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Append_WhenPassingEmptyMessage_ThrowsNullException(string msg)
        {
            Assert.That(() => textFile.Append(msg), Throws.ArgumentNullException);
        }

        [Test]
        public void Append_WhenPassingMessage_CallWriteLine()
        {
            file.Setup(s => s.WriteLine(simpleMessage));

            textFile.Append(simpleMessage);

            file.Verify(s => s.WriteLine(simpleMessage));
        }
    }
}
