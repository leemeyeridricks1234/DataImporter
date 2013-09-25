using System;
using System.Collections.Generic;
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

        public List<string> Import(string idNumber, ConditionalOperator conditionalOperator)
        {
            var importData = IocContainer.Resolve<IImportData>();
            var insertQueryBuilder = IocContainer.Resolve<IInsertQueryBuilder>();
            var condition = new Condition
                {
                    Column = "IDNumber",
                    Operator = conditionalOperator,
                    Value = idNumber
                };
            var records = importData.GetHierarchy("BaseCustomer", condition);
            var queries = new List<string>();
            foreach (var record in records)
            {
                foreach (var row in record.Records)
                {
                    queries.Add(insertQueryBuilder.GetQuery(record.Name, record.Primarykey, row, record.IsIdentityInsertOn));
                }
            }
            return queries;
        }
    }
}