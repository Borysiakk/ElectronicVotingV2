
using ElectronicVoting.Common.Model.Entities;
using Microsoft.EntityFrameworkCore;

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
}