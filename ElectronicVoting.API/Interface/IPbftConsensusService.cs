using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.API.Consensus;
using Microsoft.AspNetCore.Http;

namespace ElectronicVoting.API.Interface
{
    public interface IPbftConsensusService
    {
        public Task PrePreparingAsync(HttpContext httpContext,MessageTransaction messageTransaction,CancellationToken token);
        public Task PreparingAsync(HttpContext httpContext,MessageTransaction messageTransaction,CancellationToken token);
    }
}