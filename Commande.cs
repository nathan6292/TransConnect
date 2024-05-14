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
        protected string itineraire;
        protected string duration;

        protected DateTime date;

        protected bool statut;

        #region Accesseurs

        /// <summary>
        /// Accesseur de l'attribut id en lecture et écriture
        /// </summary>
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Accesseur de l'attribut client en lecture et écriture
        /// </summary>
        public Client Client { get => client; set => client = value; }

        /// <summary>
        /// Accesseur de l'attribut chauffeur en lecture et écriture
        /// </summary>
        public Chauffeur Chauffeur { get => chauffeur; set => chauffeur = value; }

        /// <summary>
        /// Accesseur de l'attribut vehicule en lecture et écriture
        /// </summary>
        public Vehicule Vehicule { get => vehicule; set => vehicule = value; }

        /// <summary>
        /// Accesseur de l'attribut départ en lecture et écriture
        /// </summary>
        public string Depart { get => départ; set => départ = value; }

        /// <summary>
        /// Accesseur de l'attribut arrivée en lecture et écriture
        /// </summary>
        public string Arrivee { get => arrivée; set => arrivée = value; }

        /// <summary>
        /// Accesseur de l'attribut prix en lecture et écriture
        /// </summary>
        public double Prix { get => prix; set => prix = value; }

        /// <summary>
        /// Accesseur de l'attribut description en lecture et écriture
        /// </summary>
        public string Description { get => description; set => description = value; }

        /// <summary>
        /// Accesseur de l'attribut itineraire en lecture et écriture
        /// </summary>
        public DateTime Date { get => date; set => date = value; }

        /// <summary>
        /// Accesseur de l'attribut statut en lecture et écriture
        /// </summary>
        public bool Statut { get => statut; set => statut = value; }

        #endregion

        /// <summary>
        /// Créer une commande
        /// </summary>
        /// <param name="entreprise"></param>
        /// <param name="client"></param>
        /// <param name="chauffeur"></param>
        /// <param name="vehicule"></param>
        /// <param name="depart"></param>
        /// <param name="arrivee"></param>
        /// <param name="date"></param>
        /// <param name="prix"></param>
        /// <param name="description"></param>
        public Commande(Entreprise entreprise,Client client, Chauffeur chauffeur, Vehicule vehicule, string depart, string arrivee, DateTime date,double prix, string description)
        {

            
            this.entreprise = entreprise;
            this.client = client;
            this.chauffeur = chauffeur;
            this.vehicule = vehicule;
            this.départ = depart;
            this.arrivée = arrivee;
            this.description = description;
            this.duration = "";            
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

            //Si le prix est donné, on le prend tel quel, sinon on le calcule
            if (prix != -1) this.prix = prix;
            else GetPrice();
        }


        /// <summary>
        /// Afficher la commande
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return $"Commande ID: {id}\nClient: {client.Nom} {client.Prenom}\nChauffeur: {chauffeur.Nom} {chauffeur.Prenom}\nVéhicule: {vehicule.Immatriculation}\nDépart: {départ}\nArrivée: {arrivée}\nPrix: {prix}\nDate: {date.ToShortDateString()}\nStatut: " + (statut ? "Livré" : "A venir") + "\n\n";
        }

        /// <summary>
        /// Calculer le prix de la commande
        /// </summary>

        public void GetPrice()
        {
                List<string> shortestPath = entreprise.GetArbre().Shortest_Path(this.départ, this.arrivée);

                double duration = double.Parse(shortestPath[shortestPath.Count - 2]);
                this.duration = Tools.doubleToTime(duration);
                double distance = double.Parse(shortestPath[shortestPath.Count - 1]);

                double tarifHoraire = chauffeur.CalculerTarifHoraire();

                double tarifkm;

                switch (vehicule)
                {
                    case Voiture:
                        tarifkm = 0.5;
                        break;

                    case Camionette:
                        tarifkm = 1;
                        break;

                    case CamionBenne:
                        tarifkm = 1.5;
                        break;

                    case CamionCiterne:
                        tarifkm = 1.5;
                        break;

                    case CamionFrigorifique:
                        tarifkm = 2;
                        break;
                    default:
                        tarifkm = 0.5;
                        break;
                }

                List<string> path = new List<string>();
                path.Add((tarifkm * distance + tarifHoraire * duration/60).ToString());

                for (int i = shortestPath.Count - 3; i >= 0; i--)
                {
                    path.Add(shortestPath[i]);
                }

                for (int i = 1; i < path.Count-1; i++)
                {
                    itineraire += path[i] + " --> ";
                }
                itineraire += path[path.Count - 1];

                prix = double.Parse(path[0]);

            //Appliquons la remise liée à la fidélité du client
            prix = prix * (1 - client.Remise(entreprise));
        }

        /// <summary>
        /// Créer une facture
        /// </summary>
        public void CreerFacture()
        {
            GetPrice();
            //Numéro de la facture
            Random rand = new Random();
            int numeroFacture = id;

            // Création du document PDF
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"Facture_{numeroFacture}.pdf", FileMode.Create));

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

            //Chauffeur lié à la commande
            document.Add(new Paragraph($"Chauffeur: {chauffeur.GetName()}", infoFont));

            //Tarif Horaire
            document.Add(new Paragraph($"Tarif Horaire: {chauffeur.CalculerTarifHoraire()} €", infoFont));

            //Véhicule utilisé
            document.Add(new Paragraph($"Vehicule: {vehicule.GetType()}", infoFont));

            //Espace
            document.Add(new Paragraph("\n",infoFont));

            //Itinéraire
            document.Add(new Paragraph("Itinéraire : " + itineraire, detailFont));

            //Durée du trajet
            document.Add(new Paragraph("Durée du trajet : " + duration, detailFont));

            //Espace
            document.Add(new Paragraph("\n", infoFont));

            // Ajout des détails de la TVA
            document.Add(new Paragraph($"TVA: 20 %", infoFont));

            double MontantTVA = Convert.ToDouble(prix) * 0.2;
            double PrixTotal = Convert.ToDouble(prix) + MontantTVA;

            document.Add(new Paragraph($"Montant de la TVA: {Math.Round(MontantTVA,2)} €", infoFont));
            document.Add(new Paragraph($"Montant hors taxe: {Math.Round(prix, 2)} €", infoFont));

            // Ajout du prix total
            document.Add(new Paragraph($"Prix total: {Math.Round(PrixTotal, 2)} €", detailFont));

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

        /// <summary>
        /// Envoyer une facture par mail au client
        /// </summary>
        public void SendFacture()
        {
            CreerFacture();
            // Créer une instance de MailMessage pour représenter l'e-mail
            MailMessage message = new MailMessage();

            // Définir l'adresse e-mail de l'expéditeur
            message.From = new MailAddress("mr.dupond.transconnect@gmail.com");

            // Définir l'adresse e-mail du destinataire


            //message.To.Add(client.Mail);
            //En théorie, on envoie la facture au client, mais pour les besoins de la démonstration, on l'envoie à une adresse de test
            Console.WriteLine("Il semblerait que le protocole smtp ne soit pas accrsible depuis leWiFi du pole deVinci, veuillez vous connecter à un autre réseau");
            Console.WriteLine("En théorie, on envoie la facture au client, mais pour les besoins de la démonstration, on l'envoie à une adresse de test que l'on vous laisse saisir : ");
            Console.WriteLine("\nLes adresses mail devinci posent parfois problème car elle bloque parfois les mails jugés supsects, entrez si possible un autre email !");
            Console.WriteLine("Adresse mail : ");

            bool valid = false;
            while (!valid)
            {
                string adresseTest = Console.ReadLine();
                valid = true;
                try
                {
                    message.To.Add(adresseTest);
                }
                catch
                {
                    Console.WriteLine("Adresse invalide, veuillez réessayer : ");
                    valid = false;
                }
            }

            // Définir l'objet de l'e-mail
            message.Subject = "Votre facture";

            // Définir le corps de l'e-mail en HTML
            string body = $"<html><body><p>Cher {client.Nom},</p><p>Veuillez trouver ci-joint votre facture.</p><p>Cordialement,</p><p>{entreprise.Nom}</p></body></html>";
            message.Body = body;
            message.IsBodyHtml = true;

            // Ajouter la facture en pièce jointe
            Attachment factureAttachment = new Attachment($"Facture_{id}.pdf");
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
    }
}