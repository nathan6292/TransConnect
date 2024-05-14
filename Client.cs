using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;



using System.Net.Mail;

namespace Projet_TransConnect
{
    public class Client : Personne, IsToString
{

        /// <summary>
        /// Constructeur de la classe Client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prenom"></param>
        /// <param name="nom"></param>
        /// <param name="naissance"></param>
        /// <param name="adresse"></param>
        /// <param name="mail"></param>
        /// <param name="telephone"></param>
        public Client(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, string telephone) : base(id, prenom, nom, naissance, adresse, mail, telephone)
    {

    }
    /// <summary>
    /// Renvoie le taux de reduction du client
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public double Remise(Entreprise e)
        {
            Comparison<Client> compare = new Comparison<Client>((Client c1, Client c2) => e.AchatCumules(c2).CompareTo(e.AchatCumules(c1)));
            e.Clients.Sort(compare);    //On classe les clients par montant d'achat cumulé

            int ClientRank = e.Clients.IndexOf(this);   //On recupère le rang du client dans la liste triée

            if (ClientRank <= 3) return 0.3;        //On renvoie le taux de réduction en fonction du rang
            else if (ClientRank <= 5) return 0.2;   //30% pour le Top 3, 20% pour le Top 5, 10% pour le Top 10
            else if (ClientRank <= 10) return 0.1;
            else return 0;              //Hors du top 10 : 0%
        }


    }
}