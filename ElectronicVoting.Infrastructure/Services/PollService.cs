using System;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface.Services;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Persistence;
using Microsoft.AspNetCore.Http;

namespace ElectronicVoting.Infrastructure.Services
{
    public class PollService :IPollService
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public PollService(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }

        public async Task Vote(HttpContext context,string vote)
        {
            var port = context.Connection.LocalPort;
            MessageTransaction voice = new MessageTransaction()
            {
                Id = Guid.NewGuid().ToString(),
                Transaction = new Transaction()
                {
                    Vote = vote,
                    From = port.ToString(),
                    Id = Guid.NewGuid().ToString(),
                },
            };
                
            foreach (var validator in _localDbContext.Validators)
            {
                if (validator.Port != port.ToString())
                {
                    var url = validator.Address + ":" + validator.Port;
                    var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.PrePreparing, null, voice);
                }
            }
        }
    }
}