using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QueryBuilder
{
    public enum QueryType
    {
        Select,
        Insert,
        Delete, 
        ForeignKey,
        PrimaryKey,
        Where
    }
}
