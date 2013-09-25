using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace QueryBuilder
{
    public interface IQueryTemplateProvider
    {
        string SelectQuery();
        string InsertQuery();
        string PrimaryKeyRelationship();
        string ForeignKeyRelationship();
        string DeleteQuery();
        string WhereQuery();
        string NotExistsTemplate();
        string GetPrimaryKeyOfTable();
        string IsIdentityInsertOnForTable();
        string GetListOfComputedColumns();
    }
}
