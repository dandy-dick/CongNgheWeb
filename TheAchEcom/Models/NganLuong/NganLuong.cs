using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TheAchEcom.Models.NganLuong
{
    public class NlOrderDetail
    {
        public string merchant_site_code { get; } = "49732";
        public string return_url { get; set; }
        public string receiver { get; } = "nvhuu.business@gmail.com";
        public string transaction_info { get; set; } = "TheACHsOrder";
        public string order_code { get; set; }
        public int price { get; set; }
        public string currency { get; } = "usd";
        public int quantity { get; set; }
        public int tax { get; } = 0;
        public int discount { get; } = 0;
        public int fee_cal { get; } = 0;
        public int fee_shipping { get; } = 0;
        public string order_description { get; set; } = "Pleasework";
        public string buyer_info { get; set; } = "";
        public string affiliate_code { get;} = "";
        public string lang { get; } = "vi";
        public string cancel_url { get; set; }

        public string secure_code { get; set; }


        public void SetSecureCode()
        {
            string str = "";
            str += merchant_site_code + " ";
            str += return_url + " ";
            str += receiver + " ";
            str += transaction_info + " ";
            str += order_code.ToString() + " ";
            str += price.ToString() + " ";
            str += currency + " ";
            str += quantity.ToString() + " ";
            str += tax.ToString() + " ";
            str += discount.ToString() + " ";
            str += fee_cal.ToString() + " ";
            str += fee_shipping.ToString() + " ";
            str += order_description + " ";
            str += buyer_info + " ";
            str += affiliate_code + " ";
            str += "3b576e2f9d6c3bf68f2bc7476ae2f4cd";

            this.secure_code = MD5Hash(str);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
