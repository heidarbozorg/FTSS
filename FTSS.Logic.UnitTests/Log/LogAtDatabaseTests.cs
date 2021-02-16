using NUnit.Framework;
using Moq;
using FTSS.Models.Database;
using System;

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

        [Test]
        public void Add_WhenPassingMessage_RunsSP_Log_InsertJustOnce()
        {
            _logger.Add("Simple message");
            
            _ctx.Verify(c => 
                c.SP_Log_Insert
                (
                    It.IsAny<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>(), 
                    It.IsAny<ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>()
                ), Times.Once()
            );
        }

        [Test]
        public void Add_WhenPassingException_RunsSP_Log_InsertJustOnce()
        {
            var e = new Exception("Simple exception");

            _logger.Add(e);

            _ctx.Verify(c =>
                c.SP_Log_Insert
                (
                    It.IsAny<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>(),
                    It.IsAny<ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>()
                ), Times.Once()
            );
        }
    }
}
