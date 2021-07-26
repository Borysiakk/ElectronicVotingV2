using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Persistence;
using Microsoft.AspNetCore.Http;

namespace ElectronicVoting.Infrastructure.Services
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
                        var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.Preparing, null, messageTransaction);
                    }
                }   
            }
        }

        public async Task PreparingAsync(HttpContext httpContext, MessageTransaction messageTransaction,CancellationToken token)
        {
            var port = httpContext.Connection.LocalPort.ToString();
            var resultValidation = await ProofOfKnowledge.Validation(messageTransaction,token);
            

            Console.WriteLine("Preparing");
        }
    }
}