using NUnit.Framework;
using Moq;
using FTSS.Models.Database;

namespace FTSS.Logic.UnitTests.Security
{
    [TestFixture]
    class CommonTests
    {
        private string _key;
        private string _issuer;
        private System.DateTime _expireDate;
        private System.Security.Claims.Claim[] _claims;
        private Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs _data;
        private Mock<Logic.Database.IDatabaseContext> _ctx;

        [SetUp]
        public void Setup()
        {
            _key = "A simple key for generating JWT and test the common class.";
            _issuer = "http://FTSS.com";
            _expireDate = System.DateTime.Now.AddDays(1);
            _claims = new System.Security.Claims.Claim[]
            {
                new System.Security.Claims.Claim("Key1", "value1"),
                new System.Security.Claims.Claim("Key2", "value2"),
            };

            _ctx = new Mock<Logic.Database.IDatabaseContext>();
            _data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                APIAddress = "http://FTSS.com/api",
                Token = "DatabaseToken"
            };
        }

        #region GenerateJWT
        [Test]
        public void GenerateJWT_WhenPassingNullClaims_ThrowsNullException()
        {
            Assert.That(() =>
                Logic.Security.Common.GenerateJWT(null, _key, _issuer, _expireDate), Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void GenerateJWT_WhenPassingEmptyKey_ThrowsNullException(string key)
        {
            Assert.That(() =>
                Logic.Security.Common.GenerateJWT(_claims, key, _issuer, _expireDate), Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void GenerateJWT_WhenPassingEmptyIssuer_ThrowsNullException(string issuer)
        {
            Assert.That(() =>
                Logic.Security.Common.GenerateJWT(_claims, _key, issuer, _expireDate), Throws.ArgumentNullException);
        }

        [Test]
        public void GenerateJWT_WhenPassingPastExpireDate_ThrowsArgumentException()
        {
            _expireDate = _expireDate.AddYears(-1);
            Assert.That(() =>
                Logic.Security.Common.GenerateJWT(_claims, _key, _issuer, _expireDate), Throws.ArgumentException);
        }

        [Test]
        public void GenerateJWT_WhenPassingData_ReturnString()
        {
            var rst = Logic.Security.Common.GenerateJWT(_claims, _key, _issuer, _expireDate);

            Assert.That(rst, Is.Not.Null);
            Assert.That(rst, Is.Not.Empty);
            Assert.That(rst, Has.Length.GreaterThan(10));
        }
        #endregion GenerateJWT

        #region IsUserAccessToAPI
        [Test]
        public void IsUserAccessToAPI_WhenPassingNullDatabaseContext_ThrowsNullException()
        {
            Assert.That(() =>
                Logic.Security.Common.IsUserAccessToAPI(null, _data), Throws.ArgumentNullException);
        }

        [Test]
        public void IsUserAccessToAPI_WhenPassingNullData_ThrowsNullException()
        {
            Assert.That(() =>
                Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, null), Throws.ArgumentNullException);
        }

        [Test]
        public void IsUserAccessToAPI_WhenCall_RunsSP_User_AccessToAPI()
        {
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            _ctx.Verify(c => c.SP_User_AccessToAPI(_data));
        }

        [Test]
        public void IsUserAccessToAPI_WhenUserAccess_ReturnsTrue()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Returns(new DBResult() {
                    StatusCode = 200,
                    Data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs()
                    {
                        Result = true
                    }
                });
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            Assert.That(rst, Is.True);
        }

        [Test]
        public void IsUserAccessToAPI_WhenUserNotAccess_ReturnsFalse()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Returns(new DBResult()
                {
                    StatusCode = 200,
                    Data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs()
                    {
                        Result = false
                    }
                });
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            Assert.That(rst, Is.False);
        }

        [Test]
        public void IsUserAccessToAPI_WhenDatabaseHasErrorCode_ReturnsFalse()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Returns(new DBResult()
                {
                    StatusCode = 300,
                    Data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs()
                    {
                        Result = true
                    }
                });
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            Assert.That(rst, Is.False);
        }

        [Test]
        public void IsUserAccessToAPI_WhenDatabaseHasErrorMessage_ReturnsFalse()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Returns(new DBResult()
                {
                    StatusCode = 200,
                    ErrorMessage = "A simple error",
                    Data = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Outputs()
                    {
                        Result = true
                    }
                });
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            Assert.That(rst, Is.False);
        }

        [Test]
        public void IsUserAccessToAPI_WhenDatabaseResultIsNull_ReturnsFalse()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Returns(new DBResult()
                {
                    StatusCode = 200,
                    Data = null
                });
            var rst = Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data);

            Assert.That(rst, Is.False);
        }

        [Test]
        public void IsUserAccessToAPI_WhenDatabaseThrowsException_ThrowsException()
        {
            _ctx.Setup(c => c.SP_User_AccessToAPI(_data))
                .Throws(new System.Exception("Unknown error"));
            Assert.That(() => 
                Logic.Security.Common.IsUserAccessToAPI(_ctx.Object, _data), 
                Throws.Exception);
        }
        #endregion IsUserAccessToAPI
    }
}
