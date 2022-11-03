#region

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

#endregion

namespace MariaDBNaturalSortExtension
{
    public class NaturalSortTranslator : IMethodCallTranslator
    {
        private static readonly MethodInfo UseNaturalSortMethodInfo
            = typeof(NaturalSortExtensions).GetMethod(
                nameof(NaturalSortExtensions.UseNaturalSort), new[] { typeof(string) });

        private readonly ISqlExpressionFactory _sqlExpressionFactory;

        public NaturalSortTranslator(ISqlExpressionFactory sqlExpressionFactory)
        {
            _sqlExpressionFactory = sqlExpressionFactory;
        }

        public SqlExpression Translate(SqlExpression instance, MethodInfo method,
            IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            if (method != UseNaturalSortMethodInfo)
            {
                return null;
            }

            return _sqlExpressionFactory.Function(
                NaturalSortExtensions.NaturalSortDatabaseFunctionName,
                arguments,
                true,
                arguments.Select(_ => true).ToArray(),
                method.ReturnType,
                arguments[0].TypeMapping);
        }
    }
}