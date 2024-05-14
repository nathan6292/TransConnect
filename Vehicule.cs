using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public abstract class Vehicule : IsToString
    {
    private static List<string> ListImmat = new List<string>();
    protected string immatriculation;
    protected string marque;
    protected string modele;
    protected int annee;
    protected double prix;
    protected List<DateTime> emploiDuTemps;

        #region accesseurs

        /// <summary>
        /// Accesseur de l'immatriculation en lecture seule
        /// </summary>
        public string Immatriculation
    {
        get { return immatriculation; }
    }

        /// <summary>
        /// Accesseur de la marque en lecture seule
        /// </summary>
        public string Marque
    {
        get { return marque; }
    }

        /// <summary>
        /// Accesseur du modèle en lecture seule
        /// </summary>
        public string Modele
    {
        get { return modele; }
    }

        /// <summary>
        /// Accesseur de l'année en lecture seule
        /// </summary>

        public int Annee
    {
        get { return annee; }
    }

        /// <summary>
        /// Accesseur de l'emploi du temps en lecture et écriture
        /// </summary>
        public List<DateTime> EmploiDuTemps
    {
        get { return emploiDuTemps; }
        set { emploiDuTemps = value; }
    }

        /// <summary>
        /// Accesseur du prix en lecture et écriture
        ///        /// </summary>
        /// </summary>
        public double Prix
    {
        get { return prix; }
        set { prix = value; }
    }

        #endregion

        /// <summary>
        /// Constructeur de la classe Vehicule
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        public Vehicule(string immatriculation, string marque, string modele, int annee, double prix)
    {
        if (ListImmat.Contains(immatriculation))
        {
            ListImmat.Add(immatriculation);
            immatriculation = immatriculation + "(" + ListImmat.Count(i => i == immatriculation) + ")";
        }
        else
        {
            ListImmat.Add(immatriculation);
        }
        this.immatriculation = immatriculation;
        this.marque = marque;
        this.modele = modele;
        this.annee = annee;
        this.prix = prix;
        emploiDuTemps = new List<DateTime>();
    }

        /// <summary>
        /// Afficher les informations du véhicule
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        string type="";
        switch (this.GetType().Name)
        {
            case "Voiture":
                type = "Voiture";
                break;
            case "Camionette":
                type = "Camionette";
                break;
            case "CamionCiterne":
                type = "Camion Citerne";
                break;
            case "CamionBenne":
                type = "Camion Benne";
                break;
            case "CamionFrigorifique":
                type = "Camion Frigorifique";
                break;
        }
        return "Type : " + type + "\nImmatriculation : " + immatriculation + "\nMarque : " + marque + "\nModèle : " + modele + "\nAnnée : " + annee + "\nPrix : " + prix + "\n";
    }

        /// <summary>
        /// Afficher l'emploi du temps du véhicule
        /// </summary>
        /// <returns></returns>
        public string EmploiDuTempsToString()
    {
        string str = "";
        foreach (DateTime date in emploiDuTemps)
        {
            str += date.ToString("dd/MM/yyyy") + "\n";
        }
        return str;
    }

        /// <summary>
        /// Renvoie si le vehicule est dispo à une date donnée
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
    public bool IsDispo(DateTime date)
    {
        return !emploiDuTemps.Contains(date);
    }
}
}
