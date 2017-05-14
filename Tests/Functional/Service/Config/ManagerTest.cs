namespace Indd.Tests.Functional.Service.Config
{
    using NUnit.Framework;
    using ConfigManager = Indd.Service.Config.Manager;
    using System;
    using System.Security.Principal;
    [TestFixture]
    public class ManagerTests
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
        public void StorageConfigManager_load()
        {
            dynamic config = ConfigManager.load("storage");

            Assert.IsNotNull(config);
        }

        [Test]
        
        public void StorageConfigManager_loadFails()
        {
            Assert.Throws<System.IO.FileNotFoundException>(() => ConfigManager.load("storssssssage"));
        }

        [Test]
        public void StorageConfigManager_getStoragePath()
        {
            string templateStoragePath = ConfigManager.getStoragePath("templates");
            
            Assert.IsTrue(System.IO.Directory.Exists(templateStoragePath));
        }

        [Test]
        public void StorageConfigManager_getJobQueuePath()
        {
            string jobQueuePath = ConfigManager.getJobQueuePath("in");

            Assert.IsTrue(System.IO.Directory.Exists(jobQueuePath));
        }

        [Test]
        public void StorageConfigManager_getWrongJobQueuePath()
        {
            Assert.Throws<Indd.Exception.StoragePathNotFoundException>(() => ConfigManager.getJobQueuePath("asenselesspath"));
        }

        [Test]
        public void StorageManager_getWrongStoragePath()
        {
            Assert.Throws<Indd.Exception.StoragePathNotFoundException>(() => ConfigManager.getStoragePath("asenselesspath"));
        }
    }
}