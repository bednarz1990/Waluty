using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Waluty.Models
{
    public class RatesPaged
    {
        public PagedCollectionResponse<Rate> RatePagedCollectionResponse { get; set; }

        public decimal AveragePrice { get; set; }
    }
}
