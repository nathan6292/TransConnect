using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Voiture : Vehicule, IsToString
    {
    protected int nbPlaces;

        #region accesseurs

        /// <summary>
        /// Acceseurs en lecture de nbPlaces
        /// </summary>
        public int NbPlaces
    {
        get { return nbPlaces; }
    }

        #endregion

        /// <summary>
        /// Constructeyr de la classe Voiture
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="nbPlaces"></param>
        public Voiture(string immatriculation, string marque, string modele, int annee, double prix,int nbPlaces) : base(immatriculation, marque, modele, annee,prix)
    {
        this.nbPlaces = nbPlaces;
    }

        /// <summary>
        /// Afficher les informations de la voiture
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Nombre de places : " + nbPlaces + "\n";
    }
}
}
