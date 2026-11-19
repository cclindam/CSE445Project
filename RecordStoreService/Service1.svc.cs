using System;
using Newtonsoft.Json;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection.Emit;

namespace RecordStoreService
{
    
    public class Service1 : IService1
    {
        public string findNearestRecordStore(string zipcode, string storeName)
        {
            int zipcodeAsInt; 
            if (string.IsNullOrWhiteSpace(zipcode) ||
                zipcode.Length != 5 || !int.TryParse(zipcode, out zipcodeAsInt))
            {
                return "Provide valid ZIP code"; 
            }
            if (string.IsNullOrEmpty(storeName))
            {
                return "Provide valid store name"; 
            }

            GeoapifyLoc zipcodeLocation = GetZipcodeLoc(zipcode);
            List<GeoapifyLoc> stores = GetRecordStores(storeName);

            double minDistance = Double.MaxValue;
            GeoapifyLoc nearestStore = null; 
            foreach(GeoapifyLoc store in stores)
            {
                if (store.category != "commercial.video_and_music")
                {
                    continue; 
                }

                double distance = haverDistance(zipcodeLocation.lat, zipcodeLocation.lon,
                    store.lat, store.lon); 

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestStore = store; 
                }
            }


            return nearestStore.formatted;
        }

        private double haverDistance(double lon1, double lat1, 
            double lon2, double lat2)
        {
            double earthRadius = 3963; 
            double latRad1 = lat1 * Math.PI / 180;
            double latRad2 = lat2 * Math.PI / 180;
            double delLat = (lat2 - lat1) * Math.PI / 180;
            double delLon = (lon2 - lon1) * Math.PI / 180;

            double a = Math.Sin(delLat / 2) * Math.Sin(delLat / 2) +
                       Math.Cos(latRad1) * Math.Cos(latRad2) *
                       Math.Sin(delLon / 2) * Math.Sin(delLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadius * c; 
        }

        private GeoapifyLoc GetZipcodeLoc(string zipcode)
        {
            string apiKey = ConfigurationManager.AppSettings["GeoapifyApiKey"];
            string url = "https://api.geoapify.com/v1/geocode/search" +
                "?postcode=" + Uri.EscapeDataString(zipcode) +
                "&country=United%20States" + 
                "&format=json" + 
                "&apiKey=" + Uri.EscapeDataString(apiKey);
            HttpClient client = new HttpClient();
            try
            {
               
                HttpResponseMessage response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                GeoapifyResponse result = JsonConvert.DeserializeObject<GeoapifyResponse>(jsonResponse);
                

                if (result == null || result.results == null || result.results.Count == 0)
                {

                    return null;
                }

                return result.results[0];
            } finally
            {
                client.Dispose(); 
            }
        }
        private List<GeoapifyLoc> GetRecordStores(string storeName)
        {
            string apiKey = ConfigurationManager.AppSettings["GeoapifyApiKey"];

            string storeSearch = storeName + " Arizona"; 

            
            string url = "https://api.geoapify.com/v1/geocode/search" +
                "?text=" + Uri.EscapeDataString(storeSearch) +
                "&format=json" +
                "&limit=10" +
                "&apiKey=" + Uri.EscapeDataString(apiKey);
            
            
            HttpClient client = new HttpClient();
            try
            {

                HttpResponseMessage response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();

                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                GeoapifyResponse result = JsonConvert.DeserializeObject<GeoapifyResponse>(jsonResponse);


                if (result == null || result.results == null || result.results.Count == 0)
                {

                    return null;
                }

                return result.results;
            }
            finally
            {
                client.Dispose();
            }
        }
    }

    public class GeoapifyResponse
    { 
        public List<GeoapifyLoc> results { get; set; }
    }

    public class GeoapifyLoc
    { 
        public string name { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string formatted { get; set; }
        public string category { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }



    }

}
