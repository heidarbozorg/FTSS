using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_DeleteTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_Delete.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs>> _sp;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_User_Delete.Inputs()
            {
                Token = "TokenValue",
                UserId = 1
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        [Test]
        public void SP_User_Delete_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_Delete(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", 1)]
        [TestCase(null, 1)]
        [TestCase("Token", 0)]
        [TestCase("", 0)]
        [TestCase(null, 0)]
        [Test]
        public void SP_User_Delete_WhenPassingInvalidData_ThrowsArgumentException(string token, int userId)
        {
            _inputs.Token = token;
            _inputs.UserId = userId;

            Assert.That(() => _dbCTX.SP_User_Delete(_inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_Delete(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_Delete(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
    }
}
