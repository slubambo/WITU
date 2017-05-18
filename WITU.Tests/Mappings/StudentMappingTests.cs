using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARMSv2.Core.Model;
using FluentNHibernate.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace ARMSv2.Tests.Mappings
{
    [TestClass]
    public class StudentMappingTests
    {
        private Mock<ISession> _session;

        [TestInitialize]
        private void SetUp()
        {
            _session = new Mock<ISession>();
            _session.Setup(r => r.Get<Student>(It.IsAny<int>())).Returns(new Student());
        }

            
        [TestMethod]
        public void CanCorrectlyMapStudent()
        {
            var student = new Student();

            Assert.IsNotNull(_session);

            new PersistenceSpecification<Student>(_session.Object)
                .CheckProperty(c => c.Id, 5)
                .CheckProperty(x => x.RegistrationNumber, "11/1/327/D/1839");

        }
    }
}
