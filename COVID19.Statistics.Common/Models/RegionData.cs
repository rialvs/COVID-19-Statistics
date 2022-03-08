namespace COVID19.Statistics.Common.Models
{
    public class DatumRegions
    {
        public string iso { get; set; }
        public string name { get; set; }
    }

    public class RegionData
    {
        public List<DatumRegions> data { get; set; }
    }

}
