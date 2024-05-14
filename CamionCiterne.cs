using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect{
    public class CamionCiterne : PoidsLourd, IsToString
{
    protected string typeCuve;

        #region accesseurs
        /// <summary>
        /// Accesseur de l'attribut typeCuve en lecture seule
        /// </summary>
        public string TypeCuve
    {
        get { return typeCuve; }
    }
        #endregion

        /// <summary>
        /// Constructeur de la classe CamionCiterne
        /// </summary>
        /// <param name="immatriculation"></param>
        /// <param name="marque"></param>
        /// <param name="modele"></param>
        /// <param name="annee"></param>
        /// <param name="prix"></param>
        /// <param name="poids"></param>
        /// <param name="volume"></param>
        /// <param name="typeCuve"></param>
        public CamionCiterne(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume, string typeCuve) : base(immatriculation, marque, modele, annee, prix, poids, volume)
    {
        this.typeCuve = typeCuve;
    }

        /// <summary>
        /// Affiche les informations du camion citerne
        /// </summary>
        /// <returns></returns>
        public override string ToString()
    {
        return base.ToString() + "Type de cuve : " + typeCuve + "\n";
    }
}
}
