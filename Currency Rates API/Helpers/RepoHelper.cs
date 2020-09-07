using Currency_Rates_API.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Currency_Rates_API.Helper
{
    //Class to support CurrencyRepo class methods.
    public class RepoHelper
    {
        //Consumes the json formated string result from a given url.
        public static async Task<string> GetStringAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(url);
            }
        }

        //Returns the objec type value of a given property from a given source class. Returns null if property does not exists.
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        //Returns a dictionary with Currency names and rates whose value are not equal to 0. 
        public static Dictionary<string, float> GetRatesWithWalues(Rates rates)
        {
            Dictionary<string, float> RatesDict = new Dictionary<string, float>();

            foreach (PropertyInfo propertyInfo in rates.GetType().GetProperties())
            {
                var getPropValue = RepoHelper.GetPropValue(rates, propertyInfo.Name);

                if (float.Parse(getPropValue.ToString()) != 0)
                {
                    RatesDict.Add(propertyInfo.Name, float.Parse(getPropValue.ToString()));
                }
            }

            return RatesDict;
        }
    }
}
