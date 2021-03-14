using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_APILog_InsertTests
    {
        readonly string _connectionString = "Not empty string";
        readonly string _apiAddress = "http://Domain.com/api";

        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_APILog_Insert.Inputs _inputs;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs()
            {
                APIAddress = _apiAddress
            };
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_APILog_Insert(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void SP_APILog_Insert_WhenPassingEmptyMSG_ThrowsArgumentNullException(string apiAddress)
        {
            _inputs.APIAddress = apiAddress;
            Assert.That(() => _dbCTX.SP_APILog_Insert(_inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>>();
            sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_APILog_Insert(_inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>>();

            _dbCTX.SP_APILog_Insert(_inputs, sp.Object);

            sp.Verify(s => s.Call(_inputs));
        }
    }
}
