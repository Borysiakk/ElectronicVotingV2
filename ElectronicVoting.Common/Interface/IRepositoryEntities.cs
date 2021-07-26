using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElectronicVoting.Common.Interface
{
    public interface IRepositoryEntities<TA>
    {
        public Task<TA> AddAsync(TA entities);
        public Task<TA> FindAsync(string key);
        public Task<TA> UpdateAsync(TA entities,string key);
        public Task<TA> DeleteAsync(TA entities);

        public IEnumerable<TA> GetAll();
    }
}