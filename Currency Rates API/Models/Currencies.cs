namespace Currency_Rates_API.Models
{
    //Class to represent all currency rates by a Base currency on a specified date.
    public class Currencies
    {
        public Rates Rates { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
    }  
}
