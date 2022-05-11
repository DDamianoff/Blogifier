using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blogifier.Core.Data;

public sealed class SqlServerContext : AppDbContext
{
    public SqlServerContext (IConfiguration config) : base(config) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(Config.GetConnectionString("SQLSERVER"));
        base.OnConfiguring(optionsBuilder);
    }

    // TODO: Possible Sql Injection
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => ConfigureModel(modelBuilder, "getdate()");
}
