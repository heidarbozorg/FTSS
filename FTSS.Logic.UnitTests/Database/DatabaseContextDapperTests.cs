using Moq;
using NUnit.Framework;

namespace FTSS.Logic.UnitTests.Database
{
    [TestFixture]
    class DatabaseContextDapperTests
    {
        readonly string _connectionString = "Not empty string";
        Logic.Database.IDatabaseContext _dbCTX;
        Models.Database.StoredProcedures.SP_Login.Inputs _loginInputs;
        Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs _accessToAPIInputs;


        [SetUp]
        public void Setup()
        {
            _dbCTX = new Logic.Database.DatabaseContextDapper(_connectionString);
            _loginInputs = new Models.Database.StoredProcedures.SP_Login.Inputs()
            {
                Email = "username",
                Password = "password"
            };

            _accessToAPIInputs = new Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs()
            {
                Token = "TokenValue",
                APIAddress = "http://Domain.com/api"
            };
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
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();
            sp.Setup(s => s.Call(_accessToAPIInputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_AccessToAPI_WhenPassingValidData_ItRunsCallMethod()
        {            
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_AccessToAPI.Inputs>>();

            _dbCTX.SP_User_AccessToAPI(_accessToAPIInputs, sp.Object);

            sp.Verify(s => s.Call(_accessToAPIInputs));
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
            var inputs = new Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs()
            {
                Token = token,
                LastName = lastName
            };

            Assert.That(() => _dbCTX.SP_User_UpdateProfile(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs()
            {
                Token = "TokenValue",
                LastName = "lastname"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_UpdateProfile(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_UpdateProfile_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs()
            {
                Token = "TokenValue",
                LastName = "lastname"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_UpdateProfile.Inputs>>();

            _dbCTX.SP_User_UpdateProfile(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_UpdateProfile

        #region SP_User_SetPassword
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
            var inputs = new Models.Database.StoredProcedures.SP_User_SetPassword.Inputs()
            {
                Token = token,
                UserId = userId,
                Password = password
            };

            Assert.That(() => _dbCTX.SP_User_SetPassword(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_SetPassword.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Password = "Password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_SetPassword(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_SetPassword_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_SetPassword.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Password = "Password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_SetPassword.Inputs>>();

            _dbCTX.SP_User_SetPassword(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_SetPassword

        #region SP_User_Delete
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
            var inputs = new Models.Database.StoredProcedures.SP_User_Delete.Inputs()
            {
                Token = token,
                UserId = userId
            };

            Assert.That(() => _dbCTX.SP_User_Delete(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Delete.Inputs()
            {
                Token = "TokenValue",
                UserId = 1
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_Delete(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Delete_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Delete.Inputs()
            {
                Token = "TokenValue",
                UserId = 1
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Delete.Inputs>>();

            _dbCTX.SP_User_Delete(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_Delete

        #region SP_User_Update
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
            var inputs = new Models.Database.StoredProcedures.SP_User_Update.Inputs()
            {
                Token = token,
                UserId = userId,
                Email = email,
                LastName = lastName
            };

            Assert.That(() => _dbCTX.SP_User_Update(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Update_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Update.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Email = "email",
                LastName = "lastName"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_Update(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Update_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Update.Inputs()
            {
                Token = "TokenValue",
                UserId = 1,
                Email = "email",
                LastName = "lastName"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Update.Inputs>>();

            _dbCTX.SP_User_Update(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_Update

        #region SP_User_Insert
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
            var inputs = new Models.Database.StoredProcedures.SP_User_Insert.Inputs()
            {
                Token = token,
                Email = email,
                LastName = lastName,
                Password = password
            };

            Assert.That(() => _dbCTX.SP_User_Insert(inputs),
                Throws.ArgumentException);
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItReturnDBResult()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Insert.Inputs()
            {
                Token = "TokenValue",
                Email = "email",
                LastName = "lastName",
                Password = "password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs>>();
            sp.Setup(s => s.Call(inputs)).Returns(new Models.Database.DBResult());

            var result = _dbCTX.SP_User_Insert(inputs, sp.Object);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(Models.Database.DBResult)));
        }

        [Test]
        public void SP_User_Insert_WhenPassingValidData_ItRunsCallMethod()
        {
            var inputs = new Models.Database.StoredProcedures.SP_User_Insert.Inputs()
            {
                Token = "TokenValue",
                Email = "email",
                LastName = "lastName",
                Password = "password"
            };
            var sp = new Mock<Models.Database.ISP<Models.Database.StoredProcedures.SP_User_Insert.Inputs>>();

            _dbCTX.SP_User_Insert(inputs, sp.Object);

            sp.Verify(s => s.Call(inputs));
        }
        #endregion SP_User_Insert
    }
}
