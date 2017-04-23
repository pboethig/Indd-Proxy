namespace Indd.Tests.Functional.Service.IndesignServer
{
    using NUnit.Framework;
    using IndesignServer;
    using Indd.Service.IndesignServerWrapper;

    [TestFixture]
    public class ApplicationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void ApplicationManager_createInstance()
        {
            for(int i = 0; i < 100; i++) { 

            ApplicationMananger manager = new ApplicationMananger();

            InDesignServer.Application app = manager.createInstance();

            Assert.IsNotNull(app);

            }
        }
    }
}