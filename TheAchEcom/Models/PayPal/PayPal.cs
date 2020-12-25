using System;
using System.Collections.Generic;


namespace TheAchEcom.Models.PayPal
{
    public class Amount
    {
        public string currency_code { get; set; } = "USD";
        public double value { get; set; }
    }

    public class PurchaseUnit
    {
        public Amount amount { get; set; }
    }
}
