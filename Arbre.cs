using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Arbre
    {
        public List<Node> nodes = new List<Node>();

        public Arbre(List<Node> nodes = null)
        {
            if(nodes != null)
            {
                this.nodes = nodes;
            }else{
                this.nodes = new List<Node>();
            }
        }

        public void InitiateGraphe(){
            string[] lines = Tools.ReadCSV("./Coordinates.csv");

            for(int i = 0; i<lines.Length; i++){
                string[] values = lines[i].Split(';');
                nodes.Add(new Node(values[0], double.Parse(values[1]), double.Parse(values[2])));               
            }
            Console.WriteLine("Fin de la lecture des noeuds");
            string[] lines2 = Tools.ReadCSV("./Distances.csv");
            for(int i = 0; i<lines2.Length; i++){
                string[] values2 = lines2[i].Split(';');
                //Partie à changer pour ajouter calcul en temps réel des trajets
                Node city1 = nodes.Find(x => x.GetName() == values2[0]);
                Node city2 = nodes.Find(x => x.GetName() == values2[1]);

                city1.AddArrête(new Arrête(nodes.Find(x => x.GetName() == values2[0]), nodes[0],double.Parse(values2[2]),Tools.Time_StringtoDouble(values2[3])));
                city2.AddArrête(new Arrête(nodes.Find(x => x.GetName() == values2[1]), nodes[0],double.Parse(values2[2]),Tools.Time_StringtoDouble(values2[3])));
            }

            for(int i = 0; i<nodes.Count; i++){
                Console.WriteLine(nodes[i].ToString());
            }
        }
    }
}