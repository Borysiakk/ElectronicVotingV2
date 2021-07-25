using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicVoting.API.Model.Entities;

namespace ElectronicVoting.API.Interface
{
    public interface ITransactionService
    {
        public Task<TransactionEntities> AddAsync(TransactionEntities entities);
        public Task<TransactionEntities> UpdateAsync(TransactionEntities entities);
        public IEnumerable<TransactionEntities> Find(string id);
    }
}