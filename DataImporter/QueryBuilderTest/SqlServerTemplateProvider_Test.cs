using System.Collections.Generic;
using BootStrapper;
using Common;
using Model;
using NUnit.Framework;
using QueryBuilder;

namespace QueryBuilderTest
{
    [TestFixture]
    public class SqlServerTemplateProviderTest
    {
        private void Arrange()
        {
            new ComponentRegistry();
        }

        [Test]
        public void CanGetSqlServerTemplateType()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            Assert.IsInstanceOf<SqlServerTemplateProvider>(templateProvider);
        }

        [Test]
        public void SelectQuery_IsCorrect_Start()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            var query = templateProvider.SelectQuery();
            Assert.That(query.ToLower().Contains("select"), query);
        }

        [Test]
        public void InsertQuery_IsCorrect_Start()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            var query = templateProvider.InsertQuery();
            Assert.That(query.ToLower().Contains("insert"), query);
        }

        [Test]
        public void PrimaryKeyRelationship_IsCorrect_Start()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            var query = templateProvider.PrimaryKeyRelationship();
            Assert.That(query.Contains("FK.TABLE_NAME = '"), query);
        }

        [Test]
        public void ForeignKeyRelationship_IsCorrect_Start()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            var query = templateProvider.ForeignKeyRelationship();
            Assert.That(query.Contains("PK.TABLE_NAME = '"), query);
        }

        [Test]
        public void DeleteQuery_IsCorrect_Start()
        {
            Arrange();
            var templateProvider = IocContainer.Resolve<IQueryTemplateProvider>();

            var query = templateProvider.DeleteQuery();
            Assert.That(query.ToLower().Contains("delete"), query);
        }
    }
}