using COVID19.Statistics.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace COVID19.Statistics.API.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public class BaseController : ControllerBase
    {
        public ErrorMensajeStandar std = new ErrorMensajeStandar();
    }
}
