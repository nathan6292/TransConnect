using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Chauffeur : Salarie
    {
        protected double tarifHoraire;
        protected List<DateTime> emploiDuTemps; //Liste des horaires de livraisons du chauffeur
        //Ajouter Liste Livraison

        #region accesseurs

        /// <summary>
        /// Accesseur en lecture et en écriture du tarif horaire
        /// </summary>
        public double TarifHoraire
        {
            get { return tarifHoraire; }
            set { tarifHoraire = value; }
        }

        /// <summary>
        /// Accesseur en lecture et en écriture de l'emploi du temps
        /// </summary>
        public List<DateTime> EmploiDuTemps
        {
            get { return emploiDuTemps; }
            set { emploiDuTemps = value; }
        }

        #endregion

        /// <summary>
        /// Créer un objet Chauffeur
        /// </summary>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="naissance"></param>
        /// <param name="adresse"></param>
        /// <param name="mail"></param>
        /// <param name="telephone"></param>
        /// <param name="poste"></param>
        /// <param name="salaire"></param>
        /// <param name="dateEmbauche"></param>

        public Chauffeur(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, int telephone, string poste, double salaire, DateTime dateEmbauche, Salarie superieurHierarchique, double tarifHoraire = 0) : base(id, prenom, nom, naissance, adresse, mail, telephone, poste, salaire, dateEmbauche, superieurHierarchique)
        {
            if (tarifHoraire == 0)
            {
                this.tarifHoraire = this.CalculerTarifHoraire();
            }
            else
            {
                this.tarifHoraire = tarifHoraire;
            }
            emploiDuTemps = new List<DateTime>();
        }

        /// <summary>
        /// Calculer le tarif horaire du chauffeur
        /// </summary>
        /// <returns></returns>
        public double CalculerTarifHoraire()
        {
            int anciennete = (DateTime.Now - this.dateEmbauche).Days / 365;

            //Tarif horaire de base
            double tarifHoraire = 15.0;

            if (anciennete >= 1 && anciennete < 5)
            {
                tarifHoraire *= 1.05;
            }
            else if (anciennete >= 5 && anciennete < 10)
            {
                tarifHoraire *= 1.10;
            }
            else if (anciennete >= 10)
            {
                tarifHoraire *= 1.15;
            }

            return tarifHoraire;
        }

        public override string ToString()
        {
            return base.ToString() + "\nTarif horaire : " + tarifHoraire + "\n";
        }

        public string EmploiDuTempsToString()
        {
            string str = "";
            foreach (DateTime date in emploiDuTemps)
            {
                str += date.ToString("dd/MM/yyyy") + "\n";
            }
            return str;
        }
    }
}
