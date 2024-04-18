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

            //Console.WriteLine("test");
            double[] startCoordinates = {7.25,43.7};
            double[] endCoordinates = {2.333333,48.866667};
            //Tools.ReadCSV("Distance.csv");
            //Tools.GetTravelDuration(startCoordinates,endCoordinates);

            Entreprise TransConnect = Entreprise.ReadEntreprise("Sauvegarde\\Entreprise.csv");
            TransConnect.ReadChauffeur("Sauvegarde\\Chauffeur.csv");
            TransConnect.ReadSalarie("Sauvegarde\\Salaries.csv");
            TransConnect.ReadRelation("Sauvegarde\\Relations.csv");

            Client client1 = new Client(1,"Jean","Dupont",new DateTime(1990,1,1),"1 rue de la paix","jean.dupont@gmail.com",0123456789);
            Client client2 = new Client(2, "Marie", "Martin", new DateTime(1995, 3, 15), "2 avenue des fleurs", "marie.martin@gmail.com", 987654321);
            Client client3 = new Client(3, "Pierre", "Dubois", new DateTime(1985, 7, 20), "3 rue du commerce", "pierre.dubois@gmail.com", 555555555);
            Client client4 = new Client(4, "Sophie", "Lefevre", new DateTime(1988, 9, 5), "4 rue des écoles", "sophie.lefevre@gmail.com", 111111111);
            Client client5 = new Client(5, "Thomas", "Moreau", new DateTime(1975, 11, 10), "5 avenue des champs", "thomas.moreau@gmail.com", 222222222);
            Client client6 = new Client(6, "Camille", "Garcia", new DateTime(2000, 2, 25), "6 rue du parc", "camille.garcia@gmail.com", 333333333);
            Client client7 = new Client(7, "Lucas", "Leroy", new DateTime(1993, 12, 30), "7 avenue des sports", "lucas.leroy@gmail.com", 444444444);
            Client client8 = new Client(8, "Emma", "Bonnet", new DateTime(1980, 6, 8), "8 boulevard des arts", "emma.bonnet@gmail.com", 666666666);
            Client client9 = new Client(9, "Hugo", "Roux", new DateTime(1978, 4, 18), "9 rue de la liberté", "hugo.roux@gmail.com", 777777777);
            Client client10 = new Client(10, "Manon", "Morel", new DateTime(1992, 10, 3), "10 rue de la victoire", "manon.morel@gmail.com", 888888888);

            TransConnect.Clients.Add(client1);
            TransConnect.Clients.Add(client2);
            TransConnect.Clients.Add(client3);
            TransConnect.Clients.Add(client4);
            TransConnect.Clients.Add(client5);
            TransConnect.Clients.Add(client6);
            TransConnect.Clients.Add(client7);
            TransConnect.Clients.Add(client8);
            TransConnect.Clients.Add(client9);
            TransConnect.Clients.Add(client10);
            
        
            TransConnect.SaveClient("Sauvegarde\\Clients.csv");

            
        }
    }
}
