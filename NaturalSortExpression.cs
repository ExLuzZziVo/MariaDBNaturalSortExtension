#region

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

#endregion

namespace MariaDBNaturalSortExtension
{
    public class NaturalSortExpression : Expression
    {
        private readonly Expression _value;

        public NaturalSortExpression(Expression value)
        {
            _value = value;
        }

        protected override Expression VisitChildren(ExpressionVisitor visitor)
        {
            var visitedValue = visitor.Visit(_value);

            if (ReferenceEquals(_value, visitedValue))
            {
                return this;
            }

            return new NaturalSortExpression(visitedValue);
        }

        protected override Expression Accept(ExpressionVisitor visitor)
        {
            visitor.Visit(new SqlFragmentExpression($"{NaturalSortExtensions.NaturalSortDatabaseFunctionName}("));

            visitor.Visit(_value);

            visitor.Visit(new SqlFragmentExpression(")"));

            return this;
        }
    }
}