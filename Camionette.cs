using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect{
    public class Camionette : Vehicule
    {
        protected double volume;
        protected string usage;

        #region accesseurs

        public double Volume
        {
            get { return volume; }
        }

        public string Usage
        {
            get { return usage;}
        }

        #endregion

        public Camionette(string immatriculation, string marque, string modele, int annee, double volume, string usage) : base(immatriculation, marque, modele, annee)
        {
            this.volume = volume;
            this.usage = usage;
        }
        public override string ToString()
        {
            return base.ToString() + "Volume : " + volume + "\nUsage : " + usage + "\n";
        }
    }
}
