using MiProyectoPruebas.Tests;
using NUnit.Framework;

namespace MiProyectoPruebas.Suites
{
    [TestFixture]
    [Category("LoginSuite")]
    public class LoginTestSuite
    {
        private LoginTests loginTests;

        [SetUp]
        public void SetUp()
        {
            loginTests = new LoginTests();
            loginTests.SetUp();
        }

        [Test]
        public void RunLoginTest()
        {
            loginTests.TearDown();
        }
    }
}