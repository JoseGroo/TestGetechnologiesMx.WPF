using Newtonsoft.Json;
using RestSharp;

namespace TestGetechnologiesMx.WPF.Helpers
{
    public class ApiClient
    {
        private RestClient _restClient;

        public ApiClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

       
    }
}
