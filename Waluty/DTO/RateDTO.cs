using System;
using System.Xml.Serialization;

namespace Waluty.DTO
{
    [XmlRoot("Rate")]
    public class RateDTO
    {
        [XmlElement(ElementName = "No")]
        public string Number { get; set; }

        [XmlElement(ElementName = "EffectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [XmlElement(ElementName = "Mid")]
        public decimal MidPrice { get; set; }
    }
}