using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_LoginTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_Login.Inputs _loginInputs;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _loginInputs = new Models.Database.StoredProcedures.SP_Login.Inputs()
            {
                Email = "username",
                Password = "password"
            };
        }

        [Test]
        public void SP_Login_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Login(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "Pass")]
        [TestCase(null, "Pass")]
        [TestCase("Email", "")]
        [TestCase("Email", null)]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase(null, null)]
        [Test]
        public void SP_Login_WhenPassingEmptyEmailOrPassword_ThrowsArgumentNullException(string email, string password)
        {
            _loginInputs.Email = email;
            _loginInputs.Password = password;

            Assert.That(() => _dbCTX.SP_Login(_loginInputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItReturnDBResult()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Login.Inputs>>();
            sp.Setup(s => s.Call(_loginInputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_Login(_loginInputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItRunsCallMethod()
        {
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Login.Inputs>>();

            _dbCTX.SP_Login(_loginInputs, sp.Object);

            sp.Verify(s => s.Call(_loginInputs));
        }
    }
}