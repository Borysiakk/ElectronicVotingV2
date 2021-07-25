using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ElectronicVoting.API.Http
{
    public class HttpHelper :Singleton<HttpHelper>,IHttpHelper
    {
        public async Task<TA> PostAsync<TA>(string serverUrl, string relativeUrl, IAuthenticator authenticatorBase)
        {
            RestClient client = new RestClient(serverUrl);
            RestRequest request = new RestRequest(relativeUrl);

            if(authenticatorBase != null)
                client.Authenticator = authenticatorBase;

            var response = await client.ExecuteAsync<TA>(request);
            return JsonConvert.DeserializeObject<TA>(response.Content);
        }

        public async Task<IRestResponse> PostAsync<TA>(string serverUrl, string relativeUrl, IAuthenticator authenticatorBase, TA model)
        {
            RestClient client = new RestClient(serverUrl);
            RestRequest request = new RestRequest(relativeUrl,Method.POST);

            if(authenticatorBase != null)
                client.Authenticator = authenticatorBase;
            
            request.AddJsonBody(model);
            return await client.ExecuteAsync(request);
        }
    }
}