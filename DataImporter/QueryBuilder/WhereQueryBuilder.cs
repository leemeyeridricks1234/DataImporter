using System;
using Model;

namespace QueryBuilder
{
    public class WhereQueryBuilder : IWhereQueryBuilder
    {
        private readonly IQueryTemplateProvider _queryTemplateProvider;

        public WhereQueryBuilder(IQueryTemplateProvider queryTemplateProvider)
        {
            _queryTemplateProvider = queryTemplateProvider;
        }

        string IWhereQueryBuilder.GetQuery(Condition condition)
        {
            string query = _queryTemplateProvider.WhereQuery();
            string condString;
            switch (condition.Operator)
            {
                case ConditionalOperator.EqualTo:
                    condString = string.Format("{0}='{1}'", condition.Column, condition.Value);
                    break;
                case ConditionalOperator.Between:
                    condString = string.Format("{0} between '{1}' and '{2}'", condition.Column, condition.MinValue, condition.MaxValue);
                    break;
                case ConditionalOperator.In:
                    condString = string.Format("{0} in ({1})", condition.Column, condition.Value);
                    break;
                default:
                    throw new NotImplementedException("Not Implmented");
            }
            return string.Format("{0} {1}", query, condString);
        }
    }
}