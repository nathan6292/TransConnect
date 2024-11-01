using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    /// <summary>
    /// Classe représentant un arbre de noeuds.
    /// </summary>
    public class Arbre
    {
        // Liste des noeuds dans l'arbre.
        public List<Node> nodes = new List<Node>();

        /// <summary>
        /// Constructeur de la classe Arbre.
        /// </summary>
        /// <param name="nodes">Liste optionnelle de noeuds pour initialiser l'arbre.</param>
        public Arbre(List<Node> nodes = null)
        {
            if (nodes != null)
            {
                this.nodes = nodes;
            }
            else
            {
                this.nodes = new List<Node>();
            }
        }

        /// <summary>
        /// Initialise le graphe en lisant les données à partir de fichiers CSV.
        /// </summary>
        public void InitiateGraphe()
        {
            // Mise à jour des données CSV.
            Tools.UpdateCSV();
            // Lecture des coordonnées des noeuds.
            string[] lines = Tools.ReadCSV("./Sauvegarde/Coordinates.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(';');
                nodes.Add(new Node(values[0], double.Parse(values[1]), double.Parse(values[2])));
            }

            // Lecture des distances entre les noeuds.
            string[] lines2 = Tools.ReadCSV("./Sauvegarde/Distances.csv");
            for (int i = 0; i < lines2.Length; i++)
            {
                string[] values2 = lines2[i].Split(';');
                Node city1 = nodes.Find(x => x.GetName() == values2[0]);
                Node city2 = nodes.Find(x => x.GetName() == values2[1]);

                city1.AddArrête(new Arrête(city1, city2, double.Parse(values2[2]), double.Parse(values2[3])));
                city2.AddArrête(new Arrête(city1, city2, double.Parse(values2[2]), double.Parse(values2[3])));
            }
        }

        /// <summary>
        /// Convertit l'arbre en une chaîne de caractères.
        /// </summary>
        /// <returns>La représentation textuelle de l'arbre.</returns>
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < nodes.Count; i++)
            {
                output += nodes[i].ToString() + "\n";
            }
            return output;
        }

        /// <summary>
        /// Trouve le chemin le plus court entre deux noeuds dans l'arbre.
        /// </summary>
        /// <param name="start">Le noeud de départ.</param>
        /// <param name="end">Le noeud de fin.</param>
        /// <returns>Le chemin le plus court sous forme de liste de noms de noeuds et les distances et temps de parcours.</returns>
        public List<string> ShortestPath(string start, string end)
        {
            // Dictionnaire pour stocker les distances entre les noeuds.
            Dictionary<Node, (Node, double, double)> distance = new Dictionary<Node, (Node, double, double)>();
            // Liste des noeuds non visités.
            List<Node> unvisited = new List<Node>();

            // Recherche des noeuds de départ et de fin.
            Node Start_Node = nodes.Find(node => node.GetName() == start);
            Node End_Node = nodes.Find(node => node.GetName() == end);

            // Initialisation des distances.
            foreach (Node node in nodes)
            {
                if (node.GetName() == Start_Node.GetName())
                {
                    distance[node] = (Start_Node, 0, 0);
                }
                else
                {
                    distance[node] = (null, double.PositiveInfinity, double.PositiveInfinity);
                }
                unvisited.Add(node);
            }

            // Algorithme de Dijkstra pour trouver le chemin le plus court.
            while (unvisited.Any())
            {
                Node current = unvisited.OrderBy(node => distance[node].Item2).First();

                if (current == End_Node)
                    break;

                unvisited.Remove(current);

                foreach (Arrête neighbor in current.GetArrêtes())
                {
                    Node neighborNode = neighbor.GetEnd() == current ? neighbor.GetStart() : neighbor.GetEnd();
                    double alt = distance[current].Item2 + neighbor.GetDistance();
                    double alt2 = distance[current].Item3 + neighbor.GetTime();

                    if (alt < distance[neighborNode].Item2)
                    {
                        distance[neighborNode] = (current, alt, alt2);
                    }
                }
            }

            // Reconstruction du chemin le plus court.
            List<string> output = new List<string>();
            Node temp = End_Node;
            output.Add(End_Node.GetName());
            while (distance[temp].Item1 != Start_Node)
            {
                output.Add(distance[temp].Item1.GetName());
                temp = distance[temp].Item1;
            }
            output.Add(Start_Node.GetName());
            output.Add(distance[End_Node].Item2.ToString()); // Distance totale
            output.Add(distance[End_Node].Item3.ToString()); // Temps total

            return output;
        }

        /// <summary>
        /// Vérifie si tous les noeuds ont été visités dans le calcul du chemin le plus court.
        /// </summary>
        /// <param name="distance">Dictionnaire des distances.</param>
        /// <returns>True si tous les noeuds ont été visités, sinon False.</returns>
        public static bool IsEnd(Dictionary<Node, (Node, double, double)> distance)
        {
            bool end = true;

            foreach (var key in distance.Keys)
            {
                if (distance[key].Item1 == null)
                {
                    end = false;
                }
            }
            return end;
        }
    }
}
