using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_SetPasswordTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_SetPassword.Inputs _inputs;
        Mock<DP.DapperORM.ISqlExecuter> executer;


        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISqlExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new SP_User_SetPassword.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Password = "Password"
            };
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_SetPassword(null),
                Throws.ArgumentNullException);
        }        

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_SetPassword(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_SetPassword(_inputs);

            executer.Verify(s => 
                s.Query<SP_User_SetPassword.Outputs>("SP_User_SetPassword", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
