using ElectronicVoting.Common.Interface;
using ElectronicVoting.Infrastructure.Background;
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
            
            return services;
        }
    }
}