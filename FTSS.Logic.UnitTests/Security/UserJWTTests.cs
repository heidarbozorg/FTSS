using NUnit.Framework;
using Moq;

namespace FTSS.Logic.UnitTests.Security
{
    [TestFixture]
    class UserJWTTests
    {
        private string _key = "A simple key for generating JWT and test the common class.";
        private string _issuer = "http://FTSS.com";
        private string _dbToken = "DatabaseToken";
        private string _roleTitle = "DBRoleTitle";
        private string _email = "Username";
        private System.DateTime _expireDate = System.DateTime.Now.AddDays(1);

        private Models.Database.DBResult _data;
        private Models.Database.StoredProcedures.SP_Login.Outputs _loginResult;
        private Logic.Security.UserJWT _userJWT;
        Mock<AutoMapper.IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _loginResult = new Models.Database.StoredProcedures.SP_Login.Outputs()
            {
                UserId = 1,
                Token = _dbToken,
                ExpireDate = _expireDate,
                RoleTitle = _roleTitle,
                Email = _email
            };
            _data = new Models.Database.DBResult(200, "", _loginResult);

            _userJWT = new Logic.Security.UserJWT()
            {
                UserId = 1,
                Token = _dbToken,
                ExpireDate = _expireDate,
                RoleTitle = _roleTitle,
                Email = _email
            };
            _mapper = new Mock<AutoMapper.IMapper>();
            _mapper.Setup(c => c.Map<Logic.Security.UserJWT>(_loginResult)).Returns(_userJWT);
        }

        [Test]
        public void Get_WhenDataIsNull_ThrowsNullException()
        {
            Assert.That(() =>
                Logic.Security.UserJWT.Get(null, _key, _issuer, _mapper.Object),
                Throws.ArgumentNullException);
        }


        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Get_WhenPassingEmptyKey_ThrowsNullException(string key)
        {
            Assert.That(() =>
                Logic.Security.UserJWT.Get(_data, key, _issuer, _mapper.Object),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void Get_WhenPassingEmptyIssuer_ThrowsNullException(string issuer)
        {
            Assert.That(() =>
                Logic.Security.UserJWT.Get(_data, _key, issuer, _mapper.Object),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Get_WhenPassingNullMapper_ThrowsNullException()
        {
            Assert.That(() =>
                Logic.Security.UserJWT.Get(_data, _key, _issuer, null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void Get_WhenLoginResultHasErrorCode_ThrowsArgumentException()
        {
            _data.ErrorCode = 300;
            Assert.That(() =>
                Logic.Security.UserJWT.Get(_data, _key, _issuer, _mapper.Object),
                Throws.ArgumentException);
        }

        [Test]
        public void Get_WhenLoginResultHasErrorMessage_ThrowsArgumentException()
        {
            _data.ErrorMessage = "A simple error message";
            Assert.That(() =>
                Logic.Security.UserJWT.Get(_data, _key, _issuer, _mapper.Object),
                Throws.ArgumentException);
        }

        [Test]
        public void Get_WhenPassingLoginResult_ReturnUserJWT()
        {
            var result = Logic.Security.UserJWT.Get(_data, _key, _issuer, _mapper.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Logic.Security.UserJWT>());
        }

        [Test]
        public void Get_WhenPassingLoginResult_UserJWTHasStringJWTToken()
        {
            var result = Logic.Security.UserJWT.Get(_data, _key, _issuer, _mapper.Object);

            Assert.That(result.JWT, Is.Not.Null);
            Assert.That(result.JWT, Is.Not.Empty);
            Assert.That(result.JWT, Has.Length.GreaterThan(10));
        }
    }
}
