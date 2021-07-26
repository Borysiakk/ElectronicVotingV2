using System;
using System.Threading;
using System.Threading.Tasks;
using ElectronicVoting.Common.Model.Blockchain;

namespace ElectronicVoting.Infrastructure
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