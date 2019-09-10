using System.Data;
using EasyDbConnection.Events;
using FakeItEasy;
using NUnit.Framework;

namespace EasyDbConnection.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EasyDbConnectionTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = A.Fake<IDbConnection>();
            _command = A.Fake<IDbCommand>();
            _easyDbConnection = new EasyDbConnection(_connection);

            A.CallTo(() => _connection.CreateCommand())
                .Returns(_command);
        }

        private IDbConnection _connection;
        private IDbCommand _command;
        private IEasyDbConnection _easyDbConnection;

        [Test]
        public void ExecuteNonQuery_runs_ExecuteNonQuery_on_the_IDbCommand()
        {
            A.CallTo(() => _command.ExecuteNonQuery())
                .Returns(5);
            
            var result = _easyDbConnection.ExecuteNonQuery("TEST", new[]
            {
                new DbParam("@Test", DbType.String, "test")
            });

            Assert.AreEqual(5, result);

            A.CallTo(() => _command.ExecuteNonQuery())
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ExecuteNonQuery_invokes_Executing_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutingEventArgs evt = null;

            _easyDbConnection.Executing += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteNonQuery(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
        }
        
        [Test]
        public void ExecuteNonQuery_invokes_Executed_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutedEventArgs evt = null;

            _easyDbConnection.Executed += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteNonQuery(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
            Assert.IsNotNull(evt.TimeTaken);
        }

        [Test]
        public void ExecuteScalar_runs_ExecuteScalar_on_the_IDbCommand()
        {
            A.CallTo(() => _command.ExecuteScalar())
                .Returns("abc123");
            
            var result = _easyDbConnection.ExecuteScalar("TEST", new[]
            {
                new DbParam("@Test", DbType.String, "test")
            });

            Assert.AreEqual("abc123", result);

            A.CallTo(() => _command.ExecuteScalar())
                .MustHaveHappenedOnceExactly();
        }
        
        [Test]
        public void ExecuteScalar_invokes_Executing_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutingEventArgs evt = null;

            _easyDbConnection.Executing += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteScalar(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
        }
        
        [Test]
        public void ExecuteScalar_invokes_Executed_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutedEventArgs evt = null;

            _easyDbConnection.Executed += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteScalar(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
            Assert.IsNotNull(evt.TimeTaken);
        }
        
        [Test]
        public void ExecuteReader_runs_ExecuteReader_on_the_IDbCommand()
        {
            var dataReader = A.Fake<IDataReader>();
            
            A.CallTo(() => _command.ExecuteReader())
                .Returns(dataReader);
            
            var result = _easyDbConnection.ExecuteReader("TEST", new[]
            {
                new DbParam("@Test", DbType.String, "test")
            });

            Assert.AreEqual(dataReader, result);

            A.CallTo(() => _command.ExecuteReader())
                .MustHaveHappenedOnceExactly();
        }
        
        [Test]
        public void ExecuteReader_invokes_Executing_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutingEventArgs evt = null;

            _easyDbConnection.Executing += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteReader(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
        }
        
        [Test]
        public void ExecuteReader_invokes_Executed_event()
        {
            const string commandText = "TEST";
            var parameters = new[]
            {
                new DbParam("@Test", DbType.String, "test")
            };
            DbCommandExecutedEventArgs evt = null;

            _easyDbConnection.Executed += (o, e) => evt = e;
            
            _easyDbConnection.ExecuteReader(commandText, parameters);

            Assert.IsNotNull(evt);
            Assert.AreEqual(commandText, evt.CommandText);
            Assert.AreEqual(parameters, evt.Parameters);
            Assert.IsNotNull(evt.TimeTaken);
        }
    }
}
