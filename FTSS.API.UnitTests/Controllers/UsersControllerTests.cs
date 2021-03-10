using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTSS.API.UnitTests.Controllers
{
    [TestFixture]
    class UsersControllerTests
    {
        private API.Controllers.UsersController userController;

        [SetUp]
        public void Setup()
        {
            userController = new API.Controllers.UsersController(dbCTX: null, logger: null, null, null, null);
        }

        [Test]
        public void Login_WhenCtxAndLoggerIsNull_ShouldThrowsException()
        {
            Assert.That(() => { userController.Login(null); }, Throws.Exception);
        }
    }
}
