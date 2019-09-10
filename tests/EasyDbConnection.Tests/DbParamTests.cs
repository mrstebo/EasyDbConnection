using System.Data;
using NUnit.Framework;

namespace EasyDbConnection.Tests
{
    [TestFixture]
    [Parallelizable]
    public class DbParamTests
    {
        [Test]
        public void Constructor_without_DbType_sets_it_to_a_default_value()
        {
            var dbParam = new DbParam("@name", "Test");
            
            Assert.AreEqual(DbType.Object, dbParam.DbType);
        }

        [Test]
        public void Constructor_sets_object_properties()
        {
            var dbParam = new DbParam("@name", DbType.String, "Test");
            
            Assert.AreEqual("@name", dbParam.ParameterName);
            Assert.AreEqual(DbType.String, dbParam.DbType);
            Assert.AreEqual("Test", dbParam.Value);
        }
    }
}
