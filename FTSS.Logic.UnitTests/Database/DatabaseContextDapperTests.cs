using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.UnitTests.Database
{
    [TestFixture]
    class DatabaseContextDapperTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;

        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
        }

        #region Constractor
        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void DatabaseContextDapper_ConstructorByEmptyConnectionString_ThrowsArgumentNullException(string cns)
        {
            Assert.That(() => new Logic.Database.DatabaseContextDapper(cns),
                Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("A Valid connection String")]
        [TestCase("Not empty")]
        [TestCase("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=FTSS;Data Source=.")]
        public void DatabaseContextDapper_ConstructorByConnectionString_ReturnDataContextObject(string cns)
        {
            var result = new Logic.Database.DatabaseContextDapper(cns);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Logic.Database.IDatabaseContext>());
        }
        #endregion Constractor

        #region SP_Log_Insert
        [Test]
        public void SP_Log_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_Log_Insert(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void SP_Log_Insert_WhenPassingEmptyMSG_ThrowsArgumentNullException(string msg)
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = msg
            };

            Assert.That(() => _dbCTX.SP_Log_Insert(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_Log_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = "Simple log message"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_Log_Insert(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Log_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Log_Insert.Inputs()
            {
                MSG = "Simple log message"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Log_Insert.Inputs>>();

            _dbCTX.SP_Log_Insert(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_Log_Insert

        #region SP_APILog_Insert
        [Test]
        public void SP_APILog_Insert_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_APILog_Insert(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void SP_APILog_Insert_WhenPassingEmptyMSG_ThrowsArgumentNullException(string apiAddress)
        {
            var inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs()
            {
                APIAddress = apiAddress
            };

            Assert.That(() => _dbCTX.SP_APILog_Insert(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs()
            {
                APIAddress = "http://Domain.com/api"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_APILog_Insert(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_APILog_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs()
            {
                APIAddress = "http://Domain.com/api"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_APILog_Insert.Inputs>>();

            _dbCTX.SP_APILog_Insert(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_APILog_Insert

        #region SP_Login
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
            var inputs = new Models.Database.StoredProcedures.SP_Login.Inputs()
            {
                Email = email,
                Password = password
            };

            Assert.That(() => _dbCTX.SP_Login(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Login.Inputs()
            {
                Email = "Username",
                Password = "Password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Login.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_Login(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Login_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Login.Inputs()
            {
                Email = "Username",
                Password = "Password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Login.Inputs>>();

            _dbCTX.SP_Login(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_Login

        #region SP_User_AccessToAPI
        [Test]
        public void SP_User_AccessToAPI_WhenPassingNullInputs_ThrowsArgumentNullException()
        {
            Assert.That(() => _dbCTX.SP_User_AccessToAPI(null),
                Throws.ArgumentNullException);
        }

        [TestCase("", "APIAddress")]
        [TestCase(null, "APIAddress")]
        [TestCase("TokenValue", "")]
        [TestCase("TokenValue", null)]
        [TestCase("", null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase(null, null)]
        [Test]
        public void SP_User_AccessToAPI_WhenPassingEmptyAPIAddressOrEmptyToken_ThrowsArgumentNullException(string token, string apiAddress)
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = token,
                APIAddress = apiAddress
            };

            Assert.That(() => _dbCTX.SP_User_AccessToAPI(inputs),
                Throws.ArgumentNullException);
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = "TokenValue",
                APIAddress = "http://Domain.com/api"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_AccessToAPI(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = "TokenValue",
                APIAddress = "http://Domain.com/api"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();

            _dbCTX.SP_User_AccessToAPI(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_AccessToAPI

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
            var inputs = new Models.Database.StoredProcedures.SP_Users_GetAll.Inputs()
            {
                Token = "TokenValue",
                StartIndex = 1,
                PageSize = 10
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_Users_GetAll(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_Users_GetAll_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_Users_GetAll.Inputs()
            {
                Token = "TokenValue",
                StartIndex = 1,
                PageSize = 10
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_Users_GetAll.Inputs>>();

            _dbCTX.SP_Users_GetAll(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_Users_GetAll

        #region SP_User_ChangePassword
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
            var inputs = new Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs()
            {
                Token = "TokenValue",
                OldPassword = "P0",
                NewPassword = "P1"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_ChangePassword(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_ChangePassword_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs()
            {
                Token = "TokenValue",
                OldPassword = "P0",
                NewPassword = "P1"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_ChangePassword.Inputs>>();

            _dbCTX.SP_User_ChangePassword(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_ChangePassword
    }
}
