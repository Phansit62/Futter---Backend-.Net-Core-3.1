using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndFlutter.Helpers
{
    public class Constants
    {
        public static string DirectoryPayments = "\\uploadpayments\\";
        public static string DirectoryFood = "\\uploadfoods\\";
        public static string FoodsImage = "Food" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
        public static string PaymentsImage = "payment" + DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
    }
}
