using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace QueryBuilder
{
    public class ForeignKeyRelationShipQueryBuilder : IForeignKeyRelationShipQueryBuilder
    {
        private readonly IQueryTemplateProvider _queryTemplateProvider;
        public ForeignKeyRelationShipQueryBuilder(IQueryTemplateProvider queryTemplateProvider)
        {
            _queryTemplateProvider = queryTemplateProvider;
        }

        public string GetQuery(string tableName)
        {
            return _queryTemplateProvider.ForeignKeyRelationship().Replace(ConstantPlaceHolders.TableName, tableName);
        }
    }
}
