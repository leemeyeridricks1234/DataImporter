using Common;
using Model;

namespace QueryBuilder
{
    public class SelectQueryBuilder : ISelectQueryBuilder
    {
        private readonly IQueryTemplateProvider _queryTemplateProvider;
        private readonly IWhereQueryBuilder _whereQueryBuilder;
        private readonly Query _query;

        public SelectQueryBuilder(IQueryTemplateProvider queryTemplateProvider,
            IWhereQueryBuilder whereQueryBuilder)
        {
            _queryTemplateProvider = queryTemplateProvider;
            _whereQueryBuilder = whereQueryBuilder;
            _query = new Query();
        }

        public string GetQuery(string tableName)
        {
            return _query.ProcessQuery(_queryTemplateProvider.SelectQuery(), tableName);
        }

        public string GetQuery(string tableName, Condition condition)
        {
            return string.Format("{0} {1}", this.GetQuery(tableName), _whereQueryBuilder.GetQuery(condition));
        }
    }
}