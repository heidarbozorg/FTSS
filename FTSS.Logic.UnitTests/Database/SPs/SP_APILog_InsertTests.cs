using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;


namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_APILog_InsertTests
    {
        readonly string _connectionString = "Not empty string";
        readonly string _apiAddress = "http://Domain.com/api";

        Logic.Database.IDatabaseContext_MisExtract _dbCTX;
        SP_APILog_Insert.Inputs _inputs;
        Mock<DP.DapperORM.ISqlExecuter> executer;

        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISqlExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper_MisExtract(_connectionString, executer.Object);
            _inputs = new SP_APILog_Insert.Inputs()
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

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItReturnDBResult()
        {            
            var result = _dbCTX.SP_APILog_Insert(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_APILog_Insert(_inputs);

            executer.Verify(s =>
                s.Query<SP_APILog_Insert.Outputs>("SP_APILog_Insert", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
    }
}
