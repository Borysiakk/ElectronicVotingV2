using System;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Common.Model.Entities;
using ElectronicVoting.Infrastructure.Helper;
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

        [HttpGet("TESTGet")]
        public async Task<IActionResult> TestGet()
        {
            try
            {
                var list = _repositoryEntities.GetAllAsync();
                Console.WriteLine("TestGet");
                var url = "https://172.20.96.1:5001";
                ValidatorEntities validatorEntities = new ValidatorEntities()
                {
                    Name = "TEST",
                    Address = "https://127.0.0.0",
                    Port = "5001",
                };
                var result = await HttpHelper.Instance.PostAsync<ValidatorEntities>(url, "/TestAdd", null, validatorEntities);
                Console.WriteLine(result.ErrorMessage);
                Console.WriteLine(result.StatusCode);
                return Ok(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("TestAdd")]
        public async Task<IActionResult> TestAdd(ValidatorEntities entities)
        {
            Console.WriteLine("TESTADD");
            await _repositoryEntities.AddAsync(entities);
            return Ok(entities);
        }
    }
}