using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Log
{
    [TestFixture]
    public class APILoggerTextFileTests
    {
        private Logic.Log.APILoggerTextFile _logger;
        private Mock<System.IO.StreamWriter> _writer;

        [SetUp]
        public void Setup()
        {
            var stream = new System.IO.MemoryStream();              //Write on the fly
            _writer = new Mock<System.IO.StreamWriter>(stream);     //Creating new textFile writer mock
            var textFile = new Logic.FileIO.TextFile(_writer.Object);

            //Creating new textFile writer mock              
            _logger = new Logic.Log.APILoggerTextFile(textFile);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Constructor_WhenPassingEmptyPath_ThrowsNullException(string path)
        {
            Assert.That(() => new Logic.Log.APILoggerTextFile(path), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_WhenPassingNullFileOperation_ThrowsNullException()
        {
            Logic.FileIO.IFileOperation fileOperation = null;
            Assert.That(() => new Logic.Log.APILoggerTextFile(fileOperation), Throws.ArgumentNullException);
        }

        [Test]
        public void Save_WhenPassingNullData_ThrowsNullException()
        {
            Assert.That(() => _logger.Save(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Save_WhenPassingData_RunsWriteLine()
        {
            var data = new Models.API.Log()
            {
                APIAddress = "https://Google.Com/",
                Params = "{'Token': 'token'}",
                StatusCode = 200,
            };

            _logger.Save(data);
            
            _writer.Verify(
                s => s.WriteLine(It.IsAny<string>())
            );
        }
    }
}
