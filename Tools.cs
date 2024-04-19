using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Projet_TransConnect;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Projet_TransConnect
{


        public class Tools
    {   
        
        //Predicate utile pour la suite :
       

        // Get the travel duration between two points in real time with an API
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

        //Read an Excel file and return the data
        public static string[] ReadCSV(string path)
        {
            // Read the file
            string[] lines = System.IO.File.ReadAllLines(path);

            return lines;
        }

        public static string Saisie(string texte, Dictionary<Predicate<string>,string> dico)
        {
            string input = "";
            bool success = false;

            while (!success)
            {
                Console.Write(texte);
                input = Console.ReadLine();
                foreach (var key in dico.Keys) 
                {
                    if (key(input))
                    {
                        success = true;
                    }
                    if (!key(input))
                    {
                        Console.WriteLine(dico[key] + "\n");
                        success = false;
                        break;
                    }
                }
            }
            return input;
        }

        public static double Time_StringtoDouble(string time){
            if (time.EndsWith("mn"))
            {
                // If the time ends with "mn", parse the minutes and divide by 60 to get the decimal equivalent
                return double.Parse(time.Replace("mn", "")) / 60;
            }
            else
            {
                // If the time is in the format "hh:mm", split it into hours and minutes
                var parts = time.Split('h');
                double hours = double.Parse(parts[0]);
                double minutes = double.Parse(parts[1]) / 60;
                return hours + minutes;
            }
                    
        }

        public static void EndOfProgram()
        {
            Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir au menu...");
            Console.ReadKey();
            Console.Clear();
        }
        
    }
}