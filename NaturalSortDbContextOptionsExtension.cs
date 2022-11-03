#region

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace MariaDBNaturalSortExtension
{
    public class NaturalSortDbContextOptionsExtension : IDbContextOptionsExtension
    {
        private DbContextOptionsExtensionInfo _info;

        void IDbContextOptionsExtension.ApplyServices(IServiceCollection services)
        {
            services.AddScoped<IMethodCallTranslatorPlugin, NaturalSortMethodCallTranslatorPlugin>();
        }

        public void Validate(IDbContextOptions options) { }

        public DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            public ExtensionInfo(IDbContextOptionsExtension extension) : base(extension) { }

            public override bool IsDatabaseProvider => false;

            public override string LogFragment => $"using {nameof(NaturalSortDbContextOptionsExtension)}";

            public override int GetServiceProviderHashCode()
            {
                return nameof(NaturalSortDbContextOptionsExtension).GetHashCode();
            }

            public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
            {
                return other is ExtensionInfo;
            }

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
            {
                debugInfo["MariaDB:" + nameof(NaturalSortExtensions.AddNaturalSortSupport)] = "1";
            }
        }
    }
}