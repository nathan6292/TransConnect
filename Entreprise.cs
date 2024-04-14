﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Entreprise
    {
        protected string nom;
        protected string adresse;
        protected string mail;
        protected int telephone;
        protected List<Salarie> salaries;
        Salarie patron;                           //On considère que le patron est de la classe employé (pour faciliter l'orgranigramme)
        //protected List<Commande> commandes;
        //protected List<Client> clients;
        //protected List<Vehicule> vehicules;

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

        #endregion 
        //A enlever peut etre ??

        public Entreprise(string nom, string adresse, string mail, int telephone, Salarie patron)
        {
            this.nom = nom;
            this.adresse = adresse;
            this.mail = mail;
            this.telephone = telephone;
            salaries = new List<Salarie>();
            this.patron = patron;
          //commandes = new List<Commande>();
          //clients = new List<Client>();
          //vehicules = new List<Vehicule>();
        }

        /// <summary>
        /// Ajouter un salarié à l'entreprise
        /// </summary>
        /// <param name="salarie"></param>

        Predicate<string> IsDouble = new Predicate<string>(x => double.TryParse(x, out _)); 
        Predicate<string> IsInt = new Predicate<string>(x => int.TryParse(x, out _)); 
        Predicate<string> IsDate = new Predicate<string>(x => DateTime.TryParse(x, out _)); 
        Predicate<string> IsMail = new Predicate<string>(x => x.Contains("@")); 
        Predicate<string> IsNotEmpty = new Predicate<string>(x => x.Length > 0); 
        Predicate<string> IsPositive = new Predicate<string>(x =>
        {
            if (double.TryParse(x, out double result))
            {
                return result > 0;
            }
            else
            {
                return false;
            }
        });
        public void AjouterSalarie(Salarie salarie)
        {
            salaries.Add(salarie);
        }

        /// <summary>
        /// Supprimer/Licencier un salarié de l'entreprise
        /// </summary>
        /// <param name="salarie"></param>
        public void SupprimerSalarie(Salarie salarie)
        {
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
                    Organigramme(sal, tab+1);
                    Console.WriteLine("");
                }
            }
        }

        public void AfficherSalarie()
        {
            foreach (Salarie s in salaries)
            {
                Console.WriteLine(s.ToString() + "\n");
            }
        }
        public Salarie FindSalarie()
        {

            bool find = false;
            Console.WriteLine("Liste des salariés : \n");
            this.AfficherSalarie();
            Console.WriteLine("\nVoici la liste des salariés\n");
            while (!find)
            {
                string nom = Tools.Saisie("Entrez le nom du salarié que vous cherchez : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le nom ne peut pas être vide" } });
                List<Salarie> resultat = this.Salaries.FindAll(x => x.Nom == nom);
                switch (resultat.Count)
                {
                    case 0:
                        Console.WriteLine("Aucun salarié trouvé\n\n");
                        break;
                    case 1:
                        return resultat[0];
                    default:
                        Console.WriteLine("\n\nPlusieurs salariés ont le nom " + nom);
                        string prenom = Tools.Saisie("Entrez le prénom du salarié : ", new Dictionary<Predicate<string>, string> { { IsNotEmpty, "Le nom ne peut pas être vide" } });
                        resultat = resultat.FindAll(x => x.Prenom == prenom);
                        switch (resultat.Count)
                        {
                            case 0:
                                Console.WriteLine("\nAucun salarié trouvé\n");
                                break;
                            case 1:
                                return resultat[0];
                        }
                break;
                }

            }
            Console.Clear();
            return null;
        }

        public void CreerSalarie(Entreprise entreprise)
        {
            string prenom = Tools.Saisie("Entrez le prénom du salarié : ", new Dictionary<Predicate<string>,string> { { IsNotEmpty, "Le prénom ne peut pas être vide" } });
            string nom = Tools.Saisie("Entrez le nom du salarié : ", new Dictionary<Predicate<string>,string> { { IsNotEmpty, "Le nom ne peut pas être vide" } });
            DateTime naissance = DateTime.Parse(Tools.Saisie("Entrez la date de naissance du salarié : ", new Dictionary<Predicate<string>,string> { { IsDate, "La date n'est pas valide" } }));
            string adresse = Tools.Saisie("Entrez l'adresse du salarié : ", new Dictionary<Predicate<string>,string> { { IsNotEmpty, "L'adresse ne peut pas être vide" } });
            string mail = Tools.Saisie("Entrez l'adresse mail du salarié : ", new Dictionary<Predicate<string>,string> { { IsMail, "L'adresse mail n'est pas valide" } });
            int telephone = int.Parse(Tools.Saisie("Entrez le numéro de téléphone du salarié : ", new Dictionary<Predicate<string>, string> { { IsInt, "Le numéro de téléphone n'est pas valide" }, { x => x.Length == 10, "Le numéro doit contenir 10 chiffres" } })) ;
            string poste = Tools.Saisie("Entrez le poste du salarié : ", new Dictionary<Predicate<string>,string> { { IsNotEmpty, "Le poste ne peut pas être vide" } });
            double salaire = double.Parse(Tools.Saisie("Entrez le salaire du salarié : ", new Dictionary<Predicate<string>,string> { { IsDouble, "Le salaire n'est pas valide" }, { IsPositive, "Le salaire ne peut pas être négatif" } }));
            DateTime dateEmbauche = DateTime.Parse(Tools.Saisie("Entrez la date d'embauche du salarié : ", new Dictionary<Predicate<string>,string> { { IsDate, "La date n'est pas valide" } }));
            Salarie superieurHierarchique = entreprise.Salaries[0];       //A modifier pour choisir le supérieur hiérarchique
            //Modifier les inféieurs hierachiques
            Salarie salarie = new Salarie(prenom, nom, naissance, adresse, mail, telephone, poste, salaire,dateEmbauche, null);

            entreprise.AjouterSalarie(salarie);
        }
    }
}
