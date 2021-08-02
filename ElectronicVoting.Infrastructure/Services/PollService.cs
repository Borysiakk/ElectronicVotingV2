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

        public async Task Vote(string vote)
        {
            MessageVote messageVote = new MessageVote()
            {
                Vote = vote,
                Id = Guid.NewGuid().ToString()
            };

            var url = "https://localhost:4005";
            var result = await HttpHelper.Instance.PostAsync<MessageVote>(url, Routes.PbftConsensusRoutesApi.PrePreparing, null, messageVote);
        }
    }
}