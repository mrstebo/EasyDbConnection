using System.Data;
using FakeItEasy;
using NUnit.Framework;

namespace EasyDbConnection.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EasyDbConnectionFactoryTests
    {
        [Test]
        public void Create_returns_an_EasyDbConnection()
        {
            var connection = A.Fake<IDbConnection>();

            var result = EasyDbConnectionFactory.Create(connection);
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<EasyDbConnection>(result);
        }
    }
}