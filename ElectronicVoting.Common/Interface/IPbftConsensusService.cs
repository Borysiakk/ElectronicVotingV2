using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Common.Model;
using Microsoft.AspNetCore.Http;
using ElectronicVoting.Common.Model.Blockchain;

namespace ElectronicVoting.Common.Interface
{
    public interface IPbftConsensusService
    {
        public Task PrePreparingAsync(HttpContextInformation httpContextInformation,MessageVote messageVote,CancellationToken token);
        public Task PreparingAsync(HttpContextInformation httpContextInformation,MessageTransaction messageTransaction,CancellationToken token);
        public Task CommitAsync(HttpContextInformation httpContextInformation, MessageVerificationVote messageVerificationVote, CancellationToken token);
    }
}