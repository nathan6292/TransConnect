using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Projet_TransConnect
{
    public class Tools
    {   
        public static double GetTravelDuration(double[] startCoordinates, double[] endCoordinates)
        {
            // Get the Mapbox API token from the environment variables
            string mapboxToken = "pk.eyJ1IjoibmF0aGFuZmxldXJ5IiwiYSI6ImNsdXpxN2puODE5Y3cyam53bXJ2cTNqdG0ifQ.f3OLdt1DwM6NGTOaS82Dgw";

            // Create the request URL (We use ToString with the InvariantCulture to ensure that the decimal separator is a dot and not a comma)
            string coordinates = startCoordinates[0].ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + startCoordinates[1].ToString(System.Globalization.CultureInfo.InvariantCulture) + ";" + endCoordinates[0].ToString(System.Globalization.CultureInfo.InvariantCulture) + "," + endCoordinates[1].ToString(System.Globalization.CultureInfo.InvariantCulture);
            string requestUrl = $"https://api.mapbox.com/directions/v5/mapbox/driving/{coordinates}?&depart_at=2024-04-30T16%3A15&access_token={mapboxToken}";

            // Send the request and get the response
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(requestUrl).Result;

            // Check if the response is successful
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;

            // Parse the response to get the duration
            JObject jsonResponse = JObject.Parse(responseBody);
            double duration = (double)jsonResponse["routes"][0]["duration"];

            Console.WriteLine("Duration: " + duration);
            return duration;
        }
    }
}