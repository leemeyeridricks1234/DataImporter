using Model;

namespace QueryBuilder
{
    public interface ISelectQueryBuilder
    {
        string GetQuery(string tableName);
        string GetQuery(string tableName, Condition condition);
    }

    public interface IInsertQueryBuilder
    {
        string GetQuery(string tableName, Row record);
        string GetQuery(string tableName, string primaryKey, Row record, bool isIdentityInsertOn);
    }

    public interface IDeleteQueryBuilder
    {
        string GetQuery(string tableName);
    }

    public interface IPrimaryKeyRelationShipQueryBuilder
    {
        string GetQuery(string tableName);
    }

    public interface IForeignKeyRelationShipQueryBuilder
    {
        string GetQuery(string tableName);
    }

    public interface IWhereQueryBuilder
    {
        string GetQuery(Condition condition);
    }
}