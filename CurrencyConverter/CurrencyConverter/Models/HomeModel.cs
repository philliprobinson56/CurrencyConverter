using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyConverter.Models
{
    public class HomeModel
    {
        public Dictionary<string, double> Currencies { get; set; }
        public ConverterFormModel ConveterForm { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public IEnumerable<SearchInfo> SearchInfos { get; set; }
    }
}