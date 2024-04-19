using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public abstract class Vehicule
    {
        private static List<string> ListImmat = new List<string>();
        protected string immatriculation;
        protected string marque;
        protected string modele;
        protected int annee;
        protected List<DateTime> emploiDuTemps;     

        #region accesseurs

        public string Immatriculation
        {
            get { return immatriculation; }
        }

        public string Marque
        {
            get { return marque; }
        }

        public string Modele
        {
            get { return modele; }
        }

        public int Annee
        {
            get { return annee; }
        }

        public List<DateTime> EmploiDuTemps
        {
            get { return emploiDuTemps; }
            set { emploiDuTemps = value; }
        }

        #endregion

        public Vehicule(string immatriculation, string marque, string modele, int annee)
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
            emploiDuTemps = new List<DateTime>();
        }

        
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
            return "Type : " + type + "\nImmatriculation : " + immatriculation + "\nMarque : " + marque + "\nModèle : " + modele + "\nAnnée : " + annee + "\n";
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
