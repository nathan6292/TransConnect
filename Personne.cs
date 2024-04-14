using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Personne
    {
        private static int ProchainID = 1;     //Permet de générer  atomatiquement un ID, qui s'incrémente à chaque création d'un objet (donc est unique)
        
        protected int id;
        protected string prenom;
        protected string nom;
        protected DateTime naissance;
        protected string adresse;
        protected string mail;
        protected int telephone;

        #region accesseurs
        /// <summary>
        /// Accesseur en lecture seule de l'ID
        /// </summary>
        public int Id
        {
            get { return id; }         
        }

        /// <summary>
        /// Accesseurs en lecture seule du prénom
        /// </summary>

        public string Prenom
        {
            get { return prenom; }
        }

        /// <summary>
        /// Accesseurs en lecture seule du nom
        /// </summary>
        public string Nom
        {
            get { return nom; }
        }

        /// <summary>
        /// Accesseurs en lecture seule de la date de naissance
        /// </summary>
        public DateTime Naissance
        {
            get { return naissance; }
        }

        /// <summary>
        /// Accesseur en lecture et en écriture de l'adresse
        /// </summary>
        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        /// <summary>
        /// Accesseurs en lecture et en écriture du mail
        /// </summary>
        public string Mail
        {
            get { return mail; }
            set { mail = value; }
        }

        /// <summary>
        /// Accesseurs en lecture et en écriture du téléphone
        /// </summary>
        public int Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        #endregion

        /// <summary>
        /// Génerer un objet Personne
        /// </summary>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="naissance"></param>
        /// <param name="adresse"></param>
        /// <param name="mail"></param>
        /// <param name="telephone"></param>
        public Personne(string prenom, string nom, DateTime naissance, string adresse, string mail, int telephone)
        {
            this.id = ProchainID;
            ProchainID++;                       //Incrémente l'ID pour le prochain objet pour éviter les doublons

            this.prenom = prenom;
            this.nom = nom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.telephone = telephone;
        }



        /// <summary>
        /// Affiche les informations de la personne
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return "ID : " + id + "\nPrénom : " + prenom + "\nNom : " + nom + "\nDate de naissance : " + naissance + "\nAdresse : " + adresse + "\nMail : " + mail + "\nTéléphone : " + telephone;
        }

    }
}
