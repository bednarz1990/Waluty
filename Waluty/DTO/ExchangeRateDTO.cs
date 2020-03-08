using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Waluty.Models;

namespace Waluty.DTO
{
    [XmlRoot("ExchangeRatesSeries")]
    public class ExchangeRateDTO
    {
        public string Table { get; set; }

        public string Currency { get; set; }
        public string Code { get; set; }

        [XmlElement("Rates")]
        public RateDTO[] Rates { get; set; }
    }
}
