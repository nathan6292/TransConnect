using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using Projet_TransConnect;

namespace TransConnect{

    class Program
    {
        static void Main(string[] args){

        //Console.WriteLine("test");
        double[] startCoordinates = { 2.333333, 48.866667 };
        double[] endCoordinates = { -0.56667,44.833328};
        //Tools.ReadCSV("Distance.csv");

        Entreprise TransConnect = new Entreprise("Sauvegarde");
        TransConnect.ReadSauvegarde("Sauvegarde");


        Arbre graphe = new Arbre();
        graphe.InitiateGraphe();
        graphe.ToString();
        Console.Write(graphe.ToString());
        Console.ReadKey();
        Console.Clear();

       Console.WriteLine(TransConnect.Commandes[0].ToString());
       TransConnect.Commandes[0].CreerFacture("Facture.pdf");
           // TransConnect.Commandes[0].SendFacture("Facture.pdf");

        List<string> output = graphe.Shortest_Path("Angers", "Toulouse");

        for(int i = 0; i < output.Count; i++)
            {
                Console.Write(output[i] + " ");
            }

        //TransConnect.WriteSauvegarde("Sauvegarde");
        }
    }
}
