using Common;

namespace QueryBuilder
{
    public class Query
    {
        public Query()
        {
        }

        public string ProcessQuery(string query, string tableName, 
            string columnNames = null, string columnValues = null)
        {
            string temp = query.Replace(ConstantPlaceHolders.TableName, tableName);
            return temp;
        }
    }
}