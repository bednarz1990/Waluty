using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Waluty.DTO;
using Waluty.Models;

namespace Waluty.Services
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        public HttpClient Client { get; }
        private readonly Waluty.Data.WalutyContext _context;


        public ExchangeRatesService(HttpClient client, Waluty.Data.WalutyContext context)
        {
            client.BaseAddress = new Uri("http://api.nbp.pl");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "text/xml");
            client.DefaultRequestHeaders.Add("User-Agent", "api");
            Client = client;
            _context = context;
        }
        public string Rates { get; set; }

        public async Task SaveRate()
        {
            var response = await Client.GetAsync(
                "/api/exchangerates/rates/a/chf/");

            response.EnsureSuccessStatusCode();

            var exchangeRate = await response.Content.ReadAsStringAsync();

            XmlSerializer serializer = new XmlSerializer(typeof(ExchangeRatesSeries), new XmlRootAttribute("ExchangeRatesSeries"));
            StringReader stringReader = new StringReader(exchangeRate);
            ExchangeRatesSeries exchangeRatesSeries = (ExchangeRatesSeries)serializer.Deserialize(stringReader);
            _context.ExchangeRate.Add(new ExchangeRate() { Code = exchangeRatesSeries.Code, Table = exchangeRatesSeries.Table, Currency = exchangeRatesSeries.Currency, Rates = new List<Rate>() { ToExchangeRateMap(exchangeRatesSeries.Rates) } });

            await _context.SaveChangesAsync();
        }

        private static Rate ToExchangeRateMap(ExchangeRatesSeriesRates exchangeRatesSeriesRates)
        {
            return new Rate()
            {
                EffectiveDate = exchangeRatesSeriesRates.Rate.EffectiveDate,
                MidPrice = (decimal)exchangeRatesSeriesRates.Rate.Mid,
                Number = exchangeRatesSeriesRates.Rate.No

            };
        }

    }
}
