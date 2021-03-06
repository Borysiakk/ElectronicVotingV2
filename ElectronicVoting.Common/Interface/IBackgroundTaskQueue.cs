using System;
using System.Threading;
using System.Threading.Tasks;

namespace ElectronicVoting.Common.Interface
{
    public interface IBackgroundTaskQueue
    {
        public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem);
        public Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}