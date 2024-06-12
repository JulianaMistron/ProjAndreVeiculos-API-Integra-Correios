using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CorreiosService : ICorreiosService
    {
        private static HttpClient sharedClient;
        public CorreiosService()
        {
            sharedClient = new HttpClient
            {
                BaseAddress = new Uri("https://viacep.com.br")
            };
        }

        public async Task<CorreiosResponse> ObterCepAsync(string cep)
        {
            var response = await sharedClient.GetFromJsonAsync<CorreiosResponse>($"/ws/{cep}/json/");
            return response;
        }

    }
}
