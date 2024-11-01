using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect{
    public class Camionette : Vehicule, IsToString
{
    protected double volume;
    protected string usage;

        #region accesseurs

        /// <summary>
        /// Accesseur en lecture de l'attribut volume
        /// </summary>
        public double Volume
    {
        get { return volume; }
    }

        /// <summary>
        /// Accesseur en lecture de l'attribut usage
        /// </summary>
        public string Usage
    {
        get { return usage; }
    }

        #endregion

        /// <summary>
        /// Constructeur de la classe Camionette
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="volume"></param>
        /// <param name="usage"></param>
        public Camionette(string immatriculation, string marque, string modele, int annee, double prix, double volume, string usage) : base(immatriculation, marque, modele, annee, prix)
    {
        this.volume = volume;
        this.usage = usage;
    }

        /// <summary>
        /// Affiche les informations de la camionette
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Volume : " + volume + "\nUsage : " + usage + "\n";
    }
}
}
