using Moq;
using NUnit.Framework;
using FTSS.Models.Database.StoredProcedures;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_UpdateProfileTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        SP_User_UpdateProfile.Inputs _inputs;
        Mock<DP.DapperORM.ISQLExecuter> executer;


        [SetUp]
        public void Setup()
        {
            executer = new Mock<DP.DapperORM.ISQLExecuter>();
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString, executer.Object);
            _inputs = new SP_User_UpdateProfile.Inputs()
            {
                Token = "TokenValue",
                LastName = "lastname"
            };
        }

        #region SP_User_UpdateProfile
        [Test]
        public void SP_User_UpdateProfile_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_UpdateProfile(null),
                Throws.ArgumentNullException);
        }
        
        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_UpdateProfile(_inputs);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_UpdateProfile(_inputs);

            executer.Verify(s => 
                s.Query<SP_User_UpdateProfile.Outputs>("SP_User_UpdateProfile", It.IsAny<object>(), System.Data.CommandType.StoredProcedure));
        }
        #endregion SP_User_UpdateProfile
    }
}
