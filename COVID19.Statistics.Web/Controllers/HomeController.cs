using COVID19.Statistics.Common;
using COVID19.Statistics.Common.Helpers;
using COVID19.Statistics.Common.Models;
using COVID19.Statistics.Web.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Globalization;
using System.Xml.Serialization;

namespace COVID19.Statistics.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string SESSION_KEY_DATA = "SessionData";
        private const string SESSION_KEY_REGION = "SessionRegion";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Regions = await GenViewBagRegionsAsync();

                var obj = HttpContext.Session.GetObjectFromJson<List<TopStatistics>>(SESSION_KEY_DATA);

                if (obj != null)
                {
                    ViewBag.Data = obj;
                }

                ViewBag.ByRegion = HttpContext.Session.GetObjectFromJson<bool>(SESSION_KEY_REGION);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeViewModel)
        {
            try
            {
                Uri uri;
                List<TopStatistics>? topStatistics = new List<TopStatistics>();

                ViewBag.Regions = await GenViewBagRegionsAsync();

                bool byRegion = false;

                if (homeViewModel.region == null)
                {
                    uri = new Uri(Constants.URL_API_BY_REGION);
                    byRegion = true;
                }
                else
                {
                    uri = new Uri(string.Concat(Constants.URL_API_BY_PROVINCES, homeViewModel.region));
                }

                using HttpHelper httpHelper = new HttpHelper();
                topStatistics = await httpHelper.ReadAsStringAsync<List<TopStatistics>>(HttpMethod.Get, uri, null);

                ViewBag.Data = topStatistics;
                ViewBag.ByRegion = byRegion;

                HttpContext.Session.SetObjectAsJson(SESSION_KEY_DATA, topStatistics);
                HttpContext.Session.SetObjectAsJson(SESSION_KEY_REGION, byRegion);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            return View();
        }

        public ActionResult GetCSV()
        {
            try
            {
                //throw new Exception();
                var obj = HttpContext.Session.GetObjectFromJson<List<TopStatistics>>(SESSION_KEY_DATA);

                MemoryStream ms;
                using (var memoryStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(memoryStream))
                using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(obj);
                    streamWriter.Flush();
                    var result = memoryStream.ToArray();
                    ms = new MemoryStream(result);
                }

                return new FileStreamResult(ms, "text/csv") { FileDownloadName = "Top10CovidCases.csv" };
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetXML()
        {
            try
            {
                //throw new Exception();
                var obj = HttpContext.Session.GetObjectFromJson<List<TopStatistics>>(SESSION_KEY_DATA);

                var ser = new XmlSerializer(typeof(List<TopStatistics>));
                var ms = new MemoryStream();
                ser.Serialize(ms, obj);
                ms.Position = 0;

                return new FileStreamResult(ms, "application/xml") { FileDownloadName = "Top10CovidCases.xml" };
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetJSON()
        {
            try
            {
                //throw new Exception();
                var obj = HttpContext.Session.GetObjectFromJson<List<TopStatistics>>(SESSION_KEY_DATA);

                var data = JsonConvert.SerializeObject(obj);

                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                MemoryStream ms = new MemoryStream(bytes);

                return new FileStreamResult(ms, "application/json") { FileDownloadName = "Top10CovidCases.json" };
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        private async Task<List<SelectListItem>> GenViewBagRegionsAsync()
        {
            Uri uri = new Uri(Constants.URL_API_ALL_REGIONS);

            using HttpHelper httpHelper = new HttpHelper();
            RegionData? data = await httpHelper.ReadAsStringAsync<RegionData>(HttpMethod.Get, uri, null);

            List<SelectListItem> slregions = new List<SelectListItem>();

            if (data != null)
            {
                foreach (var region in data.data)
                {
                    slregions.Add(new SelectListItem { Value = region.iso, Text = region.name });
                }
            }

            return slregions;
        }

        private void SetError(string Message)
        {
            if (ViewBag.Regions == null)
            {
                ViewBag.Regions = new List<SelectListItem>();
            }

            ModelState.AddModelError("", Message);
        }
    }
}