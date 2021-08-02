
using System.Threading.Tasks;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class PbftConsensusController :ControllerBase
    {
        private readonly IBackgroundTaskQueue _queue;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PbftConsensusController(IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost("PrePreparing")]
        public async Task<IActionResult> PrePreparing(MessageVote messageVote)
        {
            HttpContextInformation httpContextInformation = new HttpContextInformation()
            {
                Port = HttpContext.Connection.LocalPort.ToString(),
            };
            
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _pbftConsensusService = scope.ServiceProvider.GetService<IPbftConsensusService>();
                    await _pbftConsensusService.PrePreparingAsync(httpContextInformation,messageVote,token);   
                }
            });
            
            return Ok();
        }
        
        [HttpPost("Preparing")]
        public async Task<IActionResult> Preparing(MessageTransaction messageTransaction)
        {
            HttpContextInformation httpContextInformation = new HttpContextInformation()
            {
                Port = HttpContext.Connection.LocalPort.ToString(),
            };
            
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _pbftConsensusService = scope.ServiceProvider.GetService<IPbftConsensusService>();
                    await _pbftConsensusService.PreparingAsync(httpContextInformation,messageTransaction,token);   
                }
            });
            
            return Ok("Preparing");
        }
        
        [HttpPost("Commit")]
        public async Task<IActionResult> Commit(MessageVerificationVote messageVerificationVote)
        {
            HttpContextInformation httpContextInformation = new HttpContextInformation()
            {
                Port = HttpContext.Connection.LocalPort.ToString(),
            };
            
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _pbftConsensusService = scope.ServiceProvider.GetService<IPbftConsensusService>();
                    await _pbftConsensusService.CommitAsync(httpContextInformation,messageVerificationVote,token);   
                }
            });
            return Ok("Commit");
        }
        
        [HttpPost("Reply")]
        public async Task<IActionResult> Reply()
        {
            return Ok("Reply");
        }
    }
}