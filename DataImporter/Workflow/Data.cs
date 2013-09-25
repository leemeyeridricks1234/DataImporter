using System;
using BootStrapper;
using Common;
using Model;
using ProcessData;
using QueryBuilder;

namespace Workflow
{
    public class Data
    {
        public Data()
        {
            new ComponentRegistry();
        }

        public void Import()
        {
            var importData = IocContainer.Resolve<IImportData>();
            var insertQueryBuilder = IocContainer.Resolve<IInsertQueryBuilder>();
            var condition = new Condition
                {
                    Column = "IDNumber",
                    Operator = ConditionalOperator.EqualTo,
                    Value = "8410045076083"
                };
            var records = importData.GetHierarchy("BaseCustomer", condition);

            foreach (var record in records)
            {
                foreach (var row in record.Records)
                {
                    Console.WriteLine(insertQueryBuilder.GetQuery(record.Name, record.Primarykey, row, record.IsIdentityInsertOn));
                }
            }
        }
    }
}