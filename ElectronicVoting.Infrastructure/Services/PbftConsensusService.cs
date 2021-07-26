using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Common.Model.Entities;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Persistence;
using Microsoft.AspNetCore.Http;

namespace ElectronicVoting.Infrastructure.Services
{
    public class PbftConsensusService :IPbftConsensusService
    {
        private readonly IRepositoryEntities<ValidatorEntities> _repositoryValidator;
        private readonly IRepositoryEntities<InitialTransactionEntities> _repositoryInitialTransaction;
        public PbftConsensusService( IRepositoryEntities<InitialTransactionEntities> repositoryInitialTransaction,ApplicationLocalDbContext localDbContext, IRepositoryEntities<ValidatorEntities> repositoryValidator)
        {
            _repositoryInitialTransaction = repositoryInitialTransaction;
            _repositoryValidator = repositoryValidator;
        }

        public async Task PrePreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            Console.WriteLine("PrePreparing");
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);
            
            if (resultValidation)
            {
                foreach (var validator in _repositoryValidator.GetAll())
                {
                    if (validator.Port != port)
                    {
                        var url = validator.Address + ":" + validator.Port;
                        var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.Preparing, null, messageTransaction);
                    }
                }
            }
        }

        public async Task PreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);
            var isInitialTransaction = await _repositoryInitialTransaction.FindAsync(messageTransaction.Id);

            if (isInitialTransaction != null)
            {
                
            }
            else
            {
                InitialTransactionEntities initialTransaction = new InitialTransactionEntities()
                {
                    Id = messageTransaction.Transaction.Id,
                    Validator = messageTransaction.Transaction.From,
                };
                await _repositoryInitialTransaction.AddAsync(initialTransaction);
            }

            Console.WriteLine("Preparing");
        }
    }
}