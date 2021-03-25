using Dapper;
using Moq;
using NUnit.Framework;

namespace FTSS.DP.DapperORM.UnitTests
{
    [TestFixture]
    class CommonTests
    {
        private string _token;
        private Models.Database.BaseSearchParams _filterParams;
        private Models.Database.BaseDataModelWithToken _data;
        DynamicParameters _outputParams;

        [SetUp]
        public void Setup()
        {
            _token = "UserDBToken";
            _filterParams = new Models.Database.BaseSearchParams()
            {
                Token = _token,
                PageSize = 10,
                StartIndex = 0
            };

            _data = new Models.Database.BaseDataModelWithToken()
            {
                Token = _token
            };

            _outputParams = new DynamicParameters();
        }

        [Test]
        public void GetEmptyParams_WhenCall_ReturnDynamicParametersObject()
        {
            var result = FTSS.DP.DapperORM.Common.GetEmptyParams();

            Assert.That(result, Is.Not.Null);            
            Assert.That(result, Is.InstanceOf<DynamicParameters>());
        }

        [Test]
        public void GetErrorCodeAndErrorMessageParams_WhenCall_ReturnDynamicParametersObject()
        {
            var result = FTSS.DP.DapperORM.Common.GetErrorCodeAndErrorMessageParams();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<DynamicParameters>());
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void GetToken_WhenPassEmptyToken_ThrowsNullException(string token)
        {
            Assert.That(() =>
                FTSS.DP.DapperORM.Common.GetToken(token),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetToken_WhenPassToken_ReturnsDynamicParameters()
        {
            var result = Common.GetToken(_token);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<DynamicParameters>());
        }

        [Test]
        public void GetSearchParams_WhenPassNullParams_ThrowsNullException()
        {
            Assert.That(() =>
                Common.GetSearchParams(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void GetSearchParams_WhenPassEmptyToken_ThrowsNullException(string token)
        {
            _filterParams.Token = token;

            Assert.That(() =>
                Common.GetSearchParams(_filterParams),
                Throws.ArgumentNullException);
        }

        [TestCase(-1, 10)]
        [TestCase(0, 0)]
        [TestCase(-1, 0)]
        [Test]
        public void GetSearchParams_WhenPassInvalidPagination_ThrowsNullException(int startIndex, byte pageSize)
        {
            _filterParams.StartIndex = startIndex;
            _filterParams.PageSize = pageSize;

            Assert.That(() =>
                FTSS.DP.DapperORM.Common.GetSearchParams(_filterParams),
                Throws.InstanceOf<System.ArgumentOutOfRangeException>());
        }

        [Test]
        public void GetSearchParams_WhenPassParams_ReturnsDynamicParameters()
        {
            var result = Common.GetSearchParams(_filterParams);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<DynamicParameters>());
        }

        [Test]
        public void GetDataParams_WhenPassingNullData_ThrowsNullException()
        {
            Assert.That(() =>
                Common.GetDataParams(null),
                Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void GetDataParams_WhenPassingDataWithEmptyToken_ThrowsNullException(string token)
        {
            _data.Token = token;
            Assert.That(() =>
                Common.GetDataParams(_data),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetDataParams_WhenPassingData_ReturnsDynamicParameters()
        {
            var result = Common.GetDataParams(_data);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<DynamicParameters>());
        }

        [Test]
        public void GetResult_WhenPassingNullOutputParams_ThrowsNullException()
        {
            Assert.That(() =>
                Common.GetResult(null, _data),
                Throws.ArgumentNullException);
        }

        [Test]
        public void GetResult_WhenPassingOutputParams_ReturnsDBResult()
        {
            _outputParams.Add("@ErrorMessage", It.IsAny<string>());
            _outputParams.Add("@ErrorCode", It.IsAny<int>());

            var result = Common.GetResult(_outputParams, _data);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Models.Database.DBResult>());
        }
    }
}
