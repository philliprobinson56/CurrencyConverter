using System;

namespace CurrencyConverter.Models
{
    public class SearchInfo
    {
        public int Id { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public DateTime SearchedOn { get; set; }
    }
}