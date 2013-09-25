using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace QueryBuilder
{
    public class PrimaryKeyRelationshipQueryBuilder : IPrimaryKeyRelationShipQueryBuilder
    {
        private readonly IQueryTemplateProvider _queryTemplateProvider;
        public PrimaryKeyRelationshipQueryBuilder(IQueryTemplateProvider queryTemplateProvider)
        {
            _queryTemplateProvider = queryTemplateProvider;
        }
        string IPrimaryKeyRelationShipQueryBuilder.GetQuery(string tableName)
        {
            return _queryTemplateProvider.PrimaryKeyRelationship().Replace(ConstantPlaceHolders.TableName, tableName);
        }
    }
}
