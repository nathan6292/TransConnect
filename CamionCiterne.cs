using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public class CamionCiterne : PoidsLourd
{
    protected string typeCuve;

    #region accesseurs

    public string TypeCuve
    {
        get { return typeCuve; }
    }
    #endregion

    public CamionCiterne(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume, string typeCuve) : base(immatriculation, marque, modele, annee, prix, poids, volume)
    {
        this.typeCuve = typeCuve;
    }

    public override string ToString()
    {
        return base.ToString() + "Type de cuve : " + typeCuve + "\n";
    }
}
}
