using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicVoting.API.Interface;
using ElectronicVoting.API.Model.Entities;

namespace ElectronicVoting.API.Services
{
    public class TransactionService :ITransactionService
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public TransactionService(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public async Task<TransactionEntities> AddAsync(TransactionEntities entities)
        {
            var transactionEntities = await _localDbContext.Transactions.AddAsync(entities);
            await _localDbContext.SaveChangesAsync();

            return transactionEntities.Entity;
        }

        public async Task<TransactionEntities> UpdateAsync(TransactionEntities entities)
        {
            var transactionEntities = _localDbContext.Transactions.Update(entities);
            await _localDbContext.SaveChangesAsync();

            return transactionEntities.Entity;
        }

        public IEnumerable<TransactionEntities> Find(string id)
        {
            return _localDbContext.Transactions.Where(a => a.Id == id);
        }
        
        
    }
}