using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Indd.Service.RabbitMQ;

namespace Indd.Tests.Functional.Service.RabbitMQ
{
    class ConsumerTest : TestAbstract
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
        public void Service_RabbitMQ_Consumer()
        {
            Consumer consumer = new Consumer();
            
            //Assert.IsNotNull(consumer);
        }
        
    }
}
