using Moq;
using NUnit.Framework;

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
