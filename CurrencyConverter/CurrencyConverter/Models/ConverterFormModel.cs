namespace CurrencyConverter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class ConverterFormModel
    {
        public string CurrencyRatio { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double GbrAmount { get; set; }

        public string ConvertedAmount { get; set; }
    }
}