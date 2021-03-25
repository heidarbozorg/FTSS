using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_Users_GetAllTests
    {
        readonly string _connectionString = "Not empty string";
        SP_Users_GetAll.Inputs _inputs;
        Logic.Database.IDatabaseContext _dbCTX;        
        Mock<DP.DapperORM.ISQLExecuter> executer;

        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new SP_Users_GetAll.Inputs()
            {
                Token = "TokenValue",
                StartIndex = 1,
                PageSize = 10
            };
        }

        #region SP_Users_GetAll
        [Test]
        public void SP_Users_GetAll_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Users_GetAll(null),
                Throws.ArgumentNullException);
        }        

        [Test]
        public void SP_Users_GetAll_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_Users_GetAll(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Users_GetAll_WhenPassingValidData_ItRunsCallMethod()
        {            
            _dbCTX.SP_Users_GetAll(_inputs);

            executer.Verify(s => 
                s.Query<SP_Users_GetAll.Outputs>("SP_Users_GetAll", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
        #endregion SP_Users_GetAll
    }
}
