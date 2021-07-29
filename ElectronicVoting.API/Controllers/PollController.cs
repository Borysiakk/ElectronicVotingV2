using System;
using System.Threading.Tasks;
using ElectronicVoting.Common;
using ElectronicVoting.Common.Interface.Services;
using ElectronicVoting.Common.Model.Blockchain;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Infrastructure.Services;
using ElectronicVoting.Persistence;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ElectronicVoting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class PollController: ControllerBase
    {
        private readonly IPollService _pollService;
        public PollController(ApplicationLocalDbContext localDbContext, IPollService pollService)
        {
            _pollService = pollService;
        }


        [HttpPost("Voice")]
        public async Task<IActionResult> Voice(string value)
        {
            await _pollService.Vote(value);
            return Ok();
        }
    }
}