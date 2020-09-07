using AutoMapper.Configuration.Annotations;
using Currency_Rates_API.Helper;
using Currency_Rates_API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Currency_Rates_API.DTOs
{
    public class HistoricDTO
    {
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, float> Rates { get; set; }
    }
}
