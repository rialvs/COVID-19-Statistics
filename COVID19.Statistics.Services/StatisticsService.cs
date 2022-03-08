using COVID19.Statistics.Common;
using COVID19.Statistics.Common.Helpers;
using COVID19.Statistics.Common.Models;

namespace COVID19.Statistics.Services
{
    public interface IStatisticsService
    {
        Task<List<TopStatistics>> GetStatisticsByRegionsAsync();
        Task<List<TopStatistics>> GetStatisticsByProvincesAsync(string region);
        Task<RegionData> GetAllRegionsAsync();
    }

    public class StatisticsService : IStatisticsService
    {
        public async Task<List<TopStatistics>> GetStatisticsByProvincesAsync(string region)
        {
            string str = string.Concat(Constants.URI_COVID19_STATISTICS, "?iso=", region);
            Uri uri = new Uri(str);

            CovidReports json = await GetReportsAsync<CovidReports>(uri, Constants.JSON_FILE_BY_PROVINCES_NAME);

            var topjson = GetTopStatistics(json, region);

            return topjson;
        }

        public async Task<List<TopStatistics>> GetStatisticsByRegionsAsync()
        {
            Uri uri = new Uri(Constants.URI_COVID19_STATISTICS);

            CovidReports json = await GetReportsAsync<CovidReports>(uri, Constants.JSON_FILE_BY_REGION_NAME);

            var topjson = GetTopStatistics(json, null);

            return topjson;
        }

        public async Task<RegionData> GetAllRegionsAsync()
        {
            Uri uri = new Uri(Constants.URI_COVID19_REGIONS);

            RegionData json = await GetReportsAsync<RegionData>(uri, Constants.JSON_FILE_REGIONS);

            return json;
        }

        private async Task<T> GetReportsAsync<T>(Uri uri, string jsonfilename)
        {
            using HttpHelper httpHelper = new HttpHelper();
            JsonHelper jsonHelper = new JsonHelper();

            jsonHelper.pathFiles = Path.Combine(AppContext.BaseDirectory, Constants.JSON_FOLDER, jsonfilename);

            Dictionary<string, string> headers = AddHeaders();

            T json;

            try
            {
                json = await httpHelper.ReadAsStringAsync<T>(HttpMethod.Get, uri, headers);

                //Save data for use if Api is unavaible later
                await jsonHelper.SaveJsonAsync(json);
            }
            catch (Exception)
            {
                //Service is unavaible. Read stored data
                json = await jsonHelper.LoadJsonAsync<T>();
            }

            return json;
        }

        private List<TopStatistics> GetTopStatistics(CovidReports json, string? region)
        {
            List<TopStatistics>? queryResult = null;

            if (json.data != null)
            {
                if (region == null)
                {
                    queryResult = json.data .GroupBy(g => g.region.name)
                                            .Select(s => new TopStatistics
                                            {
                                                region_province = s.First().region.name,
                                                cases = s.Sum(c => c.confirmed),
                                                deaths = s.Sum(c => c.deaths),
                                            })
                                            .OrderByDescending(o => o.cases)
                                            .Take(10)
                                            .ToList();
                }
                else
                {
                    queryResult = json.data.GroupBy(g => g.region.province)
                                            .Select(s => new TopStatistics
                                            {
                                                region_province = s.First().region.province,
                                                cases = s.Sum(c => c.confirmed),
                                                deaths = s.Sum(c => c.deaths),
                                            })
                                            .OrderByDescending(o => o.cases)
                                            .Take(10)
                                            .ToList();
                }

                if (queryResult != null)
                {
                    return queryResult;
                }
                else
                {
                    return new List<TopStatistics> { new TopStatistics() };
                }
            }
            else
            {
                return new List<TopStatistics> { new TopStatistics() };
            }
        }

        private Dictionary<string, string> AddHeaders()
        {
            Dictionary<string, string> headers = new();
            headers.Add(Constants.X_RAPIDAPI_HOST_KEY, Constants.X_RAPIDAPI_HOST_VALUE);
            headers.Add(Constants.X_RAPIDAPI_KEY_KEY, Constants.X_RAPIDAPI_KEY_VALUE);
            return headers;
        }
    }
}