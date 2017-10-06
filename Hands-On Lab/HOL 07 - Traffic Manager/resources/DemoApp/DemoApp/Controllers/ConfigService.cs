using System.Configuration;

namespace TrafficManagerDemo.Web.Controllers
{
    public static class ConfigService
    {
        public static string GetRegionName()
        {
            var appSettings = ConfigurationManager.AppSettings;
            var value = appSettings["azure.RegionName"];

            return string.IsNullOrWhiteSpace(value) ? 
                "No Region" : value;
        }
    }
}