using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public abstract class PoidsLourd : Vehicule
{
    protected double poids;
    protected double volume;

    #region accesseurs

    public double Poids
    {
        get { return poids; }
    }

    public double Volume
    {
        get { return volume; }
    }

    #endregion

    public PoidsLourd(string immatriculation, string marque, string modele, int annee, double prix, double poids, double volume) : base(immatriculation, marque, modele, annee, prix)
    {
        this.poids = poids;
        this.volume = volume;
    }

    public override string ToString()
    {
        return base.ToString() + "Poids : " + poids + "\nVolume : " + volume + "\n";
    }
}
}
