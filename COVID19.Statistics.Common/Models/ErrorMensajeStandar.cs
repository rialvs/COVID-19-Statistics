using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace COVID19.Statistics.Common.Models
{
    public class ErrorMensajeStandar
    {
        [Required]
        [System.ComponentModel.DefaultValue(false)]
        public bool status { get; set; }

        [Required]
        public string message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
