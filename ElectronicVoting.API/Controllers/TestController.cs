using System.Threading.Tasks;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.API.Controllers
{
    public class TestController : ControllerBase
    {
        private readonly IRepositoryEntities<ValidatorEntities> _repositoryEntities;

        public TestController(IRepositoryEntities<ValidatorEntities> repositoryEntities)
        {
            _repositoryEntities = repositoryEntities;
        }

        [HttpPost("TESTGet")]
        public async Task<IActionResult> TestGet()
        {
            var list = _repositoryEntities.GetAllAsync();
            return Ok();
        }
    }
}