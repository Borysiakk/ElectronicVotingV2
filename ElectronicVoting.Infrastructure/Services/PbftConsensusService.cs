using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Common.Model.Entities;
using ElectronicVoting.Infrastructure.Helper;
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


        public async Task PrePreparingAsync(HttpContextInformation httpContextInformation, MessageVote messageVote, CancellationToken token)
        {
            Console.WriteLine("PrePreparing");
            var port = httpContextInformation.Port;
            
            MessageTransaction messageTransaction = new MessageTransaction()
            {
                Id = Guid.NewGuid().ToString(),
                Transaction = new Transaction()
                {
                    From = port.ToString(),
                    Vote = messageVote.Vote,
                    Id = Guid.NewGuid().ToString(),
                },
            };
            
            
            foreach (var validator in  _repositoryValidator.GetAllAsync())
            {
                if (validator.Port != port)
                {
                    var url = validator.Address + ":" + validator.Port;
                    var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.Preparing, null, messageTransaction);
                }
            }
        }

        public async Task PreparingAsync(HttpContextInformation httpContextInformation, MessageTransaction messageTransaction,CancellationToken token)
        {
            var port = httpContextInformation.Port;
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);
            var validators =  _repositoryValidator.GetAllAsync();
            
            TransactionEntities transactionEntities = new TransactionEntities()
            {
                
                SuperValidator = true,
                Id = Guid.NewGuid().ToString(),
                From = messageTransaction.Transaction.From,
                TransactionId = messageTransaction.Transaction.Id,
            };
            await _repositoryTransaction.AddAsync(transactionEntities);
            
            if (resultValidation)
            {
                MessageVerificationVote messageVerificationVote = new MessageVerificationVote()
                {
                    
                    IsVerificationVote = true,
                    Id = Guid.NewGuid().ToString(),
                    TransactionId = messageTransaction.Transaction.Id,
                };
                

                foreach (var validator in validators)
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

        public async Task CommitAsync(HttpContextInformation httpContextInformation, MessageVerificationVote messageVerificationVote, CancellationToken token)
        {
            var countVerificationServer = (await _repositoryElectionSettings.FindAsync("0")).VerificationServerCount;
            TransactionEntities transactionEntities = new TransactionEntities()
            {
                Id = new Guid().ToString(),
                From = messageVerificationVote.From,
                TransactionId = messageVerificationVote.TransactionId,
            };
            
            await _repositoryTransaction.AddAsync(transactionEntities);
            var transactionsCount = (await _repositoryTransaction.WhereAsync(a => a.TransactionId == transactionEntities.TransactionId)).Count();
            int expression = (countVerificationServer * 2) / 3;

            if (expression >= transactionsCount)
            {
                //var result = await HttpHelper.Instance.PostAsync<MessageVerificationVote>(url, Routes.PbftConsensusRoutesApi.Commit, null, messageVerificationVote);
            }

            Console.WriteLine("CommitAsync");
        }
    }
}