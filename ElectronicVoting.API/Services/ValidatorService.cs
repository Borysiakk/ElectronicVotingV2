using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectronicVoting.API.Interface;
using ElectronicVoting.API.Model.Entities;

namespace ElectronicVoting.API.Services
{
    public class ValidatorService :IValidatorService
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public ValidatorService(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public async Task AddAsync(ValidatorEntities validator)
        {
            try
            {
                await _localDbContext.Validators.AddAsync(validator);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ValidatorEntities> Gets()
        {
            return _localDbContext.Validators.ToList();
        }
    }
}