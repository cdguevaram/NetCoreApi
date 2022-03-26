using core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace core.Business.Helpers
{
   public class ExternalData
   {
        HttpClient clientHttp;
        string _urlBase;
        public ExternalData(string urlBase)
        {
            this.clientHttp = new HttpClient();
            this._urlBase = urlBase;
        }
        public async Task<List<Books>> GetAllBooks()
        {
            HttpResponseMessage response = await this.clientHttp.GetAsync(this._urlBase + "api/v1/Books");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                var rt = JsonConvert.DeserializeObject<List<Books>>(resp);
                return rt;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task<List<Authors>> GetAllAuthors()
        {
            HttpResponseMessage response = await this.clientHttp.GetAsync(this._urlBase + "/api/v1/Authors");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                var rt = JsonConvert.DeserializeObject<List<Authors>>(resp);
                return rt;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
        public async Task<List<UserModel>> GetAllUsers()
        {
            HttpResponseMessage response = await this.clientHttp.GetAsync(this._urlBase + "/api/v1/Users");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                var rt = JsonConvert.DeserializeObject<List<UserModel>>(resp);
                return rt;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
