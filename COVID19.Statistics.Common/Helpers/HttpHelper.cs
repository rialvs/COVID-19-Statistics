using Newtonsoft.Json;

namespace COVID19.Statistics.Common.Helpers
{
    public class HttpHelper : IDisposable
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<T?> ReadAsStringAsync<T>(HttpMethod _Method, Uri uri, Dictionary<string, string>? headers)
        {
            var request = new HttpRequestMessage
            {
                Method = _Method,
                RequestUri = uri
            };

            if (headers != null)
            {
                foreach (var item in headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else
                {
                    throw new NullReferenceException("Response content is null");
                }
            }
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }
}