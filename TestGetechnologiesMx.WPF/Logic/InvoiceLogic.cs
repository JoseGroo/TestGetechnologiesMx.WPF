using Newtonsoft.Json;
using RestSharp;
using TestGetechnologiesMx.Repository.Entities;
using TestGetechnologiesMx.WPF.Helpers;
using TestGetechnologiesMx.WPF.Models;

namespace TestGetechnologiesMx.WPF.Logic
{
    public class InvoiceLogic
    {
        private RestClient _restClient;
        public string LastError { get; set; }

        public InvoiceLogic()
        {
            _restClient = new RestClient(Helper.BaseUrlApi);
        }

        public List<Invoice> GetList(int personId)
        {
            try
            {
                var request = new RestRequest("Invoice/Get", Method.Get);
                request.AddParameter("personId", personId, ParameterType.QueryString);
                RestResponse response = _restClient.Execute(request);
                
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<List<Invoice>>(response.Content);
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public bool Save(InvoiceModel invoice)
        {
            LastError = null;
            try
            {
                var request = new RestRequest("Invoice/Save", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(invoice);
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

        public bool Delete(int invoiceId)
        {
            LastError = null;
            try
            {
                var request = new RestRequest("Invoice/Delete", Method.Post);
                request.AddParameter("invoiceId", invoiceId, ParameterType.QueryString);
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
