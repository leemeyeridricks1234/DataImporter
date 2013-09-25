using System;
using System.Linq;
using Common;
using Model;

namespace QueryBuilder
{
    public class InsertQueryBuilder : IInsertQueryBuilder
    {
        private readonly IQueryTemplateProvider _queryTemplateProvider;

        public InsertQueryBuilder(IQueryTemplateProvider queryTemplateProvider)
        {
            _queryTemplateProvider = queryTemplateProvider;
        }

        string IInsertQueryBuilder.GetQuery(string tableName, Row row)
        {
            return GetQuery(tableName, null, row, false);
        }

        public string GetQuery(string tableName, string primaryKey, Row row, bool isIdentityInsertOn)
        {
            if (row == null || !row.Any())
                throw new ArgumentNullException("row");
            string columnNames = "";
            string values = "";
            string insertQuery = _queryTemplateProvider.InsertQuery();
            string notExistsQuery = _queryTemplateProvider.NotExistsTemplate();
            foreach (var column in row)
            {
                columnNames = string.Format("{0}{1},", columnNames, column.Key);
                values = string.Format(column.Value.Equals(ConstantPlaceHolders.NullValue) ? "{0}{1}," : "{0}'{1}',", values, column.Value.EncodeForInsertQuery());
            }
            

            columnNames = columnNames.Substring(0, columnNames.Length - 1);
            values = values.Substring(0, values.Length - 1);

            
            insertQuery = insertQuery.Replace(ConstantPlaceHolders.TableName, tableName);
            insertQuery = insertQuery.Replace(ConstantPlaceHolders.ColumnNames, columnNames);
            insertQuery = insertQuery.Replace(ConstantPlaceHolders.ColumnValues, values);

            if (!string.IsNullOrEmpty(primaryKey))
            {
                notExistsQuery = notExistsQuery.Replace(ConstantPlaceHolders.TableName, tableName);
                notExistsQuery = notExistsQuery.Replace(ConstantPlaceHolders.ColumnName, primaryKey);
                notExistsQuery = notExistsQuery.Replace(ConstantPlaceHolders.ColumnValue, row[primaryKey]);
                notExistsQuery = notExistsQuery.Replace(ConstantPlaceHolders.InsertQuery, insertQuery);
                if(isIdentityInsertOn)
                {
                    notExistsQuery = string.Format(@"{0} 
{1} 
{2}", string.Format("SET IDENTITY_INSERT {0} ON", tableName), notExistsQuery, string.Format("SET IDENTITY_INSERT {0} OFF", tableName));
                }
                return notExistsQuery;
            }
            
            return insertQuery;
        }
    }
}