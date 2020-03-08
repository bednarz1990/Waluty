using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Waluty.Data;
using Waluty.Models;

namespace Waluty
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly Waluty.Data.WalutyContext _context;

        public bool GetRatesError { get; private set; }


        public IndexModel(Waluty.Data.WalutyContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;
        }

        public IList<ExchangeRate> ExchangeRate { get; set; }

        public string Rates { get; set; }

        public async Task OnGetAsync()
        {
            ExchangeRate = await _context.ExchangeRate.ToListAsync();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://api.nbp.pl/api/exchangerates/rates/a/chf/");
            request.Headers.Add("Accept", "text/xml");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(responseStream); 
                Rates = JsonConvert.SerializeXmlNode(doc);
            }
            else
            {
                GetRatesError = true;
                Rates = default;
            }

        }
    }
}
