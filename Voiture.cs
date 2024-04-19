using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public class Voiture : Vehicule
    {
        protected int nbPlaces;

        #region accesseurs

        public int NbPlaces
        {
            get { return nbPlaces; }
        }

        #endregion

        public Voiture(string immatriculation, string marque, string modele, int annee, int nbPlaces) : base(immatriculation, marque, modele, annee)
        {
            this.nbPlaces = nbPlaces;
        }

        public override string ToString()
        {
            return base.ToString() + "Nombre de places : " + nbPlaces + "\n";
        }
    }
}
