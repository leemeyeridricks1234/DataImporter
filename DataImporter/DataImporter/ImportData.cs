using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Database;
using Model;
using ProcessData;
using QueryBuilder;
using Serializer;

namespace Processor
{
    public class ImportData : IImportData
    {
        private readonly IDatabaseProvider _provider;
        private readonly IList<string> _traversedRelationship;
        private Relationships _customeDefinedRelationship = null;
        private readonly Dictionary<string, List<string>> _computedColumns = new Dictionary<string, List<string>>(); 
        public ImportData(IDatabaseProvider provider)
        {
            _provider = provider;
            _traversedRelationship = new List<string>();
        }

        readonly RecordHierarchy _hierarchy = new RecordHierarchy();
        public RecordHierarchy GetHierarchy(string tableName, Condition condition)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new NullReferenceException("TableName");

            Traverse(tableName, condition, true, true);
            return _hierarchy;
        }

        private void Traverse(string tableName, Condition condition, bool checkParent,
            bool checkChild,
            string relationShipName = null)
        {
           
            if (_traversedRelationship.Contains(relationShipName))
                return;
            _traversedRelationship.Add(relationShipName);

            var row = GetRow(tableName, condition);
            if(row==null)
                return;
            if (checkParent)
            {
                TraverseParentRelationship(tableName, row);
            }
            string primaryKeyColumn = GetPrimaryKey(tableName);
            _hierarchy.Add(new Table() { Records = row, Name = tableName, Primarykey = primaryKeyColumn, IsIdentityInsertOn = IsIdentityInsertOn(tableName) });

            if (checkChild || IsThisTableExempted(tableName))
            {
                TraverseChildRelationship(tableName, row);
            }
        }

        private bool IsThisTableExempted(string tableName)
        {
            return tableName.Equals("BrandApplication", StringComparison.CurrentCultureIgnoreCase);
        }

        private void TraverseChildRelationship(string tableName, Records row)
        {
            var foreignKeyQuery = IocContainer.Resolve<IForeignKeyRelationShipQueryBuilder>();
            var relationShips = GetRelationShips(foreignKeyQuery.GetQuery(tableName));
            foreach (var relationship in relationShips)
            {
                string value = GetValuesInCsv(row, relationship.PrimaryKeyColumn);
                if (!value.Equals(ConstantPlaceHolders.NullValue) && !string.IsNullOrEmpty(value))
                {
                    var condition = new Condition()
                    {
                        Column = relationship.ForiegnKeyColumn,
                        Value = value,
                        Operator = ConditionalOperator.In
                    };
                    Traverse(relationship.ForiegnKeyTable, condition, true, true, relationship.Name);
                }
            }
        }

        private void TraverseParentRelationship(string tableName, Records row)
        {
            var parentKeyQuery = IocContainer.Resolve<IPrimaryKeyRelationShipQueryBuilder>();
            var relationShips = GetRelationShips(parentKeyQuery.GetQuery(tableName));
            foreach (var relationship in relationShips)
            {
                var value = GetValuesInCsv(row, relationship.ForiegnKeyColumn);
                if (!value.Equals(ConstantPlaceHolders.NullValue) && !string.IsNullOrEmpty(value))
                {
                    var condition = new Condition()
                       {
                           Column = relationship.PrimaryKeyColumn,
                           Value = value,
                           Operator = ConditionalOperator.In
                       };
                    Traverse(relationship.PrimaryKeyTable, condition, true, false, relationship.Name);
                }
            }
        }
        
        private static string GetValuesInCsv(IEnumerable<Row> record, string columnName)
        {
            string value = "";
            foreach (Row row in record)
            {
                var columnValue = row[columnName];
                if(!columnValue.Equals(ConstantPlaceHolders.NullValue, StringComparison.CurrentCultureIgnoreCase))
                    value = value + string.Format("'{0}',", columnValue);
                else
                    value = value + string.Format("{0},", columnValue);
            }
            if(value.Length>0)
                value = value.Remove(value.Length - 1, 1);
            return value;
        }

        private IEnumerable<Relationship> GetRelationShips(string query)
        {
            var reader = _provider.GetReader(query);
            var relationShips = new List<Relationship>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    relationShips.Add
                        (
                            new Relationship()
                            {
                                Name = reader["Constraint_Name"].ToString(),
                                PrimaryKeyTable = reader["parentTable"].ToString(),
                                PrimaryKeyColumn = reader["parentColumn"].ToString(),
                                ForiegnKeyTable = reader["childTable"].ToString(),
                                ForiegnKeyColumn = reader["childColumn"].ToString()
                            }
                        );
                }
            }
            reader.Close();
            return relationShips;
        }

        private Records GetRow(string tableName, Condition condition)
        {
            if (!_computedColumns.ContainsKey(tableName))
                _computedColumns.Add(tableName, GetListOfComputedColumns(tableName));
            var selectQueryBuilder = IocContainer.Resolve<ISelectQueryBuilder>();
            var reader = _provider.GetReader(selectQueryBuilder.GetQuery(tableName, condition));
            var records = new Records();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var row = new Row();
                    for (int column = 0; column < reader.VisibleFieldCount; column++)
                    {
                        var columnName = reader.GetName(column);
                        if(!IsColumnComputed(tableName, columnName))
                        {
                            row.Add(columnName, reader[column] == DBNull.Value
                            ? ConstantPlaceHolders.NullValue : reader[column].ToString());
                        }
                    }
                    records.Add(row);
                }
            }
            reader.Close();
            return records;
        }

        private bool IsColumnComputed(string tableName, string columnName)
        {
            if (!_computedColumns.ContainsKey(tableName))
            {
                return false;
            }

            var listOfColumns = _computedColumns[tableName];
            return listOfColumns.Any(x => x.Equals(columnName));
        }

        private List<string> GetListOfComputedColumns(string tableName)
        {
            var columns = new List<string>();
            var selectQueryBuilder = IocContainer.Resolve<IQueryTemplateProvider>();
            var reader = _provider.GetReader(selectQueryBuilder.GetListOfComputedColumns().Replace(ConstantPlaceHolders.TableName, tableName));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    columns.Add(reader["name"].ToString());
                }
            }
            reader.Close();

            return columns;
        }

        private string GetPrimaryKey(string tableName)
        {
            var selectQueryBuilder = IocContainer.Resolve<IQueryTemplateProvider>();
            var reader = _provider.GetReader(selectQueryBuilder.GetPrimaryKeyOfTable().Replace(ConstantPlaceHolders.TableName, tableName));
            string columnName = string.Empty;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    columnName = reader["column_name"].ToString();
                }
                while (reader.Read())
                {

                }
            }
            reader.Close();
            return columnName;
        }

        private bool IsIdentityInsertOn(string tableName)
        {
            var selectQueryBuilder = IocContainer.Resolve<IQueryTemplateProvider>();
            var reader = _provider.GetReader(selectQueryBuilder.IsIdentityInsertOnForTable().Replace(ConstantPlaceHolders.TableName, tableName));
            bool isIdentityInsertOn = false;
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    isIdentityInsertOn = reader["IsIdentityInsertOn"].ToString() == "1";
                }
                while (reader.Read())
                {

                }
            }
            reader.Close();
            return isIdentityInsertOn;
        }
    }
}
