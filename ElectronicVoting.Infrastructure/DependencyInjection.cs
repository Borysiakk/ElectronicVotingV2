﻿using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Entities;
using ElectronicVoting.Infrastructure.Background;
using ElectronicVoting.Infrastructure.Hosted;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHostedService<QueuedHostedService>();  
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddScoped<IPbftConsensusService,PbftConsensusService>();
            
            services.AddScoped<IRepositoryEntities<ValidatorEntities>,RepositoryEntities<ValidatorEntities>>();
            services.AddScoped<IRepositoryEntities<InitialTransactionEntities>,RepositoryEntities<InitialTransactionEntities>>();
            
            return services;
        }
    }
}