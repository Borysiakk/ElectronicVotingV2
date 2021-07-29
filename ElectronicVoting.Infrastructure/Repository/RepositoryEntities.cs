
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using ElectronicVoting.Persistence;
using ElectronicVoting.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Infrastructure.Repository
{
    public  class RepositoryEntities<TA> :IRepositoryEntities<TA> where TA : class
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public RepositoryEntities(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public async Task<TA> AddAsync(TA entities)
        {
            var item = _localDbContext.Set<TA>().Add(entities);
            await _localDbContext.SaveChangesAsync();
            return item.Entity;
        }

        public async Task<TA> FindAsync(string key)
        {
            var item = await _localDbContext.Set<TA>().FindAsync(key);
            
            return item;
        }

        public async Task<TA> UpdateAsync(TA entities,string key)
        {
            if (entities == null) return null;
            TA existing = await _localDbContext.Set<TA>().FindAsync(key);
            
            if (existing != null)
            {
                _localDbContext.Entry(existing).CurrentValues.SetValues(entities);
                await _localDbContext.SaveChangesAsync();
            }
            
            return existing;
        }

        public async Task<TA> DeleteAsync(TA entities)
        {
            if (entities == null) return null;
            TA existing = await _localDbContext.Set<TA>().FindAsync(entities);

            if (existing != null)
            {
                _localDbContext.Set<TA>().Remove(entities);
            }

            return entities;
        }

        public IEnumerable<TA> GetAllAsync()
        {
            return  _localDbContext.Set<TA>().ToList();
        }

        public async Task<IEnumerable<TA>> WhereAsync(Expression<Func<TA, bool>> expression)
        {
            return await _localDbContext.Set<TA>().Where(expression).ToListAsync();
        }
    }
} 