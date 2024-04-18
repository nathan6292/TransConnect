using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public abstract class Vehicule
    {
        //Test
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
            this.immatriculation = immatriculation;
            this.marque = marque;
            this.modele = modele;
            this.annee = annee;
            emploiDuTemps = new List<DateTime>();
        }
    }
}
