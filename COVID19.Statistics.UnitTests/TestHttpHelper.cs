using COVID19.Statistics.Common;
using COVID19.Statistics.Common.Helpers;
using COVID19.Statistics.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace COVID19.Statistics.UnitTests
{
    public class TestHttpHelper
    {
        [Fact]
        public async Task HttpHelper_ReadAsStringAsync_ReturnCorrect()
        {
            Uri uri = new Uri(Constants.URI_COVID19_STATISTICS);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(Constants.X_RAPIDAPI_HOST_KEY, Constants.X_RAPIDAPI_HOST_VALUE);
            headers.Add(Constants.X_RAPIDAPI_KEY_KEY, Constants.X_RAPIDAPI_KEY_VALUE);

            using HttpHelper httpHelper = new HttpHelper();
            var json = await httpHelper.ReadAsStringAsync<CovidReports>(HttpMethod.Get, uri, headers);

            Assert.True(json.data != null);
        }
    }
}