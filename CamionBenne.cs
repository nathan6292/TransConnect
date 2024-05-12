using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public class CamionBenne : PoidsLourd
{
    protected int nbBennes;
    protected bool grue;

    #region accesseurs

    public int NbBennes
    {
        get { return nbBennes; }
    }

    public bool Grue
    {
        get { return grue; }
    }

    #endregion

    public CamionBenne(string immatriculation, string marque, string modele, int annee, double prix,double poids, double volume, int nbBennes, bool grue) : base(immatriculation, marque, modele, annee, prix,poids, volume)
    {
        this.nbBennes = nbBennes;
        this.grue = grue;
    }

    public override string ToString()
    {
        return base.ToString() + "Nombre de bennes : " + nbBennes + "\nGrue : " + (grue ? "Oui" : "Non") + "\n";
    }
}
}
