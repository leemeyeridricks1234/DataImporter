using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootStrapper;
using Common;
using Database;
using NUnit.Framework;

namespace DatabaseTest
{
    [TestFixture]
    public class SqlDbProviderTest
    {
        public void Arrange()
        {
            new ComponentRegistry();
        }

        [Test]
        public void CanGetInstanceOfSqlDbProviderTest()
        {
            Arrange();
            var dbProvider = IocContainer.Resolve<IDatabaseProvider>();
            Assert.IsInstanceOf<SqlServerDbProvider>(dbProvider);
        }

        [Test]
        public void GetOneRecord_returns_OneRecord()
        {
            Arrange();
            var dbProvider = IocContainer.Resolve<IDatabaseProvider>();
            var reader = dbProvider.GetReader("Select top 1 * from Application");

            Assert.IsTrue(reader.HasRows);
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            Assert.AreEqual(1, count);
        }
    }
}
