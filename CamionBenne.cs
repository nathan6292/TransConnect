using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect{
    public class CamionBenne : PoidsLourd, IsToString
{
    protected int nbBennes;
    protected bool grue;

        #region accesseurs
        /// <summary>
        /// Accesseur en lecture du nombre de bennes
        /// </summary>
        public int NbBennes
    {
        get { return nbBennes; }
    }

        /// <summary>
        /// Accesseur en lecture de la présence d'une grue
        /// </summary>

        public bool Grue
    {
        get { return grue; }
    }

        #endregion


        /// <summary>
        /// Constructeur de la classe CamionBenne
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="nbBennes"></param>
        /// <param name="grue"></param>
        public CamionBenne(string immatriculation, string marque, string modele, int annee, double prix,double poids, double volume, int nbBennes, bool grue) : base(immatriculation, marque, modele, annee, prix,poids, volume)
    {
        this.nbBennes = nbBennes;
        this.grue = grue;
    }

        /// <summary>
        /// Affichage des informations du camion benne
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Nombre de bennes : " + nbBennes + "\nGrue : " + (grue ? "Oui" : "Non") + "\n";
    }
}
}
