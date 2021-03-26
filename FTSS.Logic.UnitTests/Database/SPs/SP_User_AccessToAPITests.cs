using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;


namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_AccessToAPITests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_AccessToAPI.Inputs _accessToAPIInputs;
        Mock<DP.DapperORM.ISqlExecuter> executer;

        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISqlExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _accessToAPIInputs = new SP_User_AccessToAPI.Inputs()
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

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs);

            executer.Verify(s =>
                s.Query<SP_User_AccessToAPI.Outputs>("SP_User_AccessToAPI", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
