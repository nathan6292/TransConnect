using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Personne : IsToString
    {
    private List<int> IDexisant = new List<int>();     //Permet de générer  atomatiquement un ID, qui s'incrémente à chaque création d'un objet (donc est unique)

    protected int id;
    protected string prenom;
    protected string nom;
    protected DateTime naissance;
    protected string adresse;
    protected string mail;
    protected string telephone;

    #region accesseurs
    /// <summary>
    /// Accesseur en lecture seule de l'ID
    /// </summary>
    public int Id
    {
        get { return id; }
    }

    /// <summary>
    /// Accesseurs en lecture seule du prénom
    /// </summary>

    public string Prenom
    {
        get { return prenom; }
    }

    /// <summary>
    /// Accesseurs en lecture seule du nom
    /// </summary>
    public string Nom
    {
        get { return nom; }

    }

    /// <summary>
    /// Accesseurs en lecture seule de la date de naissance
    /// </summary>
    public DateTime Naissance
    {
        get { return naissance; }
    }

    /// <summary>
    /// Accesseur en lecture et en écriture de l'adresse
    /// </summary>
    public string Adresse
    {
        get { return adresse; }
        set { adresse = value; }
    }

    /// <summary>
    /// Accesseurs en lecture et en écriture du mail
    /// </summary>
    public string Mail
    {
        get { return mail; }
        set { mail = value; }
    }

    /// <summary>
    /// Accesseurs en lecture et en écriture du téléphone
    /// </summary>
    public string Telephone
    {
        get { return telephone; }
        set { telephone = value; }
    }

    #endregion

    /// <summary>
    /// Génerer un objet Personne
    /// </summary>
    /// <param name="prenom"></param>
    /// <param name="nom"></param>
    /// <param name="naissance"></param>
    /// <param name="adresse"></param>
    /// <param name="mail"></param>
    /// <param name="telephone"></param>
    public Personne(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, string telephone)
    {
        if (id == 0)
        {
            bool valid = false;
            while (!valid)
            {
                Random rnd = new Random();
                id = rnd.Next(0, 1000000);
                if (!IDexisant.Contains(id))
                {
                    IDexisant.Add(id);
                    this.id = id;
                    valid = true;
                }
            }
        }
        else
        {
            this.id = id;
            IDexisant.Add(id);
        }

        this.prenom = prenom;
        this.nom = nom;
        this.naissance = naissance;
        this.adresse = adresse;
        this.mail = mail;
        this.telephone = telephone;
    }



    /// <summary>
    /// Affiche les informations de la personne
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "ID : " + id + "\nPrénom : " + prenom + "\nNom : " + nom + "\nDate de naissance : " + naissance.ToString("dd/MM/yyyy") + "\nAdresse : " + adresse + "\nMail : " + mail + "\nTéléphone : " + telephone;
    }

}
}
