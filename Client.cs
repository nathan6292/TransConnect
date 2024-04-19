using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Client : Personne
    {
         

        public Client(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, long telephone) : base(id, prenom, nom, naissance, adresse, mail, telephone)
        {
            
        }

        

    }
}