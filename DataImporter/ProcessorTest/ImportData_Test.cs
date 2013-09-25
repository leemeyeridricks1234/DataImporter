using System;
using BootStrapper;
using Common;
using Model;
using NUnit.Framework;
using ProcessData;

namespace ProcessorTest
{
    [TestFixture]
    public class ImportData_Test
    {
        public void Arrange()
        {
            new ComponentRegistry();
        }

        [Test]
        public void CanGetInstanceOfIImportData()
        {
            Arrange();
            var importData = IocContainer.Resolve<IImportData>();

            Assert.IsInstanceOf<IImportData>(importData);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void ImportData_Throws_Exception_TableNameIsnull()
        {
            Arrange();
            var importData = IocContainer.Resolve<IImportData>();
            importData.GetHierarchy(null, null);
        }

        [Test]
        public void ImportData_Returns_UplCustomer_For_Applcation()
        {
            Arrange();
            var importData = IocContainer.Resolve<IImportData>();
            var condition = new Condition()
                {
                    Column = "ApplicationId",
                    Operator = ConditionalOperator.In,
                    Value = "290001, 290002"
                };
            var rows = importData.GetHierarchy("TransactCall", condition);
        }
    }
}