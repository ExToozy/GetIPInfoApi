using Microsoft.EntityFrameworkCore;


namespace GetIPInfoApi
{
    public class IPInformationDbContext : DbContext
    {
        public IPInformationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<IPInformation> QueryHistory => Set<IPInformation>();

    }
}
