using Currency_Rates_API.Models;
using System;
using System.Reflection;

namespace Currency_Rates_API.Helper
{
    //Class to support CurrenciesController methods in incoming data validation.
    public static class ControllerHelper
    {
        /*Helps CurrenciesController [HttpGet("historic/{numberOfDays}")] method
        to count the maximum available days what can be requested from client.*/
        public static double GetMaximumDays()
        {
            DateTime today = DateTime.Today;
            DateTime firstDate = new DateTime(1999, 1, 5);

            double maxDays = (today - firstDate).TotalDays;

            return maxDays;
        }

        //Helps to validate any incoming Currency name.
        public static PropertyInfo ValidateCurrencyName(string CurrencyName)
        {
            return typeof(Rates).GetProperty(CurrencyName);
        }

        //Helps CurrenciesController [HttpPost] method to validate incoming post body.
        public static bool ValidatePostBody(Conversion body)
        {
            var fromCurrency = ValidateCurrencyName(body.FromCurrency);
            var toCurrency = ValidateCurrencyName(body.ToCurrency);
            var value = body.Value;

            if(fromCurrency != null && toCurrency != null && value < int.MaxValue)
            {
                return true;
            }

            return false;
        }
    }
}
