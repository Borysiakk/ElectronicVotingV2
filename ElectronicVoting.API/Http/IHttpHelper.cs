using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace ElectronicVoting.API.Http
{
    public interface IHttpHelper
    {
        public Task<IRestResponse> PostAsync<TA>(string serverUrl, string relativeUrl, IAuthenticator authenticatorBase, TA model);
    }
}