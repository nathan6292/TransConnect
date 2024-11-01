namespace Projet_TransConnect
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Dijkstra()
        {
            //On crée l'entreprise pour obtenir le graphe
            Entreprise TransConnect = new Entreprise("Sauvegarde");

            //On mets le résultat attendu (On fixe la durée à 0 car elle est calculé en temps réel)
            List<string> expected = new List<string>();
            expected.Add("Rouen");
            expected.Add("Paris");
            expected.Add("Toulouse");
            expected.Add("Pau");
            expected.Add("1001");
            expected.Add("0");

            //On calcule le trajet grâce à Dijkstra
            List<string> output = TransConnect.GetArbre().ShortestPath("Pau", "Rouen");
            //On fixe la durée à 0 pour correspondre avec le expected
            output[output.Count - 1] = "0";

            //On vérifie que tous les éléments des deux listes sont identiques
            bool valid = true;

            for (int i = 0; i < output.Count; i++)
            {
                if (output[i] != expected[i]) { valid = false; }
            }

            //Retourne vrai si valid = true et faux sinon
            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Test_GetPrice()
        {
            //On crée l'entreprise pour obtenir le graphe
            Entreprise TransConnect = new Entreprise("Sauvegarde");
            TransConnect.ReadSauvegarde("Sauvegarde");

            Commande test = TransConnect.Commandes[0];

            double expected = 1033;
            double output = Math.Round(test.GetPrice(false), 0);

            Assert.AreEqual(expected, output);
        }
    }
}