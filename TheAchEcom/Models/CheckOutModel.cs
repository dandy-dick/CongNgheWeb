using Repository.DomainModels;
using System;
using TheAchEcom.Models.PayPal;

namespace TheAchEcom.Models
{
    public enum CheckOutState
    {
        OrderDetail = 1,
        Payment = 2,
        Complete = 3
    }

    public class CheckOutModel
    {
        public CheckOutState State { get; set; }
        public string PayPalClientId = "Aa7X9GhKYiNTtNJRaN6g57eykkXRAXeoRroEqX36TFe2AJOEvRf7f60o-CnG2xbgyzE_MC-onPiR1TjC";
        public ShoppingCart Cart { get; set; }
        public int TotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public OrderDetailModel OrderDetail { get; set; } = new OrderDetailModel();


        public string IsActiveState(CheckOutState state)
        {
            switch (this.State)
            {
                case CheckOutState.OrderDetail:
                    if (state == CheckOutState.OrderDetail)
                        return "active";
                    break;
                
                case CheckOutState.Payment:
                    if (state == CheckOutState.OrderDetail || state == CheckOutState.Payment)
                        return "active";
                    break;
            
                case CheckOutState.Complete:
                    return "active";
            }

            return "";
        }
    }
}
