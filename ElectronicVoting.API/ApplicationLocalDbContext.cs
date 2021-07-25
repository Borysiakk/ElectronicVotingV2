using ElectronicVoting.API.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.API
{
    public class ApplicationLocalDbContext :DbContext
    {
        public DbSet<ValidatorEntities> Validators { get; set; }
        public DbSet<TransactionEntities> TransactionsHistory { get; set; }

        public ApplicationLocalDbContext(DbContextOptions<ApplicationLocalDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            base.OnModelCreating(modelBuider);
        }
    }
}