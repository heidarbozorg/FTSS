using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_DeleteTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_Delete.Inputs _inputs;
        Mock<DP.DapperORM.ISQLExecuter> executer;


        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new SP_User_Delete.Inputs()
            {
                Token = "TokenValue",
                UserId = 1
            };
        }

        [Test]
        public void SP_User_Delete_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_Delete(null),
                Throws.ArgumentNullException);
        }
        

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_Delete(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_Delete(_inputs);

            executer.Verify(s =>
                s.Query<SP_User_Delete.Outputs>("SP_User_Delete", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
