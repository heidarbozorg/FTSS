using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_UpdateTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_Update.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs>> _sp;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);

            _inputs = new Models.Database.StoredProcedures.SP_User_Update.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Email = "email",
                LastName = "lastName"
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        [Test]
        public void SP_User_Update_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_Update(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", 1, "email", "lastName")]
        [TestCase("Token", 0, "email", "lastName")]
        [TestCase("Token", 1, "", "lastName")]
        [TestCase("Token", 1, "email", "")]
        [TestCase("", 0, "", "")]
        [TestCase(null, 0, null, null)]
        [TestCase(null, 1, "email", "lastName")]
        [TestCase("Token", 1, null, "lastName")]
        [TestCase("Token", 1, "email", null)]
        [Test]
        public void SP_User_Update_WhenPassingInvalidData_ThrowsArgumentException(string token, int userId, string email, string lastName)
        {
            _inputs.Token = token;
            _inputs.UserId = userId;
            _inputs.Email = email;
            _inputs.LastName = lastName;
            Assert.That(() => _dbCTX.SP_User_Update(_inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Update_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_Update(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Update_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_Update(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
    }
}
