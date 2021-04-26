using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.Logic.UnitTests.CommonOperations
{
    [TestFixture]
    class JSONTests
    {
        #region private variables
        private class Sample
        {
            public int A { get; set; }
            public string B { get; set; }
        }

        private string json;
        private Sample sample;
        #endregion private variables

        #region Setup
        [SetUp]
        public void Setup()
        {
            sample = new Sample()
            {
                A = 1,
                B = "Simple string"
            };

            json = "{\"A\":1,\"B\":\"Simple string\"}";
        }
        #endregion Setup

        #region jsonToT
        [TestCase("")]
        [TestCase(null)]
        [Test]
        public void jsonToT_WhenPassingEmpty_ThrowsNullException(string JSON)
        {
            Assert.That(() => Logic.CommonOperations.JSON.jsonToT<object>(JSON),
                 Throws.ArgumentNullException);
        }

        [Test]
        public void jsonToT_WhenPassingSampleJson_ShouldReturnSampleObject()
        {
            var result = Logic.CommonOperations.JSON.jsonToT<Sample>(json);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Sample>());
        }

        [Test]
        public void jsonToT_WhenPassingSampleJson_ShouldReturnSameValues()
        {
            var result = Logic.CommonOperations.JSON.jsonToT<Sample>(json);

            Assert.That(result.A, Is.EqualTo(sample.A));
            Assert.That(result.B, Is.EqualTo(sample.B));
        }
        #endregion jsonToT

        #region ObjToJson
        [Test]
        public void ObjToJson_WhenPassingNull_ThrowsNullException()
        {
            Assert.That(() => Logic.CommonOperations.JSON.ObjToJson(null),
                    Throws.ArgumentNullException);
        }

        [Test]
        public void ObjToJson_WhenPassingSampleObject_ShouldReturnString()
        {
            var result = Logic.CommonOperations.JSON.ObjToJson(sample);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf(typeof(string)));
        }

        [Test]
        public void ObjToJson_WhenPassingSampleObject_ShouldReturnSameJSON()
        {
            var result = Logic.CommonOperations.JSON.ObjToJson(sample);

            Assert.That(result, Is.EqualTo(json));
        }
        #endregion ObjToJson
    }
}
