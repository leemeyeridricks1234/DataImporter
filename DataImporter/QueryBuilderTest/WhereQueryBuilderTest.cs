using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootStrapper;
using Common;
using Model;
using Moq;
using NUnit.Framework;
using QueryBuilder;

namespace QueryBuilderTest
{
    [TestFixture]
    public class WhereQueryBuilderTest
    {
        public Mock<IQueryTemplateProvider> _templateProvider;
        public IWhereQueryBuilder _whereQueryBuilder;
        public void Arrange()
        {
            _templateProvider = new Mock<IQueryTemplateProvider>();
            new StubComponentRegistry();
            IocContainer.RegisterSingletonInstance(_templateProvider.Object);
            _whereQueryBuilder = IocContainer.Resolve<IWhereQueryBuilder>();
        }

        [Test]
        public void GetQuery_Replace_Operator()
        {
            Arrange();
            const string templateQuery = "Where";
            _templateProvider.Setup(x => x.WhereQuery()).Returns(templateQuery);
            var condition = new Condition()
                {
                    Column = "ApplicationId",
                    Operator = ConditionalOperator.EqualTo,
                    Value = "29"
                };

            string outputQuery = _whereQueryBuilder.GetQuery(condition);

            Assert.AreEqual("Where ApplicationId='29'", outputQuery);
        }

        [Test]
        public void GetQuery_Replace_Between_Operator()
        {
            Arrange();
            const string templateQuery = "Where";
            _templateProvider.Setup(x => x.WhereQuery()).Returns(templateQuery);
            var condition = new Condition()
            {
                Column = "ApplicationId",
                Operator = ConditionalOperator.Between,
                MaxValue = "30",
                MinValue = "29"
            };

            string outputQuery = _whereQueryBuilder.GetQuery(condition);

            Assert.AreEqual("Where ApplicationId between '29' and '30'", outputQuery);
        }
    }
}
