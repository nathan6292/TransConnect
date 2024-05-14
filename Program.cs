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
            Start();
        }

        static void Start()
        {
            string password = "1234";
            Console.WriteLine("Veuillez entrer le mot de passe pour accéder à la sauvegarde\n");
            Console.WriteLine("Rentrez stop pour quitter le programme\n");
            Console.WriteLine("[Le mot de passe est 1234]");
            string input = Console.ReadLine();
            if (input == "stop")
            {
                Environment.Exit(0);
            }
            else
            if (input == password)
            {
                Console.WriteLine("Mot de passe correct");
                Console.WriteLine("Chargement de la sauvegarde...");
                Entreprise TransConnect = new Entreprise("Sauvegarde");
                TransConnect.ReadSauvegarde("Sauvegarde");
                Console.WriteLine("Sauvegarde chargée");
                Sortie();
                Menu(TransConnect);
                TransConnect.WriteSauvegarde("Sauvegarde");
            }
            else
            {
                Console.WriteLine("Mot de passe incorrect");
                Console.WriteLine("Veuillez réessayer");
                Start();
            }
        }

        static void Menu(Entreprise Transconnect)
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Module Clients (Tapez 1)");
                Console.WriteLine("Module Salariés (Tapez 2)");
                Console.WriteLine("Module Vehicules (Tapez 3)");
                Console.WriteLine("Module Commandes (Tapez 4)");
                Console.WriteLine("Module Statistiques (Tapez 5)");
                Console.WriteLine("\n\nTapez 0 pour quitter\n\n");
                Predicate<string> IsInt = new Predicate<string>(x => int.TryParse(x, out _));
                Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) >= 0 && int.Parse(x) < 6);
                choice = int.Parse(Tools.Saisie("Rentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 5" } }));
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        MenuClient(Transconnect);
                        break;

                    case 2:
                        Console.Clear();
                        MenuSalarie(Transconnect);
                        break;

                    case 3:
                        Console.Clear();
                        MenuVehicule(Transconnect);
                        break;

                    case 4:
                        Console.Clear();
                        MenuCommandes(Transconnect);
                        break;

                    case 5:
                        Console.Clear();
                        MenuStatistique(Transconnect);
                        break;
                }
            }
            while (choice != 0);
        }

        static void MenuClient(Entreprise Transconnect)
        {
            Console.WriteLine("Sélectionnez une option :");
            Console.WriteLine("Afficher les clients (Tapez 1)");
            Console.WriteLine("Créer un client (Tapez 2)");
            Console.WriteLine("Modifier un client (Tapez 3)");
            Console.WriteLine("Supprimer un client (Tapez 4)");

            Console.WriteLine("\n\nTapez 0 pour quitter\n\n");

            Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
            Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) >= 0 && int.Parse(x) < 5);

            int choice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 4" } }));

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Sélectionnez l'ordre d'affichage :");
                    Console.WriteLine("Ordre alphabétique sur le nom de famille (Tapez 1)");
                    Console.WriteLine("Adresse (Tapez 2)");
                    Console.WriteLine("Montant décroissant achats cumulés (Tapez 3)");

                    Predicate<string> IsOrderChoice = new Predicate<string>(x => int.Parse(x) > 0 && int.Parse(x) < 4);

                    int orderChoice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsOrderChoice, "Veuillez entrer un chiffre entre 1 et 3" } }));
                    Console.Clear();
                    Comparison<Client> compare;
                    switch (orderChoice)
                    {
                        case 1:
                            compare = new Comparison<Client>((Client c1, Client c2) => c1.Nom.CompareTo(c2.Nom));
                            Transconnect.AfficherClient(null, compare);
                            break;
                        case 2:
                            compare = new Comparison<Client>((Client c1, Client c2) => c1.Adresse.CompareTo(c2.Adresse));
                            Transconnect.AfficherClient(null, compare);
                            break;
                        case 3:
                            compare = new Comparison<Client>((Client c1, Client c2) => Transconnect.AchatCumules(c2).CompareTo(Transconnect.AchatCumules(c1)));
                            Transconnect.AfficherClient(null, compare);
                            break;
                    }
                    Sortie();
                    break;
                case 2:
                    Console.Clear();
                    Transconnect.CreerClient();
                    Sortie();
                    break;
                case 3:
                    Console.Clear();
                    Transconnect.ModifierClient();
                    Sortie();
                    break;
                case 4:
                    Console.Clear();
                    Transconnect.SupprimerClient();
                    Sortie();
                    break;
            }
        }

        static void MenuSalarie(Entreprise Transconnect)
        {
            Console.WriteLine("Sélectionnez une option :");
            Console.WriteLine("Afficher les salariés (Tapez 1)");
            Console.WriteLine("Afficher l'organigramme (Tapez 2)");
            Console.WriteLine("Créer un salarié (Tapez 3)");
            Console.WriteLine("Modifier un salarié (Tapez 4)");
            Console.WriteLine("Licencier un salarié (Tapez 5)");

            Console.WriteLine("\n\nTapez 0 pour quitter\n\n");


            Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
            Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) >= 0 && int.Parse(x) < 6);

            int choice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 5" } }));

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Transconnect.AfficherSalarie();
                    Sortie();
                    break;
                case 2:
                    Console.Clear();
                    Transconnect.Organigramme(Transconnect.Patron);
                    Sortie();
                    break;
                case 3:
                    Console.Clear();
                    Transconnect.CreerSalarie();
                    Sortie();
                    break;
                case 4:
                    Console.Clear();
                    Transconnect.ModifierSalarie();
                    Sortie();
                    break;
                case 5:
                    Console.Clear();
                    Transconnect.SupprimerSalarie();
                    Sortie();
                    break;
            }
        }

        static void MenuVehicule(Entreprise Transconnect)
        {
            Console.WriteLine("Sélectionnez une option :");
            Console.WriteLine("Afficher les véhicules (Tapez 1)");
            Console.WriteLine("Créer un véhicule (Tapez 2)");
            Console.WriteLine("Supprimer un véhicule (Tapez 3)");

            Console.WriteLine("\n\nTapez 0 pour quitter\n\n");


            Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
            Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) > 0 && int.Parse(x) < 4);

            int choice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 3" } }));

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Transconnect.AfficherVehicule();
                    Sortie();
                    break;
                case 2:
                    Console.Clear();
                    Transconnect.CreerVehicule();
                    Sortie();
                    break;
                case 3:
                    Console.Clear();
                    Transconnect.SupprimerVehicule();
                    Sortie();
                    break;
            }
        }

        static void MenuStatistique(Entreprise Transconnect)
        {
            Console.WriteLine("Sélectionnez une option :");
            Console.WriteLine("Nombre de livraisons par chauffeur (Tapez 1)");
            Console.WriteLine("Afficher les commandes sur une période (Tapez 2)");
            Console.WriteLine("Moyenne des prix des commandes (Tapez 3)");
            Console.WriteLine("Moyenne des comptes clients (Tapez 4)");
            Console.WriteLine("Liste des commandes pour un client (Tapez 5)");

            Console.WriteLine("\n\nTapez 0 pour quitter\n\n");

            Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
            Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) >= 0 && int.Parse(x) < 6);

            int choice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 5" } }));


            Predicate<string> IsDate = new Predicate<string>(x => DateTime.TryParse(x, out _));

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Transconnect.AfficherNblivraisonParChauffeur();
                    Sortie();
                    break;
                case 2:
                    Console.Clear();
                    DateTime before = DateTime.Parse(Tools.Saisie("A partir de quelle date voulez-vous voir les commandes :", new Dictionary<Predicate<string>, string> { { IsDate, "Veuillez entrer une date valide" } }));
                    Predicate<string> IsAfterBefore = new Predicate<string>(x => DateTime.Parse(x) > before);
                    DateTime after = DateTime.Parse(Tools.Saisie("Jusqu'à quelle date voulez-vous voir les commandes :", new Dictionary<Predicate<string>, string> { { IsDate, "Veuillez entrer une date valide" }, { IsAfterBefore, "La date est avant la date précédente" } }));
                    Console.Clear();
                    Transconnect.AfficherCommandeEntreDates(before, after);
                    Sortie();
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Moyenne des prix des commandes : \n\n" + Transconnect.MoyennePrixCommande());
                    Sortie();
                    break;
                case 4:
                    Console.Clear();
                    Console.WriteLine("Moyenne des comptes clients : \n\n" + Transconnect.MoyenneCompteClient());
                    Sortie();
                    break;
                case 5:
                    Console.Clear();
                    Client c = Transconnect.FindClient("Saisir le nom du client dont vous voulez afficher les commandes : ");
                    Transconnect.AffciherCommandeClient(c);
                    Sortie();
                    break;
            }
        }

        static void MenuCommandes(Entreprise Transconnect)
        {
            Console.WriteLine("Sélectionnez une option :");
            Console.WriteLine("Afficher toutes les commandes (Tapez 1)");
            Console.WriteLine("Créer une commande (Tapez 2)");
            Console.WriteLine("Supprimer une commande (Tapez 3)");
            Console.WriteLine("Créer une facture (Tapez 4)");
            Console.WriteLine("Envoyer une facture (Tapez 5)");

            Console.WriteLine("\n\nTapez 0 pour quitter\n\n");

            Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
            Predicate<string> IsChoice = new Predicate<string>(x => int.Parse(x) >= 0 && int.Parse(x) < 6);

            int choice = int.Parse(Tools.Saisie("\n\nRentrez le chiffre correspondant au choix : ", new Dictionary<Predicate<string>, string> { { IsInt, "Veuillez entrer un chiffre" }, { IsChoice, "Veuillez entrer un chiffre entre 0 et 5" } }));

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Transconnect.AfficherCommande();
                    Sortie();
                    break;
                case 2:
                    Console.Clear();
                    Transconnect.CreerCommande();
                    Sortie();
                    break;
                case 3:
                    Console.Clear();
                    Transconnect.SupprimerCommande();
                    Sortie();
                    break;
                case 4:
                    Console.Clear();
                    Commande aFacturer = Transconnect.FindCommande("Pour quelle commande souhaitez-vous créer une facture : ");
                    aFacturer.CreerFacture();
                    Sortie();
                    break;
                case 5:
                    Console.Clear();
                    Commande aEnvoyer = Transconnect.FindCommande("Pour quelle commande souhaitez-vous envoyer une facture : ");
                    aEnvoyer.SendFacture();
                    Sortie();
                    break;
            }
        }



        static void Sortie()
        {
            Console.WriteLine("Appuyez sur n'importe quelle touche...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
