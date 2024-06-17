using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Schola.EntityFrameworkCore
{
    public static class ScholaDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ScholaDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ScholaDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
