using Common;
using Database;
using ProcessData;
using Processor;
using QueryBuilder;

namespace BootStrapper
{
    public class ComponentRegistry
    {
        public ComponentRegistry()
        {   
            IocContainer.RegisterInstance<IDatabaseProvider, SqlServerDbProvider>();
            IocContainer.RegisterInstance<IQueryTemplateProvider, SqlServerTemplateProvider>();
            IocContainer.RegisterInstance<IImportData, ImportData>();
            IocContainer.RegisterInstance<ISelectQueryBuilder, SelectQueryBuilder>();
            IocContainer.RegisterInstance<IInsertQueryBuilder, InsertQueryBuilder>();
            IocContainer.RegisterInstance<IDeleteQueryBuilder, DeleteQueryBuilder>();
            IocContainer.RegisterInstance<IPrimaryKeyRelationShipQueryBuilder, PrimaryKeyRelationshipQueryBuilder>();
            IocContainer.RegisterInstance<IForeignKeyRelationShipQueryBuilder, ForeignKeyRelationShipQueryBuilder>();
            IocContainer.RegisterInstance<IWhereQueryBuilder, WhereQueryBuilder>();
        }
    }
}
