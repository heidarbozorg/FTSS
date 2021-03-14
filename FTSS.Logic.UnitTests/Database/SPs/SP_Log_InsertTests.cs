using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_Log_InsertTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
        }

        [Test]
        public void SP_Log_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Log_Insert(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void SP_Log_Insert_WhenPassingEmptyMSG_ThrowsArgumentNullException(string msg)
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = msg
            };

            Assert.That(() => _dbCTX.SP_Log_Insert(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_Log_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = "Simple log message"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_Log_Insert(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Log_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = "Simple log message"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>();

            _dbCTX.SP_Log_Insert(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
    }
}
