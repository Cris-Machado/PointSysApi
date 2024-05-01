
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Point.API.Data.Context
{
    public class ConnectionFactory : IDesignTimeDbContextFactory<PointContext>
    {
        public PointContext CreateDbContext(string[] args)
        {
            var conn = "Server=(localdb)\\mssqllocaldb;Database=IPointDb;";
            var optionsBuilder = new DbContextOptionsBuilder<PointContext>();

            optionsBuilder.UseSqlServer(conn);
            return new PointContext(optionsBuilder.Options);
        }
    }
}
