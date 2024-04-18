using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Projet_TransConnect;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;

namespace TransConnect{
    class Program{
        static void Main(string[] args){

             
            double[] startCoordinates = { 7.25, 43.7 };
            double[] endCoordinates = { 2.333333, 48.866667 };
            //Tools.ReadCSV("Distance.csv");
            //Tools.GetTravelDuration(startCoordinates,endCoordinates);

            Entreprise TransConnect = new Entreprise("Sauvegarde");
            TransConnect.ReadSauvegarde("Sauvegarde");

            TransConnect.Organigramme(TransConnect.Patron);

            
        }
    }
}
