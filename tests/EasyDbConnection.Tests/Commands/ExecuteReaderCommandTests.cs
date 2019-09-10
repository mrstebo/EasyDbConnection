using System.Data;
using System.Linq;
using EasyDbConnection.Commands;
using FakeItEasy;
using NUnit.Framework;

namespace EasyDbConnection.Tests.Commands
{
    [TestFixture]
    [Parallelizable]
    public class ExecuteReaderCommandTests
    {
        [SetUp]
        public void SetUp()
        {
            _connection = A.Fake<IDbConnection>();
            _command = new ExecuteReaderCommand(_connection);
        }

        private IDbConnection _connection;
        private ExecuteReaderCommand _command;
        
        [Test]
        public void Execute_runs_a_DbCommand()
        {
            var dbCommand = A.Fake<IDbCommand>();
            var dataReader = A.Fake<IDataReader>();

            A.CallTo(() => _connection.CreateCommand())
                .Returns(dbCommand);
            A.CallTo(() => dbCommand.ExecuteReader())
                .Returns(dataReader);

            var result = _command.Execute("TEST", Enumerable.Empty<DbParam>());

            Assert.AreEqual(dataReader, result);
        }

        [Test]
        public void Execute_creates_an_IDbCommand_from_the_IDbConnection()
        {
            var dbCommand = A.Fake<IDbCommand>();

            A.CallTo(() => _connection.CreateCommand())
                .Returns(dbCommand);

            _command.Execute("TEST", new[]
            {
                new DbParam("@test", DbType.String, "abc123")
            });

            A.CallToSet(() => dbCommand.CommandText).To("TEST")
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => dbCommand.Parameters.Add(
                    A<IDbDataParameter>.That.Matches(p =>
                        p.ParameterName == "@test" &&
                        p.DbType == DbType.String &&
                        p.Value.Equals("abc123"))))
                .MustHaveHappenedOnceExactly();
        }
    }
}
