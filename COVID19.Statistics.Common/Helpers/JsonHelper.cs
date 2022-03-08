using Newtonsoft.Json;

namespace COVID19.Statistics.Common.Helpers
{
    public class JsonHelper
    {
        public string pathFiles = "";
        
        public JsonHelper()
        {
            ValidatePath();
        }

        public async Task<T?> LoadJsonAsync<T>() 
        {
            var defaultvalue = (T)Activator.CreateInstance(typeof(T));

            if (File.Exists(pathFiles))
            {
                using (StreamReader sr = new StreamReader(pathFiles))
                {
                    string jsonlp = await sr.ReadToEndAsync();

                    try
                    {
                        return JsonConvert.DeserializeObject<T>(jsonlp);
                    }
                    catch (Exception)
                    {
                        return defaultvalue;
                    }
                }
            }
            else
            {
                return defaultvalue;
            }
        }

        public async Task SaveJsonAsync<T>(T json)
        {
            string outputJson = Newtonsoft.Json.JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(pathFiles, outputJson);
        }

        private void ValidatePath()
        {
            string pathFiles = Path.Combine(AppContext.BaseDirectory, Constants.JSON_FOLDER);

            if (!Directory.Exists(pathFiles))
            {
                Directory.CreateDirectory(pathFiles);
            }
        }
    }
}
