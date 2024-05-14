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
    protected Arbre graphe;

    protected List<Client> clients;
    protected Salarie patron;                           //On considère que le patron est de la classe employé (pour faciliter l'orgranigramme)
    protected List<Commande> commandes;
    protected List<Vehicule> vehicules;

    #region accesseurs

    /// <summary>
    /// Accesseur de nom en lecture et en écriture
    /// </summary>
    public string Nom
    {
        get { return nom; }
        set { nom = value; }
    }

        /// <summary>
        /// Accesseurs de l'arbre en lecture
        /// </summary>
        /// <returns></returns>
        public Arbre GetArbre()
        {
            return graphe;
        }

        /// <summary>
        /// Acceseur de Adresse en lecture et écriture
        /// </summary>
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        /// <summary>
        /// Accesseur de mail en lecture et en écriture
        /// </summary>
        public string Mail
    {
        get { return mail; }
        set { mail = value; }
    }

        /// <summary>
        /// Accesseur de telephone en lecture et en écriture
        /// </summary>
        public int Telephone
    {
        get { return telephone; }
        set { telephone = value; }
    }

        /// <summary>
        /// Accesseur des salariés en lecture et en écriture
        /// </summary>
        public List<Salarie> Salaries
    {
        get { return salaries; }
        set { salaries = value; }
    }

        /// <summary>
        /// Accesseur du patron en lecture et en écriture
        /// </summary>

        public Salarie Patron
    {
        get { return patron; }
        set { patron = value; }
    }

        /// <summary>
        /// Accesseur des clients en lecture et en écriture
        /// </summary>
        public List<Client> Clients
    {
        get { return clients; }
        set { clients = value; }
    }

        /// <summary>
        /// Accesseur des véhicules en lecture et en écriture
        /// </summary>
        public List<Vehicule> Vehicules
    {
        get { return vehicules; }
        set { vehicules = value; }
    }


        /// <summary>
        /// Accesseur des commandes en lecture et en écriture
        /// </summary>
        public List<Commande> Commandes
    {
        get { return commandes; }
        set { commandes = value; }
    }

    #endregion 

    /// <summary>
    /// Constructeur de l'objet Entreprise
    /// </summary>
    /// <param name="nom"></param>
    /// <param name="adresse"></param>
    /// <param name="mail"></param>
    /// <param name="telephone"></param>
    /// <param name="patron"></param>
    public Entreprise(string nom, string adresse, string mail, int telephone, Salarie patron)
    {
        this.nom = nom;
        this.adresse = adresse;
        this.mail = mail;
        this.telephone = telephone;
        this.salaries = new List<Salarie>();        //Salariés, clients, véhicules et commandes sont initialisés vides
            this.clients = new List<Client>();
        this.vehicules = new List<Vehicule>();
        this.commandes = new List<Commande>();
        this.patron = patron;
        this.graphe = new Arbre();
        graphe.InitiateGraphe();                //On initialise le graphe
        }

        /// <summary>
        /// Constructeur de l'objet entreprise à partir d'un fichier csv
        /// </summary>
        /// <param name="path"></param>
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
        this.commandes = new List<Commande>();
        this.graphe = new Arbre();
        graphe.InitiateGraphe();
        }



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

        /// <summary>
        /// Affichage de l'entreprise
        /// </summary>
        /// <returns></returns>
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
        if (liste == null) liste = salaries;    //Possibilités de renseigner une liste plus restreinte (utile dans d'autres méthodes)
        foreach (Salarie s in liste)            //On fais de même pour les prochains affichges de liste
        {
            Console.WriteLine(s.ToString() + "\n");
        }
    }

    /// <summary>
    /// Afficher la liste des clients de l'entreprise
    /// </summary>
    public void AfficherClient(List<Client> liste = null, Comparison<Client> compare = null)
    {
        if (liste == null) liste = clients;         //Possibilité de renseigner une liste plus restreinte
        if (compare!= null) liste.Sort(compare);    //Possibilité de trier la liste selon un Comparison saisie en paramètre
        foreach (Client c in liste)
        {
            Console.Write(c.ToString() + "\n");
            Console.WriteLine("Montant total des achats : " + AchatCumules(c) + "\n\n");
        }
    }

        /// <summary>
        /// Renvoie le montant des achats cumulés d'un client
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
    public double AchatCumules(Client c)
    {
        if (commandes.FindAll(com => com.Client == c).Count == 0) return 0;
        else
        {
            return commandes.FindAll(com => com.Client == c).Sum(com => com.Prix);
        }
    }

        /// <summary>
        /// Afficher la liste des véhicules de l'entreprise
        /// </summary>
        /// <param name="liste"></param>

        public void AfficherVehicule(List<Vehicule> liste = null)
    {
        if (liste == null) liste = vehicules;
        foreach (Vehicule v in liste)
        {
            Console.WriteLine(v.ToString() + "\n");
        }
    }

    /// <summary>
    /// Afficher la liste des commandes de l'entreprise
    /// </summary>
    /// <param name="liste"></param>

    public void AfficherCommande(List<Commande> liste = null)
    {
        if (liste == null) liste = commandes;
        foreach (Commande c in liste)
        {
            Console.WriteLine(c.ToString() + "\n");
        }
    }

    #endregion

    #region Créer/Modifier/Supprimer Salarie

    /// <summary>
    /// Créer un salarié à partir de saisie utilisateur
    /// </summary>
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
            infHier.Add(FindSalarie("Choisissez l'inférieur hierachique : ", sup.InferieurHierachique));    //Un salarié a toujours un sup hierachique
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
        public void SupprimerSalarie()
        {
            Salarie salarie = FindSalarie("Quel salarié voulez-vous licencier ? ");
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


        /// <summary>
        /// Permet à l'utilisateur de selectionner un salarié parmi ceux proposés
        /// </summary>
        /// <param name="text"></param>
        /// <param name="possibilites"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Modifier les paramètres d'un salarié
        /// </summary>
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

    #region Créer/Modifier/SupprimerClient

    /// <summary>
    /// Créer un client à partir de saisie utilisateur
    /// </summary>
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

        /// <summary>
        /// Supprimer un client de l'entreprise
        /// </summary>
        public void SupprimerClient()
    {
        Client c = FindClient("Quelle client voulez-vous supprimer : ");
        clients.Remove(c);
        //On supprime les commandes associées à ce client
        commandes.FindAll(commande => commande.Client == c).ForEach(commande => DeleteCommande(commande));
    }

        /// <summary>
        /// Modifier les paramètres d'un client
        /// </summary>
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
            Console.WriteLine("Informations sur le client :\n");
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

    /// <summary>
    /// Permet de selectionner un client
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Client FindClient(string text)
    {
        bool find = false;
        Console.WriteLine("Liste des clients possibles : \n");
        AfficherClient();
        Console.WriteLine("\nVoici la liste des clients possibles :\n");
        Console.WriteLine("\n\n" + text);
        Predicate<string> IsIDinClients = new Predicate<string>(x => clients.Exists(c => c.Id == int.Parse(x)));
        int inputID = int.Parse(Tools.Saisie("Entrez l'ID du client parmi ceux proposés : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'ID n'est pas valide" }, { IsIDinClients, "Cet ID n'est pas dans les choix possibles" } }));
        Console.Clear();
        return clients.Find(x => x.Id == inputID);
    }
    #endregion

    #region Créer/Supprimer Vehicule

    /// <summary>
    /// Créer un véhciule à partir de saisie utilisateur
    /// </summary>
    public void CreerVehicule()
    {
        Console.WriteLine("Quelle est le tye du véhicule :\nVoiture (Tapez 1)\nCamionette(Tapez 2)\nCamion Benne (Tapez 3)\nCamion citerne(Tapez 4)\nCamion frigorifique(Tapez 5)\n\n");
        int type = int.Parse(Tools.Saisie("Entrez le numéro correspondant au type de véhicule : ", new Dictionary<Predicate<string>, string> { { x => int.Parse(x) >= 1 && int.Parse(x) <= 5, "Le numéro n'est pas valide" } }));
        Console.Clear();

        string marque = Tools.Saisie("Entrez la marque du véhicule : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "La marque ne peut pas être vide" } });
        string modele = Tools.Saisie("Entrez le modèle du véhicule : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le modèle ne peut pas être vide" } });
        string immatriculation = Tools.Saisie("Entrez le numéro d'immatriculation du véhicule : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le numéro d'immatriculation ne peut pas être vide" } });
        int annee = int.Parse(Tools.Saisie("Entrez l'année de mise en circulation du véhicule : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'année n'est pas valide" }, { x => int.Parse(x) <= DateTime.Now.Year, "L'année ne peut pas être dans le futur" } }));
        double prix = double.Parse(Tools.Saisie("Entrez le prix de location du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le prix n'est pas valide" }, { IsPositive, "Le prix ne peut pas être négatif" } }));
        switch (type)
        {
            case 1:
                int nbplaces = int.Parse(Tools.Saisie("Entrez le nombre de places du véhicule : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le nombre de places n'est pas valide" }, { IsPositive, "Le nombre de places ne peut pas être négatif" } }));
                vehicules.Add(new Voiture(immatriculation, marque, modele, annee, prix, nbplaces));
                break;
            case 2:
                double volume = double.Parse(Tools.Saisie("Entrez le volume du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le volume n'est pas valide" }, { IsPositive, "Le volume ne peut pas être négatif" } }));
                string usage = Tools.Saisie("Entrez l'usage du véhicule : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "L'usage ne peut pas être vide" } });
                vehicules.Add(new Camionette(immatriculation, marque, modele, annee, prix, volume, usage));
                break;
            case 3:
                double poids = double.Parse(Tools.Saisie("Entrez le poids du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le poids n'est pas valide" }, { IsPositive, "Le poids ne peut pas être négatif" } }));
                double volume1 = double.Parse(Tools.Saisie("Entrez le volume du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le volume n'est pas valide" }, { IsPositive, "Le volume ne peut pas être négatif" } }));
                int nbBenne = int.Parse(Tools.Saisie("Entrez le nombre de bennes du véhicule : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le nombre de bennes n'est pas valide" }, { IsPositive, "Le nombre de bennes ne peut pas être négatif" } }));
                bool grue = Tools.Saisie("Le camion benne est-il équipé d'une grue ? (Tapez 'oui' ou 'non' en miniscules) : ", new Dictionary<Predicate<string>, string> { { IsBool, "La réponse n'est pas valide" } }) == "oui";
                vehicules.Add(new CamionBenne(immatriculation, marque, modele, annee, prix, poids, volume1, nbBenne, grue));
                break;
            case 4:
                double poids1 = double.Parse(Tools.Saisie("Entrez le poids du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le poids n'est pas valide" }, { IsPositive, "Le poids ne peut pas être négatif" } }));
                double volume2 = double.Parse(Tools.Saisie("Entrez le volume du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le volume n'est pas valide" }, { IsPositive, "Le volume ne peut pas être négatif" } }));
                string typeCuve = Tools.Saisie("Entrez le type de cuve du véhicule : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le type de cuve ne peut pas être vide" } });
                vehicules.Add(new CamionCiterne(immatriculation, marque, modele, annee, prix, poids1, volume2, typeCuve));
                break;
            case 5:
                double poids2 = double.Parse(Tools.Saisie("Entrez le poids du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le poids n'est pas valide" }, { IsPositive, "Le poids ne peut pas être négatif" } }));
                double volume3 = double.Parse(Tools.Saisie("Entrez le volume du véhicule : ", new Dictionary<Predicate<string>, string> { { IsDouble, "Le volume n'est pas valide" }, { IsPositive, "Le volume ne peut pas être négatif" } }));
                int nbGrpElectrogene = int.Parse(Tools.Saisie("Entrez le nombre de groupes électrogènes du véhicule : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le nombre de groupes électrogènes n'est pas valide" }, { IsPositive, "Le nombre de groupes électrogènes ne peut pas être négatif" } }));
                vehicules.Add(new CamionFrigorifique(immatriculation, marque, modele, annee, prix, poids2, volume3, nbGrpElectrogene));
                break;
        }
    }

    /// <summary>
    /// Peermet de selectionner un vehicule parmi ceux proposés
    /// </summary>
    /// <param name="text"></param>
    /// <param name="possibilites"></param>
    /// <returns></returns>
    public Vehicule FindVehicule(string text, List<Vehicule> possibilites = null)
    {
        if (possibilites == null) possibilites = vehicules;
        Console.WriteLine("Liste des véhicules possibles : \n");
        AfficherVehicule(possibilites);
        Console.WriteLine("\nVoici la liste des véhicules possibles :\n");
        Console.WriteLine("\n\n" + text);
        Predicate<string> IsImmatInVehicules = new Predicate<string>(x => possibilites.Exists(c => c.Immatriculation == x));
        string immat = Tools.Saisie("Entrez le numéro d'immatriculation du véhicule parmi ceux proposés : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le numéro d'immatriculation ne peut pas être vide" }, { IsImmatInVehicules, "Cet numéro d'immatriculation n'est pas dans les choix possibles" } });
        Console.Clear();
        return vehicules.Find(x=> x.Immatriculation == immat);
    }

        /// <summary>
        /// Supprimer un vehciule de l'entreprise
        /// </summary>

        public void SupprimerVehicule()
        {
            Vehicule v = FindVehicule("Quelle vehicule voulez-vous supprimer : ");
            vehicules.Remove(v);
            //Supprimer les commandes associées au véhicule
            commandes.FindAll(commande => commande.Vehicule == v).ForEach(commande => DeleteCommande(commande));
        }

        #endregion

    #region Créer/Supprimer Commande
    /// <summary>
    /// Créer une commande à partir de saisie utilisateur
    /// </summary>
        public void CreerCommande()
    {
        Console.WriteLine("Quel client a commandé : \n");
        Client client = FindClient("Quelle client a effectué la commande : ");

            Console.WriteLine("Voici la liste des villes disponibles:\n");
            foreach (Node n in graphe.nodes)
            {
                Console.WriteLine(n.Name);
            }

            Console.WriteLine("\n\n");
            Predicate<string> IsInGraph = new Predicate<string>(x => graphe.nodes.Exists(n => n.Name == x));
            string depart = Tools.Saisie("Entrez le lieu de départ de la commande : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le lieu de départ ne peut pas être vide" }, {IsInGraph, "Cette ville ne fais pas partie des villes possibles" } });
            Predicate<string> IsDifferent = new Predicate<string>(x=> x!= depart);
        string arrivee = Tools.Saisie("Entrez le lieu d'arrivée de la commande : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le lieu d'arrivée ne peut pas être vide" }, { IsInGraph, "Cette ville ne fais pas partie des villes possibles" }, {IsDifferent, "La ville d'arrivée doit être différente de celle de départ" } });
        //Fin a modif

        DateTime date = DateTime.Parse(Tools.Saisie("Entrez la date de la commande : ", new Dictionary<Predicate<string>, string> { { IsDate, "La date n'est pas valide" }}));

        string desscription = Tools.Saisie("Entrez la description de la commande : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "La description ne peut pas être vide" } });

        List<Salarie> ChauffeurPossible = salaries.FindAll(s =>
        {
            if (s is Chauffeur c) return c.IsDispo(date);
            else return false;
        });
        Chauffeur chauffeur = (Chauffeur)FindSalarie("Quel chauffeur va effectué la commande parmi ceux disponible le " + date.ToString("dd/MM/yyyy") + " : ", ChauffeurPossible);


        List<Vehicule> VehiculePossible = vehicules.FindAll(v => v.IsDispo(date));
        Vehicule vehicule = FindVehicule("Quel véhicule va effectué la commande parmi ceux disponible le " + date.ToString("dd/MM/yyyy") + " : ", VehiculePossible);

        Commande commande = new Commande(this, client, chauffeur, vehicule, depart, arrivee, date, -1, desscription);

        commandes.Add(commande);

    }

        /// <summary>
        /// Selectionner une commande parmi celles proposées
        /// </summary>
        /// <param name="text"></param>
        /// <param name="possibilites"></param>
        /// <returns></returns>
        public Commande FindCommande(string text, List<Commande> possibilites = null)
    {
        if (possibilites == null) possibilites = commandes;
        Console.WriteLine("Liste des commandes possibles : \n");
        AfficherCommande(possibilites);
        Console.WriteLine("\nVoici la liste des commandes possibles :\n");
        Console.WriteLine("\n\n" + text);
        Predicate<string> IsIdInCommande = new Predicate<string>(x => possibilites.Exists(c => c.Id == int.Parse(x)));
        int id = int.Parse(Tools.Saisie("Entrez l'ID de la commande parmi ceux proposés : ", new Dictionary<Predicate<string>, string> { { IsInt, "L'ID n'est pas valide" }, { IsIdInCommande, "Cet ID n'est pas dans les choix possibles" } }));
        Console.Clear();
        return commandes.Find(x => x.Id == id);
    }

        //Supprimer une commande parmi celles proposées
        public void SupprimerCommande()
    {
        Commande c = FindCommande("Quelle commande voulez-vous supprimer : ");
        DeleteCommande(c);
    }

        /// <summary>
        /// Supprimer une commande dans les objets concernés
        /// </summary>
        /// <param name="c"></param>
        public void DeleteCommande(Commande c)
    {
        commandes.Remove(c);
        c.Chauffeur.EmploiDuTemps.Remove(c.Date);
        c.Vehicule.EmploiDuTemps.Remove(c.Date);
    }

    #endregion

    #region Statistiques

    /// <summary>
    /// Afficher le nombre de livraison par chauffeur
    /// </summary>
    public void AfficherNblivraisonParChauffeur()
    {
        List<Salarie> chauffeurs = salaries.FindAll(s => s is Chauffeur);
        chauffeurs.Sort((Salarie a, Salarie b) => NbCommandeChauffeur(b).CompareTo(NbCommandeChauffeur(a)));
        foreach(Salarie s in chauffeurs)
        {
            Console.WriteLine(s.ToString() + "Nombres de livraisons : " + NbCommandeChauffeur(s) + "\n\n");
        }
    }

    /// <summary>
    /// Renvoie le nombre de commande effectué par un chauffeur
    /// </summary>
    /// <param name="chauffeur"></param>
    /// <returns></returns>
    public int NbCommandeChauffeur(Salarie chauffeur)
    {
        if (chauffeur is not Chauffeur) return 0;
        else return commandes.Count(c => c.Chauffeur == chauffeur);
    }

    /// <summary>
    /// Afficher les commandes entre 2 dates données
    /// </summary>
    /// <param name="inf"></param>
    /// <param name="sup"></param>
    public void AfficherCommandeEntreDates(DateTime inf, DateTime sup)
    {
        List<Commande> CommmandesAafficher = commandes.FindAll(c => c.Date >= inf && c.Date <= sup);
        AfficherCommande(CommmandesAafficher);
    }

    /// <summary>
    /// Moyenne des prix des commandes
    /// </summary>
    /// <returns></returns>
    public double MoyennePrixCommande()
    {
        return commandes.Average(c => c.Prix);
    }

    /// <summary>
    /// Renvoie la moyenne des comptes clients
    /// </summary>
    /// <returns></returns>

    public double MoyenneCompteClient()
    {
        return clients.Select(client => AchatCumules(client)).Average();
    }

    /// <summary>
    /// Afficher les commandes pour un client donné
    /// </summary>
    /// <param name="c"></param>
    public void AffciherCommandeClient(Client c)
    {
        AfficherCommande(commandes.FindAll(com=>com.Client==c));
    }


        #endregion

    #region Lecture/Ecriture de Fichiers

        /// <summary>
        /// Permet de lire les fichiers de sauvegarde
        /// </summary>
        /// <param name="path"></param>
        public void ReadSauvegarde(string path)
    {
            ReadClient(path + "\\Clients.csv");
            ReadChauffeur(path + "\\Chauffeur.csv");
            ReadSalarie(path + "\\Salaries.csv");
            ReadRelation(path + "\\Relations.csv");
            ReadVehicule(path + "\\Vehicules.csv");
            ReadCommande(path + "\\Commandes.csv");
    }

        /// <summary>
        /// Permet d'écrire les fichiers de sauvegarde
        /// </summary>
        /// <param name="path"></param>
        public void WriteSauvegarde(string path)
        {
            SaveClient(path + "\\Clients.csv");
            SaveChauffeur(path + "\\Chauffeur.csv");
            SaveSalarie(path + "\\Salaries.csv");
            SaveRelation(path + "\\Relations.csv");
            SaveVehicule(path + "\\Vehicules.csv");
            SaveEntreprise(path + "\\Entreprise.csv");
            SaveCommande(path + "\\Commandes.csv");
        }

        /// <summary>
        /// Sauvegarde les informations de l'entreprise
        /// </summary>
        /// <param name="path"></param>
        public void SaveEntreprise(string path)
    {
        List<string> text = new List<string>();
        text.Add(string.Format("{0},{1},{2},{3}", nom, adresse, mail, telephone));
        File.WriteAllLines(path, text);
    }

        /// <summary>
        /// Sauvegarde les informations des salariés
        /// </summary>
        /// <param name="path"></param>
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

        /// <summary>
        /// Sauvegarde les informations des chauffeurs
        /// </summary>
        /// <param name="path"></param>
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
                text.Add(line);
            }
        }
        File.WriteAllLines(path, text);
    }

        /// <summary>
        /// Permet de lire la sauvagarde des salariés
        /// </summary>
        /// <param name="path"></param>
    public void ReadSalarie(string path)
    {
        string[] lignes = File.ReadAllLines(path);
        if (lignes.Length == 0) return;
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

        /// <summary>
        /// Permet de lire la sauvegarde des chauffeurs
        /// </summary>
        /// <param name="path"></param>
    public void ReadChauffeur(string path)
    {
        string[] lignes = File.ReadAllLines(path);
        if (lignes.Length == 0) return;
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

            salaries.Add(chauffeur);
        }
    }

        /// <summary>
        /// Permet de sauvegarder les relations hiérarchiques entre les salariés
        /// </summary>
        /// <param name="path"></param>
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

        /// <summary>
        /// Permet de lire la sauvegarde des relations hiérarchiques
        /// </summary>
        /// <param name="path"></param>
        public void ReadRelation(string path)
    {

        string[] text = File.ReadAllLines(path);
        if (text.Length == 0) return;
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

        /// <summary>
        /// Permet de sauvegarder les clients
        /// </summary>
        /// <param name="path"></param>
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

        /// <summary>
        /// Permet de lire la sauvegarde des clients
        /// </summary>
        /// <param name="path"></param>
    public void ReadClient(string path)
    {
        string[] lignes = File.ReadAllLines(path);
        if (lignes.Length == 0) return;
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

        /// <summary>
        /// Permet de sauvegarder les véhciules
        /// </summary>
        /// <param name="path"></param>
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
            line.Add(vehicule.Prix.ToString());

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
        }

        File.WriteAllLines(path, text);
    }

        /// <summary>
        /// Permet de lire la sauvegarde des véhicules
        /// </summary>
        /// <param name="path"></param>
        public void ReadVehicule(string path)
    {
        string[] lines = File.ReadAllLines(path);
        if (lines.Length == 0) return;
        for (int i = 0; i < lines.Length; i ++)
        {
            Vehicule v = null;
            string[] line = lines[i].Split(',');

            switch (line[0])
            {
                case "Voiture":
                    v = new Voiture(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]),int.Parse(line[6]));
                    break;
                case "Camionette":
                    v = new Camionette(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), line[7]);
                    break;
                case "CamionFrigorifique":
                    v = new CamionFrigorifique(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), double.Parse(line[7]), int.Parse(line[8]));
                    break;
                case "CamionBenne":
                    v = new CamionBenne(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), double.Parse(line[7]), int.Parse(line[8]), bool.Parse(line[9]));
                    break;
                case "CamionCiterne":
                    v = new CamionCiterne(line[1], line[2], line[3], int.Parse(line[4]), double.Parse(line[5]), double.Parse(line[6]), double.Parse(line[7]), line[8]);
                    break;
            }
            vehicules.Add(v);
        }
    }

        /// <summary>
        /// Permet de sauvegarder les commandes
        /// </summary>
        /// <param name="path"></param>
        public void SaveCommande(string path)
    {
        List<string> text = new List<string>();

        foreach(Commande c in commandes)
        {
            List<string> line = new List<string>();
            line.Add(c.Id.ToString());
            line.Add(c.Client.Id.ToString());
            line.Add(c.Chauffeur.Id.ToString());
            line.Add(c.Vehicule.Immatriculation);
            line.Add(c.Depart);
            line.Add(c.Arrivee);
            line.Add(c.Date.ToString("dd/MM/yyyy"));
            line.Add(c.Prix.ToString());
            line.Add(c.Description);

            text.Add(string.Join(",", line));

        }
        File.WriteAllLines(path, text);
    }


        /// <summary>
        /// Permet de lire la sauvegarde des commandes
        /// </summary>
        /// <param name="path"></param>
        public void ReadCommande(string path)
    {
        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0) return;
        foreach (string line in lines)
        {
            string[] elements = line.Split(',');

            int id = int.Parse(elements[0]);
            Client client = clients.Find(x => x.Id == int.Parse(elements[1]));
            Chauffeur chauffeur = (Chauffeur)salaries.Find(x => x.Id == int.Parse(elements[2]));
            Vehicule vehicule = vehicules.Find(x => x.Immatriculation == elements[3]);
            string depart = elements[4];
            string arrivee = elements[5];
            DateTime date = DateTime.Parse(elements[6]);
            double prix = double.Parse(elements[7]);
            string description = elements[8];

            Commande c = new Commande(this, client, chauffeur, vehicule, depart, arrivee, date, prix, description);
            commandes.Add(c);
        }
    }


    #endregion
    }
}
