using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_ChangePasswordTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>> _sp;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs()
            {
                Token = "TokenValue",
                OldPassword = "P0",
                NewPassword = "P1"
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_ChangePassword(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "", "")]
        [TestCase(null, "", "")]
        [TestCase(null, null, "")]
        [TestCase(null, null, null)]
        [TestCase("Token", null, null)]
        [TestCase("Token", "P0", null)]
        [TestCase("Token", null, "P1")]
        [TestCase(null, "P0", "P1")]
        [TestCase("Token", "", "P1")]
        [TestCase("Token", "P0", "")]
        [Test]
        public void SP_User_ChangePassword_WhenPassingInvalidData_ThrowsArgumentException(string token, string oldPassword, string newPassword)
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs()
            {
                Token = token,
                OldPassword = oldPassword,
                NewPassword = newPassword
            };

            Assert.That(() => _dbCTX.SP_User_ChangePassword(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_ChangePassword(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_ChangePassword(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
    }
}
