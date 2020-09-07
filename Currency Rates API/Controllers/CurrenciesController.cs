using AutoMapper;
using Currency_Rates_API.DTOs;
using Currency_Rates_API.Helper;
using Currency_Rates_API.Models;
using Currency_Rates_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Rates_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyRepo _repository;
        private readonly IMapper _mapper;

        public CurrenciesController(ICurrencyRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Get api/currencies
        [HttpGet]
        public ActionResult<CurrenciesDTO> GetRates()
        {
            var rates = _repository.GetRates();

            //Variable 'rates.Result' returns null if the external server is inaccessible.
            if (rates.Result != null)
            {
                /*Mapping Currencies to CurrenciesDTO class with AutoMapper for increasing maintainability.
                Although in this case they are the same, it can change in the next version.*/
                return Ok(_mapper.Map<CurrenciesDTO>(rates.Result));
            }

            return Problem("Source server is unattainable");
        }

        //Get api/currencies/{string baseCurrency}
        [HttpGet("{baseCurrency}")]
        public ActionResult<CurrenciesDTO> GetRates(string baseCurrency)
        {
            //Validate request parameter. Returns null if {baseCurrency} value is not match with any property of Rates class.
            if (ControllerHelper.ValidateCurrencyName(baseCurrency) != null)
            {
                var rates = _repository.GetRatesByBaseCurrency(baseCurrency);

                //Variable 'rates.Result' returns null if the external server is inaccessible.
                if (rates.Result != null)
                {
                    return Ok(_mapper.Map<CurrenciesDTO>(rates.Result));
                }

                return Problem("Source server is unattainable");
            }
            //Probably save {baseCurrency} to database for development purposes.
            return UnprocessableEntity();
        }

        //Get api/currencies/historic/{int numberOfDays}        
        [HttpGet("historic/{numberOfDays}")]
        public ActionResult<HistoricDTO> GetRates(int numberOfDays)
        {
            /*Because the external server provides data from 1999-01-04, we have to validate if requested number of days 
            are not more than number of days elapsed since then. */
            if (ControllerHelper.GetMaximumDays() > numberOfDays)
            {
                var rates = _repository.GetRatesByDate(numberOfDays);

                //Variable 'rates.Result' returns null if the external server is inaccessible.
                if (rates.Result != null)
                { 
                    return Ok(rates.Result);
                }

                return Problem("Source server is unattainable");
            }
            //Probably save {numberOfDays} to database for development purposes.
            return UnprocessableEntity();
        }

        //Post api/currencies
        /* 
        Header content:
        Accept: application/json
        Content-Type: application/json
        */
        [HttpPost]
        public ActionResult<ConversionDTO> PostConversion([FromBody] Conversion body)
        {
            //Validate incoming request body. Returns true if validation is succesfull.
            if(ControllerHelper.ValidatePostBody(body))
            {
                var result = _repository.PostConversion(body);

                //Variable 'rates.Result' returns null if the external server is inaccessible.
                if (result.Result != null)
                {
                    return Ok(result.Result);
                }

                return Problem("Source server is unattainable");
            }

            //Probably save body to database for development purposes.
            return BadRequest();
        }
    }
}
