using COVID19.Statistics.Services;
using System.Threading.Tasks;
using Xunit;

namespace COVID19.Statistics.UnitTests
{
    public class TestSerices
    {
        IStatisticsService statisticsService = new StatisticsService();

        [Fact]
        public async Task TestSerices_IStatisticsService_GestStatisticsByRegionAsync_ReturnData()
        {
            var result = await statisticsService.GetStatisticsByRegionsAsync();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestSerices_IStatisticsService_GetStatisticsByProvincesAsync_ReturnData()
        {
            var result = await statisticsService.GetStatisticsByProvincesAsync("USA");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestSerices_IStatisticsService_GetAllRegionsAsync_ReturnData()
        {
            var result = await statisticsService.GetAllRegionsAsync();
            Assert.NotNull(result);
        }
    }
}
