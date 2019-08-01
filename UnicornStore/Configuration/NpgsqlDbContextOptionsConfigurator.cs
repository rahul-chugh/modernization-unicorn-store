using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UnicornStore.Configuration
{
    public class NpgsqlDbContextOptionsConfigurator : DbContextOptionsConfigurator
    {
        private readonly ILogger<NpgsqlDbContextOptionsConfigurator> logger;

        public NpgsqlDbContextOptionsConfigurator(
            DbConnectionStringBuilder dbConnectionStringBuilder,
            ILogger<NpgsqlDbContextOptionsConfigurator> logger
            ) 
            : base(dbConnectionStringBuilder)
        {
            this.logger = logger;
        }

        internal override void Configure(DbContextOptionsBuilder optionsBuilder)
        {
            this.logger.LogInformation("Connection info: Server=\"{Server}\", Port=\"{Port}\", Database=\"{Database}\", User=\"{User}\"",
                this.dbConnectionStringBuilder["Host"],
                this.dbConnectionStringBuilder["Port"],
                this.dbConnectionStringBuilder["Database"],
                this.dbConnectionStringBuilder["Username"]
                );

            optionsBuilder.UseNpgsql(this.dbConnectionStringBuilder.ConnectionString);
        }
    }
}
