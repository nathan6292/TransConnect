using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


using System.Net.Mail;
using TransConnect;


namespace Projet_TransConnect
{
    public class Commande
{
    private Entreprise entreprise;
    private static List<int> IDexistant = new List<int>();

    protected int id;

    protected Client client;
    protected Chauffeur chauffeur;
    protected Vehicule vehicule;

    protected string départ;
    protected string arrivée;

    protected double prix;
    protected string description;

    protected DateTime date;

    protected bool statut;

    #region Accesseurs

    public int Id { get => id; set => id = value; }

    public Client Client { get => client; set => client = value; }

    public Chauffeur Chauffeur { get => chauffeur; set => chauffeur = value; }

    public Vehicule Vehicule { get => vehicule; set => vehicule = value; }

    public string Depart { get => départ; set => départ = value; }

    public string Arrivee { get => arrivée; set => arrivée = value; }

    public double Prix { get => prix; set => prix = value; }

    public string Description { get => description; set => description = value; }

    public DateTime Date { get => date; set => date = value; }

    public bool Statut { get => statut; set => statut = value; }

    #endregion

    public Commande(Entreprise entreprise,Client client, Chauffeur chauffeur, Vehicule vehicule, string depart, string arrivee, DateTime date,double prix, string description)
    {
        this.entreprise = entreprise;
        this.client = client;
        this.chauffeur = chauffeur;
        this.vehicule = vehicule;
        this.départ = depart;
        this.arrivée = arrivee;
        this.prix = prix;
        this.description = description;

        this.date = date;

        this.statut = date < DateTime.Now;

        Random rand = new Random();
        int temp = rand.Next(0, 1000000);
        while (IDexistant.Contains(temp))
        {
            temp = rand.Next(0, 1000000);
        }
        this.id = temp;
        IDexistant.Add(temp);
        chauffeur.EmploiDuTemps.Add(date);
        vehicule.EmploiDuTemps.Add(date);
    }
    //Constructeur

    //Chauffeur doit etre libre
    //Véhicule doit etre libre
    //Client doit etre existant
    //ID doit etre unique


    //ToString

    public string ToString()
    {
        return $"Commande ID: {id}\nClient: {client.Nom} {client.Prenom}\nChauffeur: {chauffeur.Nom} {chauffeur.Prenom}\nVéhicule: {vehicule.Immatriculation}\nDépart: {départ}\nArrivée: {arrivée}\nPrix: {prix}\nDate: {date.ToShortDateString()}\nStatut: " + (statut ? "Livré" : "A venir") + "\n\n";

    }


    //Facture

    public void CreerFacture(string path)
    {
        //Numéro de la facture
        Random rand = new Random();
        int numeroFacture = rand.Next(0, int.MaxValue);

        // Création du document PDF
        Document document = new Document(PageSize.A4, 25, 25, 30, 30);
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(path + "Facture_" + numeroFacture + ".pdf", FileMode.Create));

        // Ouverture du document
        document.Open();

        // Police pour le titre
        Font titleFont = FontFactory.GetFont("Helvetica", 20, Font.BOLD);

        // Ajout du titre
        Paragraph title = new Paragraph("FACTURE", titleFont);
        title.Alignment = Element.ALIGN_CENTER;
        document.Add(title);

        // Police pour les informations générales
        Font infoFont = FontFactory.GetFont("Helvetica", 12, Font.NORMAL);

        // Ajout des informations sur l'entreprise
        Paragraph entrepriseInfo = new Paragraph($"{this.entreprise.Nom}\n{this.entreprise.Adresse}\n{this.entreprise.Mail}\n{this.entreprise.Telephone}", infoFont);
        entrepriseInfo.Alignment = Element.ALIGN_LEFT;
        document.Add(entrepriseInfo);

        // Ajout des informations sur le client
        Paragraph clientInfo = new Paragraph($"{client.Nom} {client.Prenom}\n{client.Adresse}\n{client.Mail}\n{client.Telephone}", infoFont);
        clientInfo.Alignment = Element.ALIGN_RIGHT;
        document.Add(clientInfo);

        // Ajout d'une ligne de séparation
        document.Add(new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)))) ;

        // Police pour les détails de la commande
        Font detailFont = FontFactory.GetFont("Helvetica", 12, Font.BOLD);


        // Ajout du numéro de facture
        document.Add(new Paragraph($"Numéro de facture: {numeroFacture}", detailFont));

        // Ajout de la date d'émission
        document.Add(new Paragraph($"Date d'émission: {DateTime.Now.ToShortDateString()}", infoFont));

        DateTime DateEcheance = DateTime.Now.AddDays(30);
        // Ajout de la date d'échéance
        document.Add(new Paragraph($"Date d'échéance: {DateEcheance.ToShortDateString()}", infoFont));

        // Ajout des détails de la TVA
        document.Add(new Paragraph($"TVA: 20 %", infoFont));

        double MontantTVA = Convert.ToDouble(prix) * 0.2;
        double PrixTotal = Convert.ToDouble(prix) + MontantTVA;

        document.Add(new Paragraph($"Montant de la TVA: {MontantTVA} €", infoFont));
        document.Add(new Paragraph($"Montant hors taxe: {prix} €", infoFont));

        // Ajout du prix total
        document.Add(new Paragraph($"Prix total: {PrixTotal} €", infoFont));

        // Ajout d'une ligne de séparation
        document.Add(new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)))) ;


        // Ajout d'une mention légale
        Paragraph mentionLegale = new Paragraph("Merci pour votre commande ! ", infoFont);
        mentionLegale.Alignment = Element.ALIGN_JUSTIFIED;
        document.Add(mentionLegale);

        // Ajout d'une signature
        Paragraph signature = new Paragraph("Signature : \n\n\n", infoFont);
        signature.Alignment = Element.ALIGN_RIGHT;
        document.Add(signature);

        // Fermeture du document
        document.Close();
    }

    public void SendFacture(string path)
    {
        // Créer une instance de MailMessage pour représenter l'e-mail
        MailMessage message = new MailMessage();

        // Définir l'adresse e-mail de l'expéditeur
        message.From = new MailAddress("mr.dupond.transconnect@gmail.com");

        // Définir l'adresse e-mail du destinataire
        message.To.Add("valentin.dugay@edu.devinci.fr");

        // Définir l'objet de l'e-mail
        message.Subject = "Votre facture";

        // Définir le corps de l'e-mail en HTML
        string body = $"<html><body><p>Cher {client.Nom},</p><p>Veuillez trouver ci-joint votre facture.</p><p>Cordialement,</p><p>{entreprise.Nom}</p></body></html>";
        message.Body = body;
        message.IsBodyHtml = true;

        // Ajouter la facture en pièce jointe
        Attachment factureAttachment = new Attachment(path);
        message.Attachments.Add(factureAttachment);

        // Créer une instance de SmtpClient pour envoyer l'e-mail
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

        // Définir les informations d'identification pour l'authentification SMTP
        smtpClient.Credentials = new System.Net.NetworkCredential("mr.dupond.transconnect@gmail.com", "elid cyth jdpi pnvc");
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;

        // Envoyer l'e-mail
        smtpClient.Send(message);

        // Fermer la connexion SMTP
        smtpClient.Dispose();

    }

    //Annuler

    //Modifier



    //Afficher List<commande> dans entreprise
    //Ne pas oublier la liste chainée
}
}