using BootStrapper;
using Common;
using Moq;
using NUnit.Framework;
using QueryBuilder;

namespace QueryBuilderTest
{
    [TestFixture]
    public class SelectQueryBuilderTest
    {
        private ISelectQueryBuilder _selectQueryBuilder;
        private Mock<IQueryTemplateProvider> _templateProvider;
        public void Arrange()
        {
            _templateProvider = new Mock<IQueryTemplateProvider>();
            new StubComponentRegistry();
            IocContainer.RegisterSingletonInstance(_templateProvider.Object);
            _selectQueryBuilder = IocContainer.Resolve<ISelectQueryBuilder>();
        }

        [Test]
        public void GetQuery()
        {
            Arrange();
            const string tableName = "application";
            var testQuery = string.Format("{0} table should be here", ConstantPlaceHolders.TableName);
            _templateProvider.Setup(x => x.SelectQuery()).Returns(testQuery);
            var selectQuery = _selectQueryBuilder.GetQuery(tableName);
            Assert.AreEqual(selectQuery, "application table should be here");
        }
    }
}