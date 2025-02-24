using MiProyectoPruebas.Tests;
using NUnit.Framework;

namespace MiProyectoPruebas.Suites
{
    [TestFixture]
    [Category("DynamicSuite")]
    public class DynamicIDTestSuite
    {
        private DynamicIDTest dynamicIDTest;

        [SetUp]
        public void SetUp()
        {
            dynamicIDTest = new DynamicIDTest();
            dynamicIDTest.SetUp();
        }

        [Test]
        public void RunDynamicIDTest()
        {
            dynamicIDTest.VerifyDynamicHeaderIsDisplayed();
        }

        [TearDown]
        public void TearDown()
        {
            dynamicIDTest.TearDown();
        }
    }
}