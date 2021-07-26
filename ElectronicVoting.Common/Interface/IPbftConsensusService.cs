﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ElectronicVoting.Common.Model.Blockchain;

namespace ElectronicVoting.Common.Interface
{
    public interface IPbftConsensusService
    {
        public Task PrePreparingAsync(HttpContext httpContext,MessageTransaction messageTransaction,CancellationToken token);
        public Task PreparingAsync(HttpContext httpContext,MessageTransaction messageTransaction,CancellationToken token);
    }
}