using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace Waluty.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        public HttpClient Client { get; }

        public ExchangeRatesService(HttpClient client )
        {
            client.BaseAddress = new Uri("http://api.nbp.pl");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "text/xml");
            client.DefaultRequestHeaders.Add("User-Agent", "api");
            Client = client;

        }
        public string Rates { get; set; }

        public async Task<string> GetRates()
        {
            var response = await Client.GetAsync(
                "/api/exchangerates/rates/a/chf/");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}
