using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Text;

namespace FTSS.DP.DapperORM.UnitTests.StoredProcedure
{
    [TestFixture]
    class SP_APILog_InsertTests
    {
        Mock<FTSS.DP.DapperORM.ISQLExecuter> _executer;
        Models.Database.StoredProcedures.SP_APILog_Insert.Inputs _data;
        DP.DapperORM.StoredProcedure.SP_APILog_Insert apiObject;

        [SetUp]
        public void Setup()
        {
            _data = new Models.Database.StoredProcedures.SP_APILog_Insert.Inputs();
            _executer = new Mock<FTSS.DP.DapperORM.ISQLExecuter>();
            apiObject = new DP.DapperORM.StoredProcedure.SP_APILog_Insert("my connection string", _executer.Object);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void Constructor_WhenConnectionStringIsEmpty_ThrowsArgumentNullException(string cns)
        {
            Assert.That(() => {
                new DP.DapperORM.StoredProcedure.SP_APILog_Insert(cns);
            }, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_WhenConnectionStringIsValid_NotThrowsArgumentNullException()
        {
            var apiObject = new DP.DapperORM.StoredProcedure.SP_APILog_Insert("my connection string");
            Assert.That(apiObject, Is.Not.Null);
            Assert.That(apiObject, Is.TypeOf<DP.DapperORM.StoredProcedure.SP_APILog_Insert>());
        }

        [Test]
        public void Call_WhenDataIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => { apiObject.Call(null); }, Throws.ArgumentNullException);
        }


        [Test]
        public void Call_WhenDataIsValid_ReturnDBResult()
        {            
            var result = apiObject.Call(_data);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ErrorCode, Is.EqualTo(200));
        }
    }
}
