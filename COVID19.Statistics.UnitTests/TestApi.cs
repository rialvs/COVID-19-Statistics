using COVID19.Statistics.API.Controllers;
using COVID19.Statistics.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace COVID19.Statistics.UnitTests
{
    public class TestApi
    {
        IStatisticsService statisticsService = new StatisticsService();
        StatisticsController statisticsController;

        public TestApi()
        {
            statisticsController = new StatisticsController(statisticsService);
        }

        [Fact]
        public async Task TestApi_GestStatisticsByRegion_ReturnData()
        {
            var result = await statisticsController.GetStatisticsByRegion();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestApi_GetStatisticsByProvince_ReturnData()
        {
            var result = await statisticsController.GetStatisticsByProvince("USA");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestApi_GetAllRegions_ReturnData()
        {
            var result = await statisticsController.GetAllRegions();
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
