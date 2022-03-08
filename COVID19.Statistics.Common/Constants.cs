namespace COVID19.Statistics.Common
{
    public static class Constants
    {
        public const string URI_COVID19_STATISTICS = @"https://covid-19-statistics.p.rapidapi.com/reports";
        public const string URI_COVID19_REGIONS = @"https://covid-19-statistics.p.rapidapi.com/regions";
        public const string X_RAPIDAPI_HOST_KEY = @"x-rapidapi-host";
        public const string X_RAPIDAPI_HOST_VALUE = @"covid-19-statistics.p.rapidapi.com";
        public const string X_RAPIDAPI_KEY_KEY = @"x-rapidapi-key";
        public const string X_RAPIDAPI_KEY_VALUE = @"d4c406ecb6msh691ea1faeab821bp12bfacjsnc7ebd0bcbd60";
        public const string JSON_FILE_BY_REGION_NAME = @"response_rn.json";
        public const string JSON_FILE_BY_PROVINCES_NAME = @"response_pr.json";
        public const string JSON_FILE_REGIONS = @"response_re.json";
        public const string JSON_FOLDER = @"JsonFiles";
        public const string URL_API_BY_REGION = @"https://localhost:7176/api/Statistics/GetStatisticsByRegion";
        public const string URL_API_BY_PROVINCES = "https://localhost:7176/api/Statistics/GetStatisticsByProvince?region=";
        public const string URL_API_ALL_REGIONS = "https://localhost:7176/api/Statistics/GetAllRegions";


    }
}
