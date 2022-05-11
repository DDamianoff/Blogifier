using Blogifier.Core.Data;
using Blogifier.Core.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Blogifier.Shared.Exceptions;

namespace Blogifier.Core.Extensions
{
	public static class ServiceCollectionExtensions
	{
      public static IServiceCollection AddBlogDatabase(this IServiceCollection services, IConfiguration configuration)
      {
          var provider = configuration
              .GetSection("Blogifier")
              .GetValue<string>("DbProvider")
              .ToUpper();

            switch (provider)
            {
                case "SQLSERVER":
                    Serilog.Log.Information("Selected SQLSERVER as dataBase provider");
                    services.AddDbContext<AppDbContext, SqlServerContext>();
                    break;

                case "SQLITE":
                    Serilog.Log.Information("Selected SQLITE as dataBase provider");
                    services.AddDbContext<AppDbContext, SqLiteContext>();
                    break;

                case "POSTGRE":
                case "POSTGRES":
                case "POSTGRESQL":
                    Serilog.Log.Error("Attempted to use PostgresSQL as ataBase provider");
                    throw new DiscardedFunctionalityException(
                        "PostgresSQL is not supported in this fork of the project");
                case "MYSQL":
                    Serilog.Log.Error("Attempted to use MySql as dataBase provider");
                    throw new DiscardedFunctionalityException(
                        "MySQL is not supported in this fork of the project");

                default:
                    Serilog.Log.Fatal("Failed to configure dataBase provider, possibly bad config file");
                    throw new Exception(
                        $"Missing or bad configuration: DataBase type \"{provider}\" not supported, misspelled or null");
            }
            services.AddDatabaseDeveloperPageExceptionFilter();
			return services;
      }

		public static IServiceCollection AddBlogProviders(this IServiceCollection services)
		{
			services.AddScoped<IAuthorProvider, AuthorProvider>();
			services.AddScoped<IBlogProvider, BlogProvider>();
			services.AddScoped<IPostProvider, PostProvider>();
			services.AddScoped<IStorageProvider, StorageProvider>();
			services.AddScoped<IFeedProvider, FeedProvider>();
			services.AddScoped<ICategoryProvider, CategoryProvider>();
			services.AddScoped<IAnalyticsProvider, AnalyticsProvider>();
			services.AddScoped<INewsletterProvider, NewsletterProvider>();
			services.AddScoped<IEmailProvider, MailKitProvider>();
			services.AddScoped<IThemeProvider, ThemeProvider>();
			services.AddScoped<ISyndicationProvider, SyndicationProvider>();
			services.AddScoped<IAboutProvider, AboutProvider>();

			return services;
		}
	}
}
