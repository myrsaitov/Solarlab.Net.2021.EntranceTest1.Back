using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Context
{
    /// <summary>
    /// Фабрика для создания контекста БД, используется для механизма миграций
    /// </summary>
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<Context>
    {
        /// <summary>Creates a new instance of a derived context.</summary>
        /// <param name="args"> Arguments provided by the design-time service. </param>
        /// <returns> An instance of <typeparamref name="TContext" />. </returns>
        public Context CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("MyEventDb"));
            return new Context(optionsBuilder.Options);
        }
    }
}
