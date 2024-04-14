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

            // Création des employés

            Salarie directeurGeneral = new Salarie("Dupond", "Mr", new DateTime(1960, 1, 1), "Adresse1", "dupond@example.com", 1234567890, "Directeur Général", 10000, DateTime.Now, null);
            Salarie directriceCommerciale = new Salarie("Fiesta", "Mme", new DateTime(1975, 3, 15), "Adresse2", "fiesta@example.com", 1234567891, "Directrice Commerciale", 8000, DateTime.Now, directeurGeneral);
            Salarie commercial1 = new Salarie("Forge", "Mr", new DateTime(1985, 5, 20), "Adresse3", "forge@example.com", 1234567892, "Commercial", 6000, DateTime.Now, directriceCommerciale);
            Salarie commerciale1 = new Salarie("Fermi", "Mme", new DateTime(1990, 7, 10), "Adresse4", "fermi@example.com", 1234567893, "Commerciale", 6000, DateTime.Now, directriceCommerciale);
            Salarie directeurOperations = new Salarie("Fetard", "Mr", new DateTime(1963, 2, 28), "Adresse5", "fetard@example.com", 1234567894, "Directeur des opérations", 9000, DateTime.Now, directeurGeneral);
            Salarie chefEquipe1 = new Salarie("Royal", "Mr", new DateTime(1980, 8, 5), "Adresse6", "royal@example.com", 1234567895, "Chef d'Équipe", 7000, DateTime.Now, directeurOperations);
            Salarie chauffeur1 = new Salarie("Romu", "Mr", new DateTime(1992, 12, 25), "Adresse7", "romu@example.com", 1234567896, "Chauffeur", 5000, DateTime.Now, chefEquipe1);
            Salarie chauffeur2 = new Salarie("Romi", "Mr", new DateTime(1992, 12, 25), "Adresse7", "romu@example.com", 1234567896, "Chauffeur", 5000, DateTime.Now, chefEquipe1);
            Salarie chauffeur3 = new Salarie("Roma", "Mr", new DateTime(1992, 12, 25), "Adresse7", "romu@example.com", 1234567896, "Chauffeur", 5000, DateTime.Now, chefEquipe1);
            Salarie chefEquipe2 = new Salarie("Prince", "Mme", new DateTime(1978, 4, 17), "Adresse8", "prince@example.com", 1234567897, "Chef d'Équipe", 7000, DateTime.Now, directeurOperations);
            Salarie chauffeur4 = new Salarie("Rome", "Mme", new DateTime(1987, 6, 30), "Adresse9", "rome@example.com", 1234567898, "Chauffeur", 5000, DateTime.Now, chefEquipe2);
            Salarie chauffeur5 = new Salarie("Rimou", "Mme", new DateTime(1995, 10, 12), "Adresse10", "rimou@example.com", 1234567899, "Chauffeur", 5000, DateTime.Now, chefEquipe2);
            Salarie directriceRH = new Salarie("Joyeuse", "Mme", new DateTime(1970, 9, 3), "Adresse11", "joyeuse@example.com", 1234567800, "Directrice des RH", 9000, DateTime.Now, directeurGeneral);
            Salarie formation = new Salarie("Couleur", "Mme", new DateTime(1983, 11, 22), "Adresse12", "couleur@example.com", 1234567801, "Formation", 7000, DateTime.Now, directriceRH);
            Salarie contrats = new Salarie("ToutleMonde", "Mme", new DateTime(1986, 1, 8), "Adresse13", "toutlemonde@example.com", 1234567802, "Contrats", 7000, DateTime.Now, directriceRH);
            Salarie directeurFinancier = new Salarie("GripSous", "Mr", new DateTime(1967, 7, 19), "Adresse14", "gripsous@example.com", 1234567803, "Directeur Financier", 9000, DateTime.Now, directeurGeneral);
            Salarie directionComptable = new Salarie("Picsou", "Mr", new DateTime(1972, 5, 25), "Adresse15", "picsou@example.com", 1234567804, "Direction comptable", 8000, DateTime.Now, directeurFinancier);
            Salarie comptable1 = new Salarie("Fournier", "Mme", new DateTime(1982, 4, 13), "Adresse16", "fournier@example.com", 1234567805, "Comptable", 6000, DateTime.Now, directionComptable);
            Salarie comptable2 = new Salarie("Gautier", "Mme", new DateTime(1991, 6, 8), "Adresse17", "gautier@example.com", 1234567806, "Comptable", 6000, DateTime.Now, directionComptable);
            Salarie controleurGestion = new Salarie("GrosSous", "Mr", new DateTime(1989, 3, 1), "Adresse18", "grossoou@example.com", 1234567807, "Contrôleur de Gestion", 8000, DateTime.Now, directeurFinancier);

            // Ajout des subordonnés

            directeurGeneral.AjouterInferieur(directriceCommerciale);
            directriceCommerciale.AjouterInferieur(commercial1);
            directriceCommerciale.AjouterInferieur(commerciale1);

            directeurGeneral.AjouterInferieur(directeurOperations);
            directeurOperations.AjouterInferieur(chefEquipe1);
            chefEquipe1.AjouterInferieur(chauffeur1);
            chefEquipe1.AjouterInferieur(chauffeur2);
            chefEquipe1.AjouterInferieur(chauffeur3);

            directeurOperations.AjouterInferieur(chefEquipe2);
            chefEquipe2.AjouterInferieur(chauffeur4);
            chefEquipe2.AjouterInferieur(chauffeur5);

            directeurGeneral.AjouterInferieur(directriceRH);
            directriceRH.AjouterInferieur(formation);
            directriceRH.AjouterInferieur(contrats);

            directeurGeneral.AjouterInferieur(directeurFinancier);
            directeurFinancier.AjouterInferieur(directionComptable);
            directionComptable.AjouterInferieur(comptable1);
            directionComptable.AjouterInferieur(comptable2);

            directeurFinancier.AjouterInferieur(controleurGestion); 

            Entreprise TransConnect = new Entreprise("TransConnect", "Adresse19", "contact@transconnect.com", 1234567808, directeurGeneral);

            TransConnect.AjouterSalarie(directeurGeneral);
            TransConnect.AjouterSalarie(directriceCommerciale);
            TransConnect.AjouterSalarie(commercial1);
            TransConnect.AjouterSalarie(commerciale1);
            TransConnect.AjouterSalarie(directeurOperations);
            TransConnect.AjouterSalarie(chefEquipe1);
            TransConnect.AjouterSalarie(chauffeur1);
            TransConnect.AjouterSalarie(chauffeur2);
            TransConnect.AjouterSalarie(chauffeur3);
            TransConnect.AjouterSalarie(chefEquipe2);
            TransConnect.AjouterSalarie(chauffeur4);
            TransConnect.AjouterSalarie(chauffeur5);
            TransConnect.AjouterSalarie(directriceRH);
            TransConnect.AjouterSalarie(formation);
            TransConnect.AjouterSalarie(contrats);
            TransConnect.AjouterSalarie(directeurFinancier);
            TransConnect.AjouterSalarie(directionComptable);
            TransConnect.AjouterSalarie(comptable1);
            TransConnect.AjouterSalarie(comptable2);
            TransConnect.AjouterSalarie(controleurGestion);

            Tools.ReadCSV("./Distance.csv");
        }
    }
}
