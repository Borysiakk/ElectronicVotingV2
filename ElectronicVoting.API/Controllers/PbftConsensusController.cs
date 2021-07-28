
using System.Threading.Tasks;
using ElectronicVoting.Common.Interface;
using ElectronicVoting.Common.Model.Blockchain;
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
        private readonly IPbftConsensusService _pbftConsensusService;

        public PbftConsensusController(IPbftConsensusService pbftConsensusService, IBackgroundTaskQueue queue, IServiceScopeFactory serviceScopeFactory)
        {
            _queue = queue;
            _serviceScopeFactory = serviceScopeFactory;
            _pbftConsensusService = pbftConsensusService;
        }

        [HttpPost("PrePreparing")]
        public async Task<IActionResult> PrePreparing(MessageVote messageVote)
        {
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    await _pbftConsensusService.PrePreparingAsync(HttpContext,messageVote,token);
                }
            });
            
            return Ok();
        }
        
        [HttpPost("Preparing")]
        public async Task<IActionResult> Preparing(MessageTransaction messageTransaction)
        {
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    await _pbftConsensusService.PreparingAsync(HttpContext,messageTransaction,token);   
                }
            });
            
            return Ok("Preparing");
        }
        
        [HttpPost("Commit")]
        public async Task<IActionResult> Commit(MessageVerificationVote messageVerificationVote)
        {
            _queue.QueueBackgroundWorkItem(async token =>
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    await _pbftConsensusService.CommitAsync(HttpContext,messageVerificationVote,token);   
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