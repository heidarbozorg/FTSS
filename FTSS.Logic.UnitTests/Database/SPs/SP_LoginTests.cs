using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_LoginTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Mock<DP.DapperORM.ISQLExecuter> _executer;
        SP_Login.Inputs _loginInputs;


        [SetUp]
        public void Setup()
        {
            _executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, _executer.Object);
            _loginInputs = new SP_Login.Inputs()
            {
                Email = "username",
                Password = "password"
            };
        }

        [Test]
        public void SP_Login_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Login(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_Login(_loginInputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_Login(_loginInputs);
            _executer.Verify(s => 
                s.Query<SP_Login.Outputs>("SP_Login", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}