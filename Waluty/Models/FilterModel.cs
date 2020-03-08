using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Waluty.Models
{
    public abstract class FilterModelBase : ICloneable
    {
       
        public int Page { get; set; }
        public int Limit { get; set; }

        public FilterModelBase()
        {
            this.Page = 1;
            this.Limit = 1;
        }
        public abstract object Clone();
    }

    public class ExchangeFilterModel : FilterModelBase
    {
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        public ExchangeFilterModel() : base()
        {
            this.MinDate = DateTime.Now;
            this.Limit = 100;
        }


        public override object Clone()
        {
            var jsonString = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject(jsonString, this.GetType());
        }
    }

    public class PagedCollectionResponse<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
    }
}
