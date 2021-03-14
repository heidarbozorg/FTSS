using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_SetPasswordTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_SetPassword.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs>> _sp;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_User_SetPassword.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Password = "Password"
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_SetPassword(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", 1, "")]
        [TestCase(null, 1, "")]
        [TestCase(null, 1, null)]
        [TestCase("", 1, null)]
        [TestCase("Token", 1, null)]
        [TestCase("Token", 1, "")]
        [TestCase("", 1, "Lastname")]
        [TestCase(null, 1, "Lastname")]
        [TestCase("Token", 0, "Lastname")]
        [Test]
        public void SP_User_SetPassword_WhenPassingInvalidData_ThrowsArgumentException(string token, int userId, string password)
        {
            _inputs.Token = token;
            _inputs.UserId = userId;
            _inputs.Password = password;

            Assert.That(() => _dbCTX.SP_User_SetPassword(_inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_SetPassword(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_SetPassword(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
    }
}
