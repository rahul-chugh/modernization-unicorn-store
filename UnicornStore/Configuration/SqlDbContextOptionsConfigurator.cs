using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace UnicornStore.Configuration
{
    public class SqlDbContextOptionsConfigurator : DbContextOptionsConfigurator
    {
        public SqlDbContextOptionsConfigurator(DbConnectionStringBuilder dbConnectionStringBuilder) 
            : base(dbConnectionStringBuilder)
        {
        }

        internal override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.dbConnectionStringBuilder.ConnectionString);
        }
    }
}
