
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Point.API.Identity.Context;

namespace Point.API.Identity.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var conn = "Server=(localdb)\\mssqllocaldb;Database=PointDb;";

            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();

            optionsBuilder.UseSqlServer(conn);
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}
