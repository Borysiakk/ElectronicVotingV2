using System;
using System.Threading.Tasks;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.API.Controllers
{
    public class TestController :ControllerBase
    {
        private readonly IRepositoryEntities<InitialTransactionEntities> _repositoryInitialTransaction;

        public TestController(IRepositoryEntities<InitialTransactionEntities> repositoryInitialTransaction)
        {
            _repositoryInitialTransaction = repositoryInitialTransaction;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add()
        {
            InitialTransactionEntities initialTransaction = new InitialTransactionEntities()
            {
                Validator = "TEST",
                Id = "2",
            };

            await _repositoryInitialTransaction.AddAsync(initialTransaction);
            return Ok();
        }
        
        [HttpPost("Update")]
        public async Task<IActionResult> Update()
        {
            InitialTransactionEntities initialTransaction = new InitialTransactionEntities()
            {
                Validator = "TEST UPDATE",
                Id = "2",
            };

            await _repositoryInitialTransaction.UpdateAsync(initialTransaction, initialTransaction.Id);
            return Ok();
        }
    }
}