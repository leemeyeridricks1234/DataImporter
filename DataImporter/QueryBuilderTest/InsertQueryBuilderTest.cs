using System;
using BootStrapper;
using Common;
using Model;
using Moq;
using NUnit.Framework;
using QueryBuilder;

namespace QueryBuilderTest
{
    [TestFixture]
    public class InsertQueryBuilderTest
    {
        private IInsertQueryBuilder _insertQueryBuilder;
        private Mock<IQueryTemplateProvider> _templateProvider;

        public void Arrange()
        {
            _templateProvider = new Mock<IQueryTemplateProvider>();
            new StubComponentRegistry();
            IocContainer.RegisterSingletonInstance(_templateProvider.Object);
            _insertQueryBuilder = IocContainer.Resolve<IInsertQueryBuilder>();
        }

        [Test]
        public void GetQuery()
        {
            Arrange();
            string templateQuery = 
                string.Format("{0} Table. ColumnName {1}. Values {2}",
                ConstantPlaceHolders.TableName, ConstantPlaceHolders.ColumnNames, 
                ConstantPlaceHolders.ColumnValues);
            string tableName = "application";
            var row = new Row()
                {
                    {"ApplicationId", "10000"}
                };
            _templateProvider.Setup(x => x.InsertQuery()).Returns(templateQuery);

            string outputQuery = _insertQueryBuilder.GetQuery(tableName, row);

            Assert.AreEqual(outputQuery, "application Table. ColumnName ApplicationId. Values '10000'");
        }

        [Test]
        public void GetQuery_TestFormat_WithMultipleColumns()
        {
            Arrange();
            string templateQuery =
                string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                ConstantPlaceHolders.TableName, ConstantPlaceHolders.ColumnNames,
                ConstantPlaceHolders.ColumnValues);
            const string tableName = "application";
            _templateProvider.Setup(x => x.InsertQuery()).Returns(templateQuery);
            var row = new Row()
                {
                    {"ApplicationId", "10000"},
                    {"PersonId", "99"}
                };
            

            string outputQuery = _insertQueryBuilder.GetQuery(tableName, row);

            Assert.AreEqual(outputQuery, "INSERT INTO application (ApplicationId,PersonId) VALUES ('10000','99')");
        }


        [Test]
        public void GetQuery_TestFormat_WithMultipleColumns_SingleQuoteInCode()
        {
            Arrange();
            string templateQuery =
                string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                ConstantPlaceHolders.TableName, ConstantPlaceHolders.ColumnNames,
                ConstantPlaceHolders.ColumnValues);
            const string tableName = "application";
            _templateProvider.Setup(x => x.InsertQuery()).Returns(templateQuery);
            var row = new Row()
                {
                    {"ApplicationId", "10000"},
                    {"PersonId", "99"},
                    {"Description", "craig's"}
                };


            string outputQuery = _insertQueryBuilder.GetQuery(tableName, row);

            Assert.AreEqual(outputQuery, "INSERT INTO application (ApplicationId,PersonId,Description) VALUES ('10000','99','craig''s')");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetQuery_ShouldThrowException_When_RowIsNullOrEmpty()
        {
            Arrange();
            string templateQuery =
               string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
               ConstantPlaceHolders.TableName, ConstantPlaceHolders.ColumnNames,
               ConstantPlaceHolders.ColumnValues);
            _templateProvider.Setup(x => x.InsertQuery()).Returns(templateQuery);
            var row = new Row();

            _insertQueryBuilder.GetQuery("test", row);
        }
    }
}