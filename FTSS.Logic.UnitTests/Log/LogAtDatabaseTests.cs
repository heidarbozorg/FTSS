using NUnit.Framework;
using Moq;

namespace FTSS.Logic.UnitTests.Log
{
    [TestFixture]
    class LogAtDatabaseTests
    {
        private Mock<Logic.Database.IDatabaseContext> _ctx;
        private Logic.Log.LogAtDatabase _logger;

        [SetUp]
        public void Setup()
        {
            _ctx = new Mock<Logic.Database.IDatabaseContext>();
            _logger = new Logic.Log.LogAtDatabase(_ctx.Object);
        }

        [Test]
        public void Constructor_WhenPassingNullDatabaseContext_ThrowsNullException()
        {
            Assert.That(() => new Logic.Log.LogAtDatabase(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Add_WhenPassingNullException_ThrowsNullException()
        {
            System.Exception e = null;
            Assert.That(() => _logger.Add(e),
                Throws.ArgumentNullException);
        }

        [TestCase(null)]
        [TestCase("")]
        [Test]
        public void Add_WhenPassingEmptyMessage_ThrowsNullException(string msg)
        {
            Assert.That(() => _logger.Add(msg),
                Throws.ArgumentNullException);
        }
    }
}
