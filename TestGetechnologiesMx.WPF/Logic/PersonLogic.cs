using Newtonsoft.Json;
using RestSharp;
using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.WPF.Helpers;

namespace TestGetechnologiesMx.WPF.Logic
{
    public class PersonLogic
    {
        private RestClient _restClient;
        public string LastError { get; set; }

        public PersonLogic()
        {
            _restClient = new RestClient(Helper.BaseUrlApi);
        }

        public List<Person> GetList()
        {
            try
            {
                var request = new RestRequest("Person/GetPeople", Method.Get);
                RestResponse response = _restClient.Execute(request);
                
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<Person>>(response.Content);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public bool Save(Person person)
        {
            LastError = null;
            try
            {
                var request = new RestRequest("Person/Save", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(person);
                RestResponse response = _restClient.Execute(request);

                if (response.IsSuccessful)
                {
                    return true;
                }
                LastError = response.Content;
                return false;
            }
            catch (Exception ex)
            {
            }
            LastError = "Error inesperado";
            return false;
        }

        public bool Delete(int personId)
        {
            LastError = null;
            try
            {
                var request = new RestRequest("Person/Delete", Method.Post);
                request.AddParameter("personId", personId, ParameterType.QueryString);
                RestResponse response = _restClient.Execute(request);

                if (response.IsSuccessful)
                {
                    return true;
                }
                LastError = response.Content;
                return false;
            }
            catch (Exception ex)
            {
            }
            LastError = "Error inesperado";
            return false;
        }
    }
}
