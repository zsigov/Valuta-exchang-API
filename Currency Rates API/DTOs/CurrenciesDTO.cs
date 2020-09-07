using Currency_Rates_API.Models;

namespace Currency_Rates_API.DTOs
{
    //DTO class created to response CurrenciesController [HttpGet] requests.
    public class CurrenciesDTO
    {
            public Rates Rates { get; set; }
            public string Base { get; set; }
            public string Date { get; set; }          
    }  
}
