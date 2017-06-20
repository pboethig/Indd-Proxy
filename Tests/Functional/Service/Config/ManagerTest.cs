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
            Assert.Throws<System.Exception>(() => ConfigManager.getJobQueuePath("asenselesspath"));
        }

        [Test]
        public void StorageManager_getWrongStoragePath()
        {
            Assert.Throws<System.Exception>(() => ConfigManager.getStoragePath("asenselesspath"));
        }

        [Test]
        public void StorageManager_getLocalIPAddress()
        {
            string ip = ConfigManager.getLocalIPAddress();

            Assert.IsNotEmpty(ip);
        }

        [Test]
        public void StorageManager_WriteNetworkInfos()
        {
            dynamic networkInfos = ConfigManager.writeNetworkInfosToSharedConfigFolder();

            Assert.IsNotEmpty(networkInfos.InDesignServerIPAddress);
        }
    }
}