using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public class CamionFrigorifique : PoidsLourd
{
    protected int nbGrpElectrogene;

    #region accesseurs

    public int NbGrpElectrogene
    {
        get { return nbGrpElectrogene; }
    }

    #endregion

    public CamionFrigorifique(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume, int NbGrpElectrogene) : base(immatriculation, marque, modele, annee, prix, poids, volume)
    {
        this.nbGrpElectrogene = NbGrpElectrogene;
    }

    public override string ToString()
    {
        return base.ToString() + "Nombre de groupe électrogène : " + nbGrpElectrogene + "\n";
    }
}
    
}
