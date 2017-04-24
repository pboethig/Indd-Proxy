namespace Indd.Tests.Functional.Helper.Json
{
    using System;
    using NUnit.Framework;
    using Indd.Service.Log;
   

    [TestFixture]
    public class SyslogTest
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
        public void Syslog_log()
        {
            Syslog.log("a unittest logentry",System.Diagnostics.EventLogEntryType.Information, 123);
        }
    }
}