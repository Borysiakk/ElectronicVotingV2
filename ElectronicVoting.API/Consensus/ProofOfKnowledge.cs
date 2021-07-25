using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicVoting.API.Consensus
{
    public static class ProofOfKnowledge
    {
        public static async Task<bool> Validation(MessageTransaction messageTransaction,CancellationToken token)
        {
            await Task.Delay(TimeSpan.FromSeconds(5),token);
            
            return true;
        }
    }
}