using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootStrapper;
using Common;
using QueryBuilder;

namespace QueryBuilderTest
{
    public class StubComponentRegistry
    {
        public StubComponentRegistry()
        {
            IocContainer.RegisterInstance<ISelectQueryBuilder, SelectQueryBuilder>();
            IocContainer.RegisterInstance<IInsertQueryBuilder, InsertQueryBuilder>();
            IocContainer.RegisterInstance<IDeleteQueryBuilder, DeleteQueryBuilder>();
            IocContainer.RegisterInstance<IPrimaryKeyRelationShipQueryBuilder, PrimaryKeyRelationshipQueryBuilder>();
            IocContainer.RegisterInstance<IWhereQueryBuilder, WhereQueryBuilder>();
        }
    }
}
