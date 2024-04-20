using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransConnect;

namespace Projet_TransConnect
{
public class Entreprise
{
    protected string nom;
    protected string adresse;
    protected string mail;
    protected int telephone;
    protected List<Salarie> salaries;

    protected List<Client> clients;
    protected Salarie patron;                           //On considère que le patron est de la classe employé (pour faciliter l'orgranigramme)
    //protected List<Commande> commandes;
    protected List<Vehicule> vehicules;

    #region accesseurs
    public string Nom
    {
        get { return nom; }
        set { nom = value; }
    }

    public string Adresse
    {
        get { return adresse; }
        set { adresse = value; }
    }

    public string Mail
    {
        get { return mail; }
        set { mail = value; }
    }

    public int Telephone
    {
        get { return telephone; }
        set { telephone = value; }
    }

    public List<Salarie> Salaries
    {
        get { return salaries; }
        set { salaries = value; }
    }

    public Salarie Patron
    {
        get { return patron; }
        set { patron = value; }
    }

    public List<Client> Clients
    {
        get { return clients; }
        set { clients = value; }
    }

    public List<Vehicule> Vehicules
    {
        get { return vehicules; }
        set { vehicules = value; }
    }

    #endregion 
    //A enlever peut etre ??

    public Entreprise(string nom, string adresse, string mail, int telephone, Salarie patron)
    {
        this.nom = nom;
        this.adresse = adresse;
        this.mail = mail;
        this.telephone = telephone;
        this.salaries = new List<Salarie>();
        this.clients = new List<Client>();
        this.vehicules = new List<Vehicule>();
        this.patron = patron;
    }

    public Entreprise (string path)
    {
        string[] text = File.ReadAllLines(path + "\\Entreprise.csv");
        string[] elements = text[0].Split(',');

        this.nom = elements[0];
        this.adresse = elements[1];
        this.mail = elements[2];
        this.telephone = Convert.ToInt32(elements[3]);
        this.patron = null;
        this.salaries = new List<Salarie>();
        this.clients = new List<Client>();
        this.vehicules = new List<Vehicule>();
    }



    /// <summary>
    /// Ajouter un salarié à l'entreprise
    /// </summary>
    /// <param name="salarie"></param>


    #region Predicate utilisés pour les saisies
    Predicate<string> IsBool = new Predicate<string>(x => x == "oui" || x == "non");
    Predicate<string> IsDouble = new Predicate<string>(x => double.TryParse(x, out _));
    Predicate<string> IsInt = new Predicate<string>(x => long.TryParse(x, out _));
    Predicate<string> IsDate = new Predicate<string>(x => DateTime.TryParse(x, out _));
    Predicate<string> IsPastDate = new Predicate<string>(x => DateTime.Parse(x) < DateTime.Now);
    Predicate<string> IsMail = new Predicate<string>(x => x.Contains("@"));
    Predicate<string> IsNotEmpty = new Predicate<string>(x => x.Length > 0);
    Predicate<string> IsPositive = new Predicate<string>(x =>
    {
        if (double.TryParse(x, out double result))
        {
            return result >= 0;
        }
        else
        {
            return false;
        }
    });

    #endregion

    #region Affichage

    public string ToString()
    {
        return "Nom : " + nom + "\nAdresse : " + adresse + "\nMail : " + mail + "\nTéléphone : " + telephone + "\nDirigeant : " + patron.Nom + "\nNombre de salariés : " + salaries.Count + "\nNombre de clients : " + clients.Count;
    }


   

    /// <summary>
    /// Afficher l'organigramme de l'entreprise
    /// </summary>
    /// <param name="s"></param>
    /// <param name="tab"></param>
    public void Organigramme(Salarie s, int tab = 0)
    {
        if (s.IsFeuille())
        {
            s.ToStringOrganigramme(tab);
            Console.WriteLine("");
        }
        else
        {
            s.ToStringOrganigramme(tab);
            Console.WriteLine("");
            foreach (Salarie sal in s.InferieurHierachique)
            {
                Organigramme(sal, tab + 1);
                Console.WriteLine("");
            }
        }
    }

    /// <summary>
    /// Afficher la liste des salariés de l'entreprise
    /// </summary>
    public void AfficherSalarie(List<Salarie> liste = null)
    {
        if (liste == null) liste = salaries;
        foreach (Salarie s in liste)
        {
            Console.WriteLine(s.ToString() + "\n");
        }
    }

    /// <summary>
    /// Afficher la liste des clients de l'entreprise
    /// </summary>
    public void AfficherClient(List<Client> liste = null)
    {
        if (liste == null) liste = clients;
        foreach (Client c in liste)
        {
            Console.WriteLine(c.ToString() + "\n");
        }
    }

    public void AfficherVehicule(List<Vehicule> liste = null)
    {
        if (liste == null) liste = vehicules;
        foreach (Vehicule v in liste)
        {
            Console.WriteLine(v.ToString() + "\n");
        }
    }

    #endregion

    #region Créer/Modifier/Supprimer Salarie
    public void CreerSalarie()
    {
        string prenom = Tools.Saisie("Entrez le prénom du salarié : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le prénom ne peut pas être vide" } });
        string nom = Tools.Saisie("Entrez le nom du salarié : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le nom ne peut pas être vide" } });
        DateTime naissance = DateTime.Parse(Tools.Saisie("Entrez la date de naissance du salarié : ", new Dictionary<Predicate<string>, string> { { IsDate, "La date n'est pas valide" }, { IsPastDate, "La date n'est pas dans le passé" } }));
        string adresse = Tools.Saisie("Entrez l'adresse du salarié : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "L'adresse ne peut pas être vide" } });
        string mail = Tools.Saisie("Entrez l'adresse mail du salarié : ", new Dictionary<Predicate<string>, string> { { IsMail, "L'adresse mail n'est pas valide" } });
        string telephone = Tools.Saisie("Entrez le numéro de téléphone du salarié : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le numéro de téléphone n'est pas valide" }, { x => x.Length == 10, "Le numéro doit contenir 10 chiffres" } });
        bool IsChauffeur = Tools.Saisie("Le salarié est-il un chauffeur ? (Tapez 'oui' ou 'non' en miniscules) : ", new Dictionary<Predicate<string>, string> { { IsBool, "La réponse n'est pas valide" } }) == "oui";
        string poste;
        if (IsChauffeur)
        {
            poste = "Chauffeur";
        }
        else
        {
            poste = Tools.Saisie("Entrez le poste du salarié : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le poste ne peut pas être vide" } });
        }
        double salaire = double.Parse(Tools.Saisie("Entrez le salaire du salarié : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le salaire n'est pas valide" }, { IsPositive, "Le salaire ne peut pas être négatif" } }));
        DateTime dateEmbauche = DateTime.Parse(Tools.Saisie("Entrez la date d'embauche du salarié : ", new Dictionary<Predicate<string>, string> { { IsDate, "La date n'est pas valide" }}));
        Salarie sup = FindSalarie("Qui est le supérieur hiérarchique de ce nouvel employé ? ");
        bool inf = Tools.Saisie("Ce salarié a-t-il des inférieurs hiérarchiques ? (Tapez 'oui' ou 'non' en miniscules) : ", new Dictionary<Predicate<string>, string> { { IsBool, "La réponse n'est pas valide" } }) == "oui";
        List<Salarie> infHier = new List<Salarie>();
        List<Salarie> possibilites = sup.InferieurHierachique;
        while (inf)
        {
            infHier.Add(FindSalarie("Choisissez l'inférieur hierachique : ", sup.InferieurHierachique));
            possibilites.Remove(infHier.Last());
            if (possibilites.Count == 0)
            {
                inf = false;
            }
            else if (Tools.Saisie("Voulez-vous ajouter un autre inférieur hiérarchique ? (Tapez 'oui' ou 'non' en miniscules) : ", new Dictionary<Predicate<string>, string> { { IsBool, "La réponse n'est pas valide" } }) == "non")
            {
                inf = false;
            }
        }
        if (poste == "Chauffeur")
        {
            Chauffeur chauffeur = new Chauffeur(0, prenom, nom, naissance, adresse, mail, telephone, poste, salaire, dateEmbauche, sup, 0);
            sup.InferieurHierachique.Add(chauffeur);
            chauffeur.InferieurHierachique = infHier;
            salaries.Add(chauffeur);
        }
        else
        {
            Salarie salarie = new Salarie(0, prenom, nom, naissance, adresse, mail, telephone, poste, salaire, dateEmbauche, sup);
            sup.InferieurHierachique.Add(salarie);
            salarie.InferieurHierachique = infHier;
            salaries.Add(salarie);
        }
    }

    /// <summary>
    /// Supprimer/Licencier un salarié de l'entreprise
    /// </summary>
    /// <param name="salarie"></param>
    public void SupprimerSalarie(Salarie salarie)
    {
        Salarie vire = FindSalarie("Quel salarié voulez-vous licencier ? ");
        if (salarie.IsFeuille())    //Si il est a "au bas de l'échelle", on le supprime simplement
        {
            salaries.Remove(salarie);
            salarie.SuperieurHierachique.InferieurHierachique.Remove(salarie);
        }
        else        //Sinon, pour conserver la structure de l'arbre on :
        {
            foreach (Salarie s in salarie.InferieurHierachique)
            {
                s.SuperieurHierachique = salarie.SuperieurHierachique;  //1. On attribue ses inférieurs à son supérieur  
                salarie.SuperieurHierachique.InferieurHierachique.Add(s);
            }

            salarie.SuperieurHierachique.InferieurHierachique.Remove(salarie);    //2. On retire le salarié de la liste des inférieurs de son supérieur
            salaries.Remove(salarie); //3. On le retire de la liste des salariés
            salarie = null; //On libère de l'espace mémoire

        }
    }

    public Salarie FindSalarie(string text, List<Salarie> possibilites = null)
    {
        if (possibilites == null) possibilites = salaries;
        bool find = false;
        Console.WriteLine("Liste des salariés possibles : \n");
        AfficherSalarie(possibilites);
        Console.WriteLine("\nVoici la liste des salariés possibles \n");
        Console.WriteLine("\n\n" + text);
        Predicate<string> IsIDinPossibilite = new Predicate<string>(x => possibilites.Exists(y => y.Id == int.Parse(x)));
        int inputID = int.Parse(Tools.Saisie("Entrez l'ID du salarié parmi ceux proposés : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'ID n'est pas valide" }, { IsIDinPossibilite, "Cet ID n'est pas dans les choix possibles" } }));
        Console.Clear();
        return salaries.Find(x => x.Id == inputID);
    }

    public void ModifierSalarie()
    {
        Console.Clear();
        Console.WriteLine("Liste des salariés : \n");
        AfficherSalarie();
        Console.WriteLine("\nVoici la liste des salariés \n");
        Console.WriteLine("\n\nQuelle salarié voulez-vous modifier");
        Predicate<string> IsIDinSalaries = new Predicate<string>(x => salaries.Exists(y => y.Id == int.Parse(x)));
        int inputID = int.Parse(Tools.Saisie1("Entrez l'ID du salarié à modifier : ", new DictionnaireChainee<Predicate<string>, string>(new NoeudDico<Predicate<string>, string>(IsInt, "L'ID n'est pas valide"), new NoeudDico<Predicate<string>, string>(IsIDinSalaries, "Cet ID n'est pas dans les choix possibles"))));
        Salarie Amodifier = salaries.Find(x => x.Id == inputID);
        Console.Clear();
        bool finish = false;
        while (!finish)
        {
            Console.WriteLine("\n\nInformations sur le salarié :\n");
            Console.WriteLine(Amodifier.ToString());
            Console.WriteLine("\n\nQue souhaitez vous modifier ?\n");
            Console.WriteLine("1 - Adresse (Tapez 1)\n2 - Mail (Tapez 2)\n3 - Telephone (Tapez 3)\n4 - Poste (Tapez 4)\n5 - Salaire (Tapez 5)");
            int nbChoice = 5;
            if (Amodifier is Chauffeur)
            {
                Console.WriteLine("6 - Tarif Horaire (Tapez 6)"); 
                nbChoice++;
            }
            int inputChoice = int.Parse(Tools.Saisie1("\nTapez le numéro correspondant à l'élément que vous voulez modifier :", new DictionnaireChainee<Predicate<string>, string> (new NoeudDico<Predicate<string>, string>( IsInt, "Veuillez entrer un chiffre entre 1 et " + nbChoice ), new NoeudDico<Predicate<string>, string>( x => int.Parse(x) >= 1 && int.Parse(x) <= nbChoice, "Veuillez entrer un chiffre entre 1 et " + nbChoice  ))));
            switch (inputChoice)
            {
                case 1:
                    Amodifier.Adresse = Tools.Saisie1("Entrez la nouvelle adresse : ", new DictionnaireChainee<Predicate<string>, string> ( new NoeudDico<Predicate<string>, string>(IsNotEmpty, "L'adresse ne peut pas être vide" )));
                    break;
                case 2:
                    Amodifier.Mail = Tools.Saisie1("Entrez le nouveau mail : ", new DictionnaireChainee<Predicate<string>, string> ( new NoeudDico<Predicate<string>, string>( IsMail, "L'adresse mail n'est pas valide" )));
                    break;
                case 3:
                    Amodifier.Telephone = Tools.Saisie1("Entrez le nouveau numéro de téléphone : ", new DictionnaireChainee<Predicate<string>, string> (new NoeudDico<Predicate<string>, string>(IsInt, "Le numéro de téléphone n'est pas valide" ), new NoeudDico<Predicate<string>, string> (x => x.Length == 10, "Le numéro doit contenir 10 chiffres" )));
                    break;
                case 4:
                    Amodifier.Poste = Tools.Saisie1("Entrez le nouveau poste : ", new DictionnaireChainee<Predicate<string>, string> (new NoeudDico<Predicate<string>, string>(IsNotEmpty, "Le poste ne peut pas être vide")));
                    break;
                case 5:
                    Amodifier.Salaire = double.Parse(Tools.Saisie1("Entrez le nouveau salaire : ", new DictionnaireChainee<Predicate<string>, string> (new NoeudDico<Predicate<string>, string>(IsDouble, "Le salaire n'est pas valide" ), new NoeudDico<Predicate<string>, string>(IsPositive, "Le salaire ne peut pas être négatif" ))));
                    break;
                case 6:
                    ((Chauffeur)Amodifier).TarifHoraire = double.Parse(Tools.Saisie1("Entrez le nouveau tarif horaire : ", new DictionnaireChainee<Predicate<string>, string>(new NoeudDico<Predicate<string>, string>(IsDouble, "Le tarif horaire n'est pas valide"), new NoeudDico<Predicate<string>, string>(IsPositive, "Le tarif horaire ne peut pas être négatif"))));
                    break;
            }
            if (Tools.Saisie1("Voulez-vous modifier un autre élément ? (Tapez 'oui' ou 'non' en miniscules) : ", new DictionnaireChainee<Predicate<string>, string>(new NoeudDico<Predicate<string>, string>(IsBool, "La réponse n'est pas valide"))) == "non")
            {
                finish = true;
            }
        }
        Console.WriteLine("Les modifications ont été enregistrées");
        Tools.EndOfProgram();
    }

    #endregion

    #region Créer/Modifier/Supprimer Client
    public void CreerClient()
    {
        string p = Tools.Saisie("Entrez le prénom du client : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le prénom ne peut pas être vide" } });
        string n = Tools.Saisie("Entrez le nom du client : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le nom ne peut pas être vide" } });
        DateTime na = DateTime.Parse(Tools.Saisie("Entrez la date de naissance du client : ", new Dictionary<Predicate<string>, string> { { IsDate, "La date n'est pas valide" }, { IsPastDate, "La date n'est pas dans le passé" } }));
        string a = Tools.Saisie("Entrez l'adresse du client : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "L'adresse ne peut pas être vide" } });
        string m = Tools.Saisie("Entrez l'adresse mail du client : ", new Dictionary<Predicate<string>, string> { { IsMail, "L'adresse mail n'est pas valide" } });
        string t = Tools.Saisie("Entrez le numéro de téléphone du client : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le numéro de téléphone n'est pas valide" }, { x => x.Length == 10, "Le numéro doit contenir 10 chiffres" } });
        clients.Add(new Client(0, p, n, na, a, m, t));
    }

    public void SupprimerClient()
    {
        Console.WriteLine("Liste des clients : \n");
        AfficherClient();
        Console.WriteLine("\nVoici la liste des clients \n");
        Console.WriteLine("\n\nQuelle client voulez-vous supprimer");
        Predicate<string> IsIDinClients = new Predicate<string>(x => clients.Exists(y => y.Id == int.Parse(x)));
        int inputID = int.Parse(Tools.Saisie("Entrez l'ID du client à supprimer : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'ID n'est pas valide" }, {IsIDinClients, "Cet ID n'est pas dans les choix possibles" } }));

        clients.Remove(clients.Find(x => x.Id == inputID));
    }

    public void ModifierClient()
    {
        Console.Clear();
        Console.WriteLine("Liste des clients : \n");
        AfficherClient();
        Console.WriteLine("\nVoici la liste des clients \n");
        Console.WriteLine("\n\nQuelle client voulez-vous modifier");
        Predicate<string> IsIDinClients = new Predicate<string>(x => clients.Exists(y => y.Id == int.Parse(x)));
        int inputID = int.Parse(Tools.Saisie("Entrez l'ID du client à modifier : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'ID n'est pas valide" }, { IsIDinClients, "Cet ID n'est pas dans les choix possibles" } }));
        Client Amodifier = clients.Find(x => x.Id == inputID);
        Console.Clear();
        bool finish = false;
        while (!finish)
        {
            Console.WriteLine("Informations sur le salarié :\n");
            Console.WriteLine(Amodifier.ToString());
            Console.WriteLine("\n\nQue souhaitez vous modifier ?\n");
            Console.WriteLine("1 - Adresse (Tapez 1)\n2 - Mail (Tapez 2)\n3 - Telephone (Tapez 3)");
            int inputChoice = int.Parse(Tools.Saisie("Tapez le numéro correspondant à l'élément que vous voulez modifier :", new Dictionary<Predicate<string>, string> {{IsInt , "Veuillez entrer un chiffre entre 1 et 3" },{ x => int.Parse(x) >= 1 && int.Parse(x) <= 3, "Veuillez entrer un chiffre entre 1 et 3" }}));
            switch (inputChoice)
            {
                case 1:
                    Amodifier.Adresse = Tools.Saisie("Entrez la nouvelle adresse : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "L'adresse ne peut pas être vide" } });
                    break;
                case 2:
                    Amodifier.Mail = Tools.Saisie("Entrez le nouveau mail : ", new Dictionary<Predicate<string>, string> { { IsMail, "L'adresse mail n'est pas valide" } });
                    break;
                case 3:
                    Amodifier.Telephone = Tools.Saisie("Entrez le nouveau numéro de téléphone : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le numéro de téléphone n'est pas valide" }, { x => x.Length == 10, "Le numéro doit contenir 10 chiffres" } });
                    break;
            }
            if (Tools.Saisie("Voulez-vous modifier un autre élément ? (Tapez 'oui' ou 'non' en miniscules) : ", new Dictionary<Predicate<string>, string> { { IsBool, "La réponse n'est pas valide" } }) == "non")
            {
                finish = true;
            }
        }
        Console.WriteLine("Les modifications ont été enregistrées");
        Tools.EndOfProgram();
    }

    #endregion

    #region Lecture/Ecriture de Fichiers
    public void ReadSauvegarde(string path)
    {
        try
        {
            ReadClient(path + "\\Clients.csv");
            ReadChauffeur(path + "\\Chauffeur.csv");
            ReadSalarie(path + "\\Salaries.csv");
            ReadRelation(path + "\\Relations.csv");
            ReadVehicule(path + "\\Vehicules.csv");
        }
        catch { }
    }

    public void WriteSauvegarde(string path)
    {
        try
        {
            SaveClient(path + "\\Clients.csv");
            SaveChauffeur(path + "\\Chauffeur.csv");
            SaveSalarie(path + "\\Salaries.csv");
            SaveRelation(path + "\\Relations.csv");
            SaveVehicule(path + "\\Vehicules.csv");
            SaveEntreprise(path + "\\Entreprise.csv");
        }
        catch { }
    }

    public void SaveEntreprise(string path)
    {
        List<string> text = new List<string>();
        text.Add(string.Format("{0},{1},{2},{3}", nom, adresse, mail, telephone));
        File.WriteAllLines(path, text);
    }

    public void SaveSalarie(string path)
    {
        List<string> text = new List<string>();
        foreach (Salarie salarie in salaries)
        {
            if (salarie.Poste != "Chauffeur")
            {
                text.Add(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                                             salarie.Id, salarie.Prenom, salarie.Nom,
                                             salarie.Naissance, salarie.Adresse, salarie.Mail,
                                             salarie.Telephone, salarie.Poste, salarie.Salaire,
                                             salarie.DateEmbauche.ToString("dd/MM/yyyy")));
            }
        }
        File.WriteAllLines(path, text);
    }

    public void SaveChauffeur(string path)
    {
        List<string> text = new List<string>();
        foreach (Salarie salarie in salaries)
        {
            if (salarie is Chauffeur c)
            {
                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                                             salarie.Id, salarie.Prenom, salarie.Nom,
                                             salarie.Naissance, salarie.Adresse, salarie.Mail,
                                             salarie.Telephone, salarie.Poste, salarie.Salaire,
                                             salarie.DateEmbauche.ToString("dd/MM/yyyy"), c.TarifHoraire);
                foreach (DateTime date in c.EmploiDuTemps)
                {
                    line += "," + date.ToString("dd/MM/yyyy");
                }
                text.Add(line);
            }
        }
        File.WriteAllLines(path, text);
    }

    public void ReadSalarie(string path)
    {
        string[] lignes = File.ReadAllLines(path);
        foreach (string ligne in lignes)
        {
            string[] elements = ligne.Split(',');

            int id = int.Parse(elements[0]);
            string prenom = elements[1];
            string nom = elements[2];
            DateTime naissance = DateTime.Parse(elements[3]);
            string adresse = elements[4];
            string mail = elements[5];
            string telephone = elements[6];
            string poste = elements[7];
            double salaire = double.Parse(elements[8]);
            DateTime dateEmbauche = DateTime.Parse(elements[9]);

            Salarie s = new Salarie(id, prenom, nom, naissance, adresse, mail, telephone, poste, salaire, dateEmbauche, null);
            if (s.Poste == "PDG")
            {
                this.Patron = s;
            }
            salaries.Add(s);
        }
    }

    public void ReadChauffeur(string path)
    {
        string[] lignes = File.ReadAllLines(path);

        foreach (string ligne in lignes)
        {
            string[] elements = ligne.Split(',');

            int id = int.Parse(elements[0]);
            string prenom = elements[1];
            string nom = elements[2];
            DateTime naissance = DateTime.Parse(elements[3]);
            string adresse = elements[4];
            string mail = elements[5];
            string telephone = elements[6];
            string poste = elements[7];
            double salaire = double.Parse(elements[8]);
            DateTime dateEmbauche = DateTime.Parse(elements[9]);
            double tarifHoraire = double.Parse(elements[10]);

            // Créer un objet Chauffeur et l'ajouter à la liste
            Chauffeur chauffeur = new Chauffeur(id, prenom, nom, naissance, adresse, mail, telephone, poste, salaire, dateEmbauche, null, tarifHoraire);

            // Ajouter les dates d'emploi du temps si elles sont disponibles
            for (int i = 11; i < elements.Length; i++)
            {
                chauffeur.EmploiDuTemps.Add(DateTime.Parse(elements[i]));
            }

            salaries.Add(chauffeur);
        }
    }

    public void SaveRelation(string path)
    {
        List<string> text = new List<string>();
        foreach (Salarie salarie in salaries)
        {
            if (salarie.InferieurHierachique.Count > 0)
            {
                string ligne = Convert.ToString(salarie.Id);
                foreach (Salarie Inf in salarie.InferieurHierachique)
                {
                    ligne += "," + Inf.Id;
                }
                text.Add(ligne);
            }
        }
        File.WriteAllLines(path, text);
    }

    public void ReadRelation(string path)
    {
        string[] text = File.ReadAllLines(path);

        foreach (string line in text)
        {
            string[] content = line.Split(',');
            Salarie sup = salaries.Find(x => x.Id == int.Parse(content[0]));
            for (int i = 1; i < content.Length; i++)
            {
                Salarie inf = salaries.Find(x => x.Id == int.Parse(content[i]));
                inf.SuperieurHierachique = sup;
                sup.InferieurHierachique.Add(inf);
            }
        }
    }

    public void SaveClient(string path)
    {
        List<string> text = new List<string>();
        foreach (Client client in clients)
        {
            text.Add(string.Format("{0},{1},{2},{3},{4},{5},{6}",
                                         client.Id, client.Prenom, client.Nom,
                                         client.Naissance, client.Adresse, client.Mail,
                                         client.Telephone));
        }
        File.WriteAllLines(path, text);
    }

    public void ReadClient(string path)
    {
        string[] lignes = File.ReadAllLines(path);
        foreach (string ligne in lignes)
        {
            string[] elements = ligne.Split(',');

            int id = int.Parse(elements[0]);
            string prenom = elements[1];
            string nom = elements[2];
            DateTime naissance = DateTime.Parse(elements[3]);
            string adresse = elements[4];
            string mail = elements[5];
            string telephone = elements[6];

            Client c = new Client(id, prenom, nom, naissance, adresse, mail, telephone);
            clients.Add(c);
        }
    }

    public void SaveVehicule(string path)
    {
        List<string> text = new List<string>();

        foreach (Vehicule vehicule in vehicules)
        {
            List<string> line = new List<string>();
            line.Add("Type");
            line.Add(vehicule.Immatriculation);
            line.Add(vehicule.Marque);
            line.Add(vehicule.Modele);
            line.Add(vehicule.Annee.ToString());

            switch (vehicule)
            {
                case Voiture v:
                    line[0] = "Voiture";
                    line.Add(v.NbPlaces.ToString());
                    break;
                case Camionette c:
                    line[0] = "Camionette";
                    line.Add(c.Volume.ToString());
                    line.Add(c.Usage);
                    break;
                case PoidsLourd pl:
                    line.Add(pl.Poids.ToString());
                    line.Add(pl.Volume.ToString());
                    switch (pl)
                    {
                        case CamionFrigorifique cf:
                            line[0] = "CamionFrigorifique";
                            line.Add(cf.NbGrpElectrogene.ToString());
                            break;
                        case CamionBenne cb:
                            line[0] = "CamionBenne";
                            line.Add(cb.NbBennes.ToString());
                            line.Add(cb.Grue.ToString());
                            break;
                        case CamionCiterne cc:
                            line[0] = "CamionCiterne";
                            line.Add(cc.TypeCuve);
                            break;
                    }
                    break;
            }

            text.Add(string.Join(",", line));
            line.Clear();
            foreach(DateTime date in vehicule.EmploiDuTemps)
            {
                line.Add(date.ToString("dd/MM/yyyy"));
            }
            text.Add(string.Join(",", line));
        }

        File.WriteAllLines(path, text);
    }

    public void ReadVehicule(string path)
    {
        string[] lines = File.ReadAllLines(path);

        for (int i = 0; i < lines.Length; i += 2)
        {
            Vehicule v = null;
            string[] line = lines[i].Split(',');
            string[] dates = lines[i + 1].Split(',');

            switch (line[0])
            {
                case "Voiture":
                    v = new Voiture(line[1], line[2], line[3], int.Parse(line[4]), int.Parse(line[5]));
                    break;
                case "Camionette":
                    v = new Camionette(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), line[6]);
                    break;
                case "CamionFrigorifique":
                    v = new CamionFrigorifique(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), int.Parse(line[7]));
                    break;
                case "CamionBenne":
                    v = new CamionBenne(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), int.Parse(line[7]), bool.Parse(line[8]));
                    break;
                case "CamionCiterne":
                    v = new CamionCiterne(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), line[7]);
                    break;
            }

            foreach (string date in dates)
            {
                try { v.EmploiDuTemps.Add(DateTime.Parse(date)); }
                catch{  }

            }

            vehicules.Add(v);
        }
    }


    #endregion
}
}
