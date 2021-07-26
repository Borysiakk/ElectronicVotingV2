using System;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Persistence;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ElectronicVoting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class PollController: ControllerBase
    {
        private readonly ApplicationLocalDbContext _localDbContext;

        public PollController(ApplicationLocalDbContext localDbContext)
        {
            _localDbContext = localDbContext;
        }


        [HttpPost("Voice")]
        public async Task<IActionResult> Voice(string value)
        {
            try
            {
                var port = HttpContext.Connection.LocalPort;
                MessageTransaction voice = new MessageTransaction()
                {
                    Id = Guid.NewGuid().ToString(),
                    Transaction = new Transaction()
                    {
                        Voice = value,
                        From = port.ToString(),
                        Id = Guid.NewGuid().ToString(),
                    },
                };
                
                foreach (var validator in _localDbContext.Validators)
                {
                    if (validator.Port != port.ToString())
                    {
                        var url = validator.Address + ":" + validator.Port;
                        var result = await HttpHelper.Instance.PostAsync<MessageTransaction>(url, Routes.PbftConsensusRoutesApi.PrePreparing, null, voice);
                    }
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}