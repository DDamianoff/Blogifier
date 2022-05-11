using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blogifier.Core.Data;

public sealed class SqLiteContext : AppDbContext
{
    public SqLiteContext(IConfiguration config) : base(config)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(Config.GetConnectionString("SQLITE"));
        base.OnConfiguring(optionsBuilder);
    }
    // TODO: Possible Sql Injection
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => ConfigureModel(modelBuilder, "DATE('now')");
}
