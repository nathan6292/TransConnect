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
using iTextSharp.text.pdf.parser;
using System.Security;
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

            return duration/60;
        }

        //Read an Excel file and return the data
        public static string[] ReadCSV(string path)
        {
            // Read the file
            string[] lines = System.IO.File.ReadAllLines(path);

            return lines;
        }

        public static string Saisie(string texte, Dictionary<Predicate<string>, string> dico)
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

        public static string Saisie1(string texte, DictionnaireChainee<Predicate<string>,string> dico)
        {
            string input = "";
            bool success = false;

            while (!success)
            {
                Console.Write(texte);
                input = Console.ReadLine();
                bool error = false;
                dico.ForEach((Predicate<string> a) =>
                {
                    if (!error)
                    {
                        if (a(input))
                        {
                            success = true;
                        }
                        else
                        {
                            Console.WriteLine(dico.Rechercher(a) + "\n");
                            success = false;
                            error = true;
                        }
                    }
                });
            }
            return input;
        }
        public static void EndOfProgram()
        {
            Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir au menu...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void UpdateCSV()
        {
            string[] lines = Tools.ReadCSV("./Sauvegarde/Distances.csv");
            List<string[]> text = new List<string[]>();

            string[] lines2 = Tools.ReadCSV("./Sauvegarde/Coordinates.csv");
            Dictionary<string,(double, double)> coordinates = new Dictionary<string, (double, double)>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(';');
                text.Add(values);
            }

            for (int i = 0; i < lines2.Length; i++)
            {
                string[] values = lines2[i].Split(';');
                coordinates.Add(values[0], (double.Parse(values[1]), double.Parse(values[2])));
                
            }

            for (int i = 0; i < text.Count; i++)
            {
                double[] Start_Coordinates = {coordinates[text[i][0]].Item1, coordinates[text[i][0]].Item2};
                double[] End_Coordinates = {coordinates[text[i][1]].Item1, coordinates[text[i][1]].Item2 };

                double duration = GetTravelDuration(Start_Coordinates,End_Coordinates);

                text[i][3] = duration.ToString();
            }

            List<string> output = new List<string>();
            for(int i = 0; i<text.Count; i++)
            {
                output.Add(text[i][0].ToString() + ";" + text[i][1].ToString() + ";" + text[i][2].ToString() + ";" + text[i][3].ToString());
            }

            File.WriteAllLines("./Sauvegarde/Distances.csv", output);
        }

        public static string doubleToTime(double value)
        {
            int heure = 0;

            while (value > 60)
            {
                heure++;
                value = value - 60;
            }

            value = Math.Round(value,0);
            if (value != 0)
            {
                return heure + "h" + value + "min";
            }
            else
            {
                return heure + "h";
            }
        }

    }
}
