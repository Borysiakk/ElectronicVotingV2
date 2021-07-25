using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.API.Consensus;
using ElectronicVoting.API.Http;
using ElectronicVoting.API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.API.Services
{
    public class PbftConsensusService :IPbftConsensusService
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public PbftConsensusService(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public async Task PrePreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            Console.WriteLine("PrePreparing");
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);
            
            if (resultValidation)
            {
                foreach (var validator in _localDbContext.Validators)
                {
                    if (validator.Port != port)
                    {
                        var url = validator.Address + ":" + validator.Port;
                        var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.ValidatorApi.Preparing, null, messageTransaction);
                    }
                }   
            }
        }

        public async Task PreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);

            var transactionEntitiesEnumerable = await _localDbContext.TransactionsHistory.FirstOrDefaultAsync(a => a.Id == messageTransaction.Transaction.Id, cancellationToken: token);
            if (transactionEntitiesEnumerable != null)
            {
                
            }
            else
            {
                
            }

            Console.WriteLine("Preparing");
        }
    }
}