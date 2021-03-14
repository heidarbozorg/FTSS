using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_Users_GetAllTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_Users_GetAll.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs>> _sp;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_Users_GetAll.Inputs()
            {
                Token = "TokenValue",
                StartIndex = 1,
                PageSize = 10
            };

            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        #region SP_Users_GetAll
        [Test]
        public void SP_Users_GetAll_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Users_GetAll(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", 1, 10)]
        [TestCase(null, 1, 10)]
        [TestCase("TokenValue", -1, 10)]
        [TestCase("TokenValue", 1, 0)]
        [TestCase("TokenValue", -1, 0)]
        [TestCase("TokenValue", 1, 101)]
        [TestCase("TokenValue", -1, 101)]
        [Test]
        public void SP_Users_GetAll_WhenPassingInvalidData_ThrowsArgumentException(string token, int startIndex, byte pageSize)
        {
            var inputs = new Models.Database.StoredProcedures.SP_Users_GetAll.Inputs()
            {
                Token = token,
                StartIndex = startIndex,
                PageSize = pageSize
            };

            Assert.That(() => _dbCTX.SP_Users_GetAll(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_Users_GetAll_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_Users_GetAll(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Users_GetAll_WhenPassingValidData_ItRunsCallMethod()
        {            
            _dbCTX.SP_Users_GetAll(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
        #endregion SP_Users_GetAll
    }
}
