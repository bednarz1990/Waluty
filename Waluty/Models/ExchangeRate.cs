using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Waluty.Models
{
    public class ExchangeRate
    {
        public int ExchangeRateId { get; set; }
        public string Table { get; set; }

        public string Currency { get; set; }

        [DataType(DataType.Currency)]
        public string Code { get; set; }

        [IgnoreDataMember]
        public ICollection<Rate> Rates { get; set; }
    }
}
