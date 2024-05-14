using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;



using System.Net.Mail;

namespace Projet_TransConnect
{
    public class Client : Personne
{


    public Client(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, string telephone) : base(id, prenom, nom, naissance, adresse, mail, telephone)
    {

    }

        public double Remise(Entreprise e)
        {
            Comparison<Client> compare = new Comparison<Client>((Client c1, Client c2) => e.AchatCumules(c2).CompareTo(e.AchatCumules(c1)));
            e.Clients.Sort(compare);

            int ClientRank = e.Clients.IndexOf(this);

            if (ClientRank <= 3) return 0.3;
            else if (ClientRank <= 5) return 0.2;
            else if (ClientRank <= 10) return 0.1;
            else return 0;
        }


    }
}