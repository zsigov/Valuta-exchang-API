using System.ComponentModel.DataAnnotations;

namespace Currency_Rates_API.Models
{
    //Class to consume CurrenciesController [HttpPost] method's body.
    public class Conversion
    {
        [Required]
        public float Value { get; set; }

        [Required]
        [MaxLength(3)]
        public string FromCurrency { get; set; }

        [Required]
        [MaxLength(3)]
        public string ToCurrency { get; set; }
    }
}
