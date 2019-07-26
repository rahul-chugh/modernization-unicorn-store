using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace UnicornStore.Configuration
{
    /// <summary>
    /// Defers resolution of the connection string configuration to after DI/IoC container 
    /// is built, enabling connection string configuration settings to be changed after 
    /// the application start, without needing application restart to notice changed 
    /// connection string configuration settings.
    /// </summary>
    public abstract class DbContextOptionsConfigurator
    {
        protected readonly DbConnectionStringBuilder dbConnectionStringBuilder;

        /// <summary>
        /// Instantiated by the DI container
        /// </summary>
        /// <param name="dbConnectionStringBuilder"></param>
        public DbContextOptionsConfigurator(DbConnectionStringBuilder dbConnectionStringBuilder)
        {
            this.dbConnectionStringBuilder = dbConnectionStringBuilder;
        }

        /// <summary>
        /// Override in subclasses to specify a database engine, like
        /// "optionsBuilder.UseSqlServer(this.dbConnectionStringBuilder.ConnectionString)"
        /// </summary>
        /// <param name="optionsBuilder"></param>
        internal abstract void Configure(DbContextOptionsBuilder optionsBuilder);
    }
}