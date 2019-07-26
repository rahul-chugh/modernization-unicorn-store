using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace UnicornStore.Configuration
{
    public class NpgsqlDbContextOptionsConfigurator : DbContextOptionsConfigurator
    {
        public NpgsqlDbContextOptionsConfigurator(DbConnectionStringBuilder dbConnectionStringBuilder) 
            : base(dbConnectionStringBuilder)
        {
        }

        internal override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.dbConnectionStringBuilder.ConnectionString);
        }
    }
}
