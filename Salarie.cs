using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Salarie : Personne
{
    protected string poste;
    protected double salaire;
    protected DateTime dateEmbauche;
    protected List<Salarie> inferieurHierachique;             //Correspond aux inférieurs hiérarchiques de l'employé
    protected Salarie superieurHierarchique;                   //Correspond au supérieur hiérarchique de l'employé

    #region accesseurs

    /// <summary>
    /// Accesseur en lecture et en écriture du poste
    /// </summary>
    public string Poste
    {
        get { return poste; }
        set { poste = value; }
    }
    /// <summary>
    /// Accesseur en lecture et en écriture du salaire
    /// </summary>
    public double Salaire
    {
        get { return salaire; }
        set { salaire = value; }
    }

    /// <summary>
    /// Accesseur en lecture seule de la date d'embauche
    /// </summary>
    public DateTime DateEmbauche
    {
        get { return dateEmbauche; }
    }

    /// <summary>
    /// Accesseur en lecture et en ecriture de la liste des inférieurs hiérarchiques
    /// </summary>
    public List<Salarie> InferieurHierachique
    {
        get { return inferieurHierachique; }
        set { inferieurHierachique = value; }
    }

    /// <summary>
    /// Accesseur en lecture et en écriture du supérieur hiérarchique
    /// </summary>
    public Salarie SuperieurHierachique
    {
        get { return superieurHierarchique; }
        set { superieurHierarchique = value; }
    }

    #endregion

    /// <summary>
    /// Créer un objet Salarie
    /// </summary>
    /// <param name="prenom"></param>
    /// <param name="nom"></param>
    /// <param name="naissance"></param>
    /// <param name="adresse"></param>
    /// <param name="mail"></param>
    /// <param name="telephone"></param>
    /// <param name="poste"></param>
    /// <param name="salaire"></param>
    /// <param name="dateEmbauche"></param>
    public Salarie(int id, string prenom, string nom, DateTime naissance, string adresse, string mail, string telephone, string poste, double salaire, DateTime dateEmbauche, Salarie superieurHierarchique) : base(id, prenom, nom, naissance, adresse, mail, telephone)
    {
        this.poste = poste;
        this.dateEmbauche = dateEmbauche;
        this.salaire = salaire;
        this.superieurHierarchique = superieurHierarchique;
        this.InferieurHierachique = new List<Salarie>();
    }

    public void AjouterInferieur(Salarie salarie)
    {
        inferieurHierachique.Add(salarie);
    }

    /// <summary>
    /// Affiche les informations de l'employé
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return base.ToString() + "\nPoste : " + poste + "\nSalaire : " + salaire + "\nDate d'embauche : " + dateEmbauche.ToString("dd/MM/yyyy") + "\n";
    }

    public void ToStringOrganigramme(int tab)
    {
        for (int i = 0; i < tab; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine(this.prenom + " " + this.nom);
        for (int i = 0; i < tab; i++)
        {
            Console.Write("\t");
        }
        Console.WriteLine("Poste : " + poste);
    }

    // Propriétés pour l'abre n-aire

    public bool IsFeuille()
    {
        return inferieurHierachique.Count == 0;
    }
}
}
