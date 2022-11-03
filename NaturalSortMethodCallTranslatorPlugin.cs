#region

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;

#endregion

namespace MariaDBNaturalSortExtension
{
    public class NaturalSortMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public NaturalSortMethodCallTranslatorPlugin(ISqlExpressionFactory sqlExpressionFactory)
        {
            Translators = new List<IMethodCallTranslator>
            {
                new NaturalSortTranslator(sqlExpressionFactory)
            };
        }

        public IEnumerable<IMethodCallTranslator> Translators { get; }
    }
}