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
        private readonly IRepositoryEntities<TransactionEntities> _repositoryTransaction;
        private readonly IRepositoryEntities<ElectionSettingsEntities> _repositoryElectionSettings;

        public PbftConsensusService(IRepositoryEntities<ValidatorEntities> repositoryValidator, IRepositoryEntities<TransactionEntities> repositoryTransaction, IRepositoryEntities<ElectionSettingsEntities> repositoryElectionSettings)
        {
            _repositoryValidator = repositoryValidator;
            _repositoryTransaction = repositoryTransaction;
            _repositoryElectionSettings = repositoryElectionSettings;
        }

        public async Task PrePreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            Console.WriteLine("PrePreparing");
            var port = httpContext.Connection.LocalPort.ToString();
            foreach (var validator in _repositoryValidator.GetAll())
            {
                if (validator.Port != port)
                {
                    var url = validator.Address + ":" + validator.Port;
                    var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.Preparing, null, messageTransaction);
                }
            }
        }

        public async Task PreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);

            if (resultValidation)
            {
                MessageVerificationVote messageVerificationVote = new MessageVerificationVote()
                {
                    IsVerificationVote = true,
                    Id = Guid.NewGuid().ToString(),
                    TransactionId = messageTransaction.Transaction.Id,
                };

                foreach (var validator in _repositoryValidator.GetAll())
                {
                    if (port != validator.Port)
                    {
                        var url = validator.Address + ":" + validator.Port;
                        var result = await HttpHelper.Instance.PostAsync<MessageVerificationVote>(url, Routes.PbftConsensusRoutesApi.Commit, null, messageVerificationVote);
                    }
                }
            }
            Console.WriteLine("Preparing");
        }

        public async Task CommitAsync(HttpContext httpContext, MessageVerificationVote messageVerificationVote, CancellationToken token)
        {
            var countVerificationServer = (await _repositoryElectionSettings.FindAsync("1")).VerificationServerCount;
            TransactionEntities transactionEntities = new TransactionEntities()
            {
                Id = new Guid().ToString(),
                From = messageVerificationVote.From,
                TransactionId = messageVerificationVote.TransactionId,
            };
            await _repositoryTransaction.AddAsync(transactionEntities);
            
            
            Console.WriteLine("CommitAsync");
        }
    }
}