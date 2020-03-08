using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Waluty.Models
{
    public class Rate
    {
        public int RateId { get; set; }
        public string Number { get; set; }

        public DateTime EffectiveDate { get; set; }

        [Column(TypeName = "decimal(18, 4)")]

        public decimal MidPrice { get; set; }
     }
}