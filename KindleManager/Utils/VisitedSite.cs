using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KindleManager.Utils
{
    public static class VisitedSite
    {

        public static List<Models.VisitedSiteModel> GetAll()
        {
            if (!File.Exists(AppSetting.VisitedSitesFileName))
            {
                return new List<Models.VisitedSiteModel>();
            }

            var data = JsonConvert.DeserializeObject<List<Models.VisitedSiteModel>>(File.ReadAllText(AppSetting.VisitedSitesFileName));
            return data;
        }

        public static void Add(string url)
        {
            var data = GetAll();

            data.Add(new Models.VisitedSiteModel
            {
                Date = DateTime.UtcNow,
                Url = url
            });

            string json = JsonConvert.SerializeObject(data);

            File.WriteAllText(AppSetting.VisitedSitesFileName, json);
        }

        public static bool IsSiteVisited(string url)
        {
            if (!File.Exists(AppSetting.VisitedSitesFileName))
            {
                return false;
            }
            var data = JsonConvert.DeserializeObject<List<Models.VisitedSiteModel>>(File.ReadAllText(AppSetting.VisitedSitesFileName));
            if (data.Any(x => x.Url == url))
            {
                return true;
            }
            return false;
        }
    }
}
