using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_InsertTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_Insert.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs>> _sp;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_User_Insert.Inputs()
            {
                Token = "TokenValue",
                Email = "email",
                LastName = "lastName",
                Password = "password"
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());

        }

        [Test]
        public void SP_User_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_Insert(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "email", "lastName", "password")]
        [TestCase("Token", "", "lastName", "password")]
        [TestCase("Token", "email", "", "password")]
        [TestCase("Token", "email", "lastName", "")]
        [TestCase(null, "email", "lastName", "password")]
        [TestCase("Token", null, "lastName", "password")]
        [TestCase("Token", "email", null, "password")]
        [TestCase("Token", "email", "lastName", null)]
        [TestCase("Token", "email", null, null)]
        [TestCase("Token", null, null, null)]
        [TestCase(null, null, null, null)]
        [Test]
        public void SP_User_Insert_WhenPassingInvalidData_ThrowsArgumentException(string token, string email, string lastName, string password)
        {
            _inputs.Token = token;
            _inputs.Email = email;
            _inputs.LastName = lastName;
            _inputs.Password = password;

            Assert.That(() => _dbCTX.SP_User_Insert(_inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_Insert(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_Insert(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
    }
}
