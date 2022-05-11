using Blogifier.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Blogifier.Core.Data;
public abstract class AppDbContext : DbContext
{
    protected readonly IConfiguration Config;

    protected AppDbContext(IConfiguration config)
        => Config = config;

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Newsletter> Newsletters { get; set; }
    public DbSet<MailSetting> MailSettings { get; set; }
    public DbSet<PostCategory> PostCategories { get; set; }

    protected void ConfigureModel(ModelBuilder modelBuilder, string defaultDateSqlFunction)
    {
        modelBuilder.Entity<PostCategory>()
            .HasKey(t => new { t.PostId, t.CategoryId });

        modelBuilder.Entity<PostCategory>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostCategories)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostCategory>()
            .HasOne(pt => pt.Category)
            .WithMany(t => t.PostCategories)
            .HasForeignKey(pt => pt.CategoryId);

        modelBuilder.Entity<Blog>().Property(b => b.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<Post>().Property(p => p.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<Author>().Property(a => a.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<Category>().Property(c => c.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<Subscriber>().Property(s => s.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<Newsletter>().Property(n => n.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
        modelBuilder.Entity<MailSetting>().Property(n => n.DateUpdated).HasDefaultValueSql(defaultDateSqlFunction);
    }
}
