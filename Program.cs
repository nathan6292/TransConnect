using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Projet_TransConnect;

namespace TransConnect{
    class Program{

        static readonly HttpClient client = new HttpClient();
        static void Main(string[] args){

                Console.WriteLine("test");
                double[] startCoordinates = {7.25,43.7};
                double[] endCoordinates = {2.333333,48.866667};
                Tools.GetTravelDuration(startCoordinates,endCoordinates);
        }
    }
}
