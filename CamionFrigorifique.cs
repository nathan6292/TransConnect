using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect{
    public class CamionFrigorifique : PoidsLourd, IsToString
{
    protected int nbGrpElectrogene;

        #region accesseurs

        /// <summary>
        /// Accesseur en lecture du nombre de groupe électrogène
        /// </summary>
        public int NbGrpElectrogene
    {
        get { return nbGrpElectrogene; }
    }

        #endregion

        /// <summary>
        /// Constructeur de la classe CamionFrigorifique
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="NbGrpElectrogene"></param>
        public CamionFrigorifique(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume, int NbGrpElectrogene) : base(immatriculation, marque, modele, annee, prix, poids, volume)
    {
        this.nbGrpElectrogene = NbGrpElectrogene;
    }

        /// <summary>
        /// Affichage des informations du camion frigorifique
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Nombre de groupe électrogène : " + nbGrpElectrogene + "\n";
    }
}
    
}
