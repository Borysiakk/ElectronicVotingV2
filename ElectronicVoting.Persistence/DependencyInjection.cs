using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Persistence
{
    public static class DependencyInjection
    {
        public static string DbConnection = @"Data Source=c:\mydb.db;Version=3;";
        
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(""));
            return services;
        }
    }
}