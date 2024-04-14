using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Client : Personne
    {
        List<Commande> commandes;

        public Client(string prenom, string nom, DateTime naissance, string adresse, string mail, int telephone, List<Commande> commandes = null) : base(prenom, nom, naissance, adresse, mail, telephone)
        {
            if(commandes == null){
                this.commandes = new List<Commande>();
            }
            else{
                this.commandes = commandes;
            }
        }
    }
}