using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryBuilder
{
    public class DeleteQueryBuilder : IDeleteQueryBuilder
    {

        string IDeleteQueryBuilder.GetQuery(string tableName)
        {
            throw new NotImplementedException();
        }
    }
}
