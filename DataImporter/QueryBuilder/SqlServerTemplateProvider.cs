using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Model;

namespace QueryBuilder
{
    public class SqlServerTemplateProvider : IQueryTemplateProvider
    {
        public string SelectQuery()
        {
            return string.Format(" SELECT * FROM {0} ", ConstantPlaceHolders.TableName);
        }

        public string InsertQuery()
        {
            return string.Format(
@" INSERT INTO {0} ({1}) 
 VALUES ({2})", ConstantPlaceHolders.TableName, ConstantPlaceHolders.ColumnNames, ConstantPlaceHolders.ColumnValues);
        }

        public string PrimaryKeyRelationship()
        {
            return string.Format(@"SELECT 
                        C.Constraint_Name , 
                        PK.TABLE_NAME as parentTable, PT.COLUMN_NAME as parentColumn, 
                        FK.TABLE_NAME as childTable, CU.COLUMN_NAME as childColumn
                    FROM 
                        INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK 
                            ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME 
                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK 
                            ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME 
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU 
                            ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME 
                        INNER JOIN 
                        ( 
                            SELECT 
                                i1.TABLE_NAME, i2.COLUMN_NAME 
                            FROM 
                                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 
                                INNER JOIN 
                                INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 
                                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME 
                                WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                        ) PT 
                        ON PT.TABLE_NAME = PK.TABLE_NAME 
                        AND FK.TABLE_NAME = '{0}'", ConstantPlaceHolders.TableName);
        }

        public string ForeignKeyRelationship()
        {
            return string.Format(@"SELECT 
                        C.Constraint_Name , 
                        PK.TABLE_NAME as parentTable, PT.COLUMN_NAME as parentColumn, 
                        FK.TABLE_NAME as childTable, CU.COLUMN_NAME as childColumn
                    FROM 
                        INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C 
                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK 
                            ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME 
                        INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK 
                            ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME 
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU 
                            ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME 
                        INNER JOIN 
                        ( 
                            SELECT 
                                i1.TABLE_NAME, i2.COLUMN_NAME 
                            FROM 
                                INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 
                                INNER JOIN 
                                INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 
                                ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME 
                                WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY' 
                        ) PT 
                        ON PT.TABLE_NAME = PK.TABLE_NAME 
                        AND PK.TABLE_NAME = '{0}'", ConstantPlaceHolders.TableName);
        }

        public string DeleteQuery()
        {
            return string.Format("DELETE FROM {0}", ConstantPlaceHolders.TableName);
        }

        public string WhereQuery()
        {
            return "Where ";
        }

        public string NotExistsTemplate()
        {
            return 
string.Format(@"IF NOT EXISTS(SELECT 1 FROM {0} WHERE {1} = {2}) 
BEGIN 
{3} 
END",ConstantPlaceHolders.TableName, 
ConstantPlaceHolders.ColumnName, ConstantPlaceHolders.ColumnValue, 
ConstantPlaceHolders.InsertQuery);
        }

        public string GetPrimaryKeyOfTable()
    {
        return string.Format(@" SELECT column_name
 FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE 
 WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 
 AND table_name = '{0}'", ConstantPlaceHolders.TableName);
    }

        public string IsIdentityInsertOnForTable()
        {
            return string.Format(@" SELECT 
CASE 
	WHEN COUNT(1) > 0 THEN 1
	ELSE 0
END as IsIdentityInsertOn
FROM sys.columns
WHERE object_id = OBJECT_ID('{0}', 'U') 
AND is_identity = 1", ConstantPlaceHolders.TableName);
        }

        public string GetListOfComputedColumns()
        {
            return string.Format(@" SELECT name FROM sys.columns
WHERE object_id = OBJECT_ID('InternetApplication', 'U')
AND is_computed = 1", ConstantPlaceHolders.TableName);
        }
    }
}
