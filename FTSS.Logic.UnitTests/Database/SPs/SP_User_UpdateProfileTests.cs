using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database.SPs
{
    [TestFixture]
    class SP_User_UpdateProfileTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs _inputs;
        Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs>> _sp;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _inputs = new Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs()
            {
                Token = "TokenValue",
                LastName = "lastname"
            };
            _sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs>>();
            _sp.Setup(s => s.Call(_inputs)).Returns(new Models.Database.DBResult());
        }

        #region SP_User_UpdateProfile
        [Test]
        public void SP_User_UpdateProfile_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_UpdateProfile(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase("Token", null)]
        [TestCase("Token", "")]
        [TestCase("", "Lastname")]
        [TestCase(null, "Lastname")]
        [Test]
        public void SP_User_UpdateProfile_WhenPassingInvalidData_ThrowsArgumentException(string token, string lastName)
        {
            _inputs.Token = token;
            _inputs.LastName = lastName;

            Assert.That(() => _dbCTX.SP_User_UpdateProfile(_inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItReturnDBResult()
        {
            var result = _dbCTX.SP_User_UpdateProfile(_inputs, _sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItRunsCallMethod()
        {
            _dbCTX.SP_User_UpdateProfile(_inputs, _sp.Object);

            _sp.Verify(s => s.Call(_inputs));
        }
        #endregion SP_User_UpdateProfile
    }
}
