namespace COVID19.Statistics.Common.Models
{
    public class City
    {
        public string name { get; set; }
        public string date { get; set; }
        public int? fips { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public string last_update { get; set; }
    }

    public class Region
    {
        public string iso { get; set; }
        public string name { get; set; }
        public string province { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
        public List<City> cities { get; set; }
    }

    public class DatumReports
    {
        public string date { get; set; }
        public int confirmed { get; set; }
        public int deaths { get; set; }
        public int recovered { get; set; }
        public int confirmed_diff { get; set; }
        public int deaths_diff { get; set; }
        public int recovered_diff { get; set; }
        public string last_update { get; set; }
        public int active { get; set; }
        public int active_diff { get; set; }
        public double fatality_rate { get; set; }
        public Region region { get; set; }
    }

    public class CovidReports
    {
        public List<DatumReports> data { get; set; }
    }
}
