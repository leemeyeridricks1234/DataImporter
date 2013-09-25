using System.Collections.Generic;
using Model;

namespace ProcessData
{
    public interface IImportData
    {
        RecordHierarchy GetHierarchy(string tableName, Condition condition);
    }
}
