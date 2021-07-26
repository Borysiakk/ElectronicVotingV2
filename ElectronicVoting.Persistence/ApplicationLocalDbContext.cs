
using ElectronicVoting.Common.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectronicVoting.Persistence
{
    public class ApplicationLocalDbContext :DbContext
    {
        public DbSet<ValidatorEntities> Validators { get; set; }
        public DbSet<InitialTransactionEntities> InitialTransactions { get; set; }

        public ApplicationLocalDbContext(DbContextOptions<ApplicationLocalDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);
        }
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationLocalDbContext>
    {
        public ApplicationLocalDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationLocalDbContext>().UseSqlite("Data Source=LocalDb.db");
            return new ApplicationLocalDbContext(builder.Options);
        }
    }
}