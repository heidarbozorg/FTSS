using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_InsertTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_Insert.Inputs _inputs;
        Mock<DP.DapperORM.ISQLExecuter> executer;

        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new Models.Database.StoredProcedures.SP_User_Insert.Inputs()
            {
                Token = "TokenValue",
                Email = "email",
                LastName = "lastName",
                Password = "password"
            };
        }

        [Test]
        public void SP_User_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_Insert(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_Insert(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_Insert(_inputs);

            executer.Verify(s =>
                s.Query<SP_User_Insert.Outputs>("SP_User_Insert", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
