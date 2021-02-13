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
    }
}
