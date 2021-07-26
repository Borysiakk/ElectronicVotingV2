using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ElectronicVoting.Common.Interface;

namespace ElectronicVoting.Infrastructure.Hosted
{
    public class QueuedHostedService :BackgroundService  
    {
        private readonly ILogger _logger;  
        private IBackgroundTaskQueue TaskQueue { get; }  
        
        public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILoggerFactory loggerFactory)  
        {  
            TaskQueue = taskQueue;  
            _logger = loggerFactory.CreateLogger<QueuedHostedService>();  
        }  
        
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)  
        {  
            _logger.LogInformation("Queued Hosted Service is starting.");  
  
            while (!cancellationToken.IsCancellationRequested)  
            {  
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);  
  
                try  
                {  
                    await workItem(cancellationToken);  
                }  
                catch (Exception ex)  
                {  
                    _logger.LogError(ex, "Error occurred executing {WorkItem}.", nameof(workItem));  
                }  
            }  
  
            _logger.LogInformation("Queued Hosted Service is stopping.");  
        }  
    }
}