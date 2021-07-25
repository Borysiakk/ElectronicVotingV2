using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicVoting.API.Model.Entities;

namespace ElectronicVoting.API.Interface
{
    public interface IValidatorService
    {
        public Task AddAsync(ValidatorEntities validator);
        public List<ValidatorEntities> Gets();
    }
}