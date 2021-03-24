using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_AccessToAPITests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs _accessToAPIInputs;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);

            _accessToAPIInputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = "TokenValue",
                APIAddress = "http://Domain.com/api"
            };
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_AccessToAPI(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "APIAddress")]
        [TestCase(null, "APIAddress")]
        [TestCase("TokenValue", "")]
        [TestCase("TokenValue", null)]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase(null, null)]
        [Test]
        public void SP_User_AccessToAPI_WhenPassingEmptyAPIAddressOrEmptyToken_ThrowsArgumentNullException(string token, string apiAddress)
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = token,
                APIAddress = apiAddress
            };

            Assert.That(() => _dbCTX.SP_User_AccessToAPI(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItReturnDBResult()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();
            sp.Setup(s => s.Call(_accessToAPIInputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItRunsCallMethod()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();

            _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs);

            sp.Verify(s => s.Call(_accessToAPIInputs));
        }
    }
}
