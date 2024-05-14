using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect{
    public abstract class PoidsLourd : Vehicule, IsToString
{
    protected double poids;
    protected double volume;

        #region accesseurs

        /// <summary>
        /// Accesseur du poids en lecture seule
        /// </summary>
        public double Poids
    {
        get { return poids; }
    }

        /// <summary>
        /// Accesseur du volume en lecture seule
        /// </summary>
        public double Volume
    {
        get { return volume; }
    }

        #endregion

        /// <summary>
        /// Constructeur de la classe PoidsLourd
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        public PoidsLourd(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume) : base(immatriculation, marque, modele, annee, prix)
    {
        this.poids = poids;
        this.volume = volume;
    }

        /// <summary>
        /// Affichage des informations du poids lourd
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Poids : " + poids + "\nVolume : " + volume + "\n";
    }
}
}
