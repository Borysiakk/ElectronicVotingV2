using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ElectronicVoting.Common.Interface.Services
{
    public interface IPollService
    {
        public Task Vote(string vote);
    }
}