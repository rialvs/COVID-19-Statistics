using COVID19.Statistics.Common.Models;
using COVID19.Statistics.Services;
using Microsoft.AspNetCore.Mvc;

namespace COVID19.Statistics.API.Controllers
{
    /// <summary>
    /// Statistics Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : BaseController
    {
        IStatisticsService _statisticsService;

        /// <summary>
        /// Constructor's controller
        /// </summary>
        /// <param name="statisticsService">Interface whit the necesary methods to obtain the statistics COVID-19</param>
        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        /// <summary>
        /// Get the top 10 provinces filtered by region affected by COVID-19, with most COVID cases in descending order
        /// </summary>
        /// <param name="region">ISO code from region to be used as a filter</param>
        /// <returns>The top 10 provinces filtered by region affected by COVID-19</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TopStatistics>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMensajeStandar))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMensajeStandar))]
        [Produces("application/json")]
        [Route("[action]")]
        public async Task<IActionResult> GetStatisticsByProvince([FromQuery] string region)
        {
            try
            {
                var result = await _statisticsService.GetStatisticsByProvincesAsync(region);
                return Ok(result);
            }
            catch (Exception ex)
            {
                std.message = ex.Message;
                return BadRequest(std);
            }
        }

        /// <summary>
        /// Get the top 10 regions affected by COVID-19, with most COVID cases in descendent order
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TopStatistics>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMensajeStandar))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMensajeStandar))]
        [Produces("application/json")]
        [Route("[action]")]
        public async Task<IActionResult> GetStatisticsByRegion()
        {
            try
            {
                var result = await _statisticsService.GetStatisticsByRegionsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                std.message = ex.Message;
                return BadRequest(std);
            }
        }

        /// <summary>
        /// Get the list of available regions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorMensajeStandar))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMensajeStandar))]
        [Produces("application/json")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllRegions()
        {
            try
            {
                var result = await _statisticsService.GetAllRegionsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                std.message = ex.Message;
                return BadRequest(std);
            }
        }
    }
}