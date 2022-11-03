#region

using Microsoft.EntityFrameworkCore.Infrastructure;

#endregion

namespace MariaDBNaturalSortExtension
{
    public static class NaturalSortExtensions
    {
        internal const string NaturalSortDatabaseFunctionName = "NATURAL_SORT_KEY";

        public static string UseNaturalSort(this string str)
        {
            return str;
        }

        public static MySqlDbContextOptionsBuilder AddNaturalSortSupport(
            this MySqlDbContextOptionsBuilder optionsBuilder)
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure)
                optionsBuilder;

            var builder = (IDbContextOptionsBuilderInfrastructure)
                infrastructure.OptionsBuilder;

            var extension = infrastructure.OptionsBuilder.Options
                                .FindExtension<NaturalSortDbContextOptionsExtension>()
                            ?? new NaturalSortDbContextOptionsExtension();

            builder.AddOrUpdateExtension(extension);

            return optionsBuilder;
        }
    }
}