using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_ChangePasswordTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_ChangePassword.Inputs _inputs;
        Mock<DP.DapperORM.ISQLExecuter> executer;

        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new SP_User_ChangePassword.Inputs()
            {
                Token = "TokenValue",
                OldPassword = "P0",
                NewPassword = "P1"
            };
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_ChangePassword(null),
                Throws.ArgumentNullException);
        }        

        [Test]
        public void SP_User_ChangePassword_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_ChangePassword(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_ChangePassword(_inputs);

            executer.Verify(s => s.Query<SP_User_ChangePassword.Outputs>("SP_User_ChangePassword", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
