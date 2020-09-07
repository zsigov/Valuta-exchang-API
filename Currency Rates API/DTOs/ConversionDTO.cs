
namespace Currency_Rates_API.DTOs
{
    //DTO class created to response CurrenciesController [HttpPost] request.
    public class ConversionDTO
    {
        public float BaseValue { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public float Result { get; set; }
    }
}
