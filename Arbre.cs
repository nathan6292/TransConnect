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

            Tools.UpdateCSV();
            string[] lines = Tools.ReadCSV("./Sauvegarde/Coordinates.csv");

            for(int i = 0; i<lines.Length; i++){
                string[] values = lines[i].Split(';');
                nodes.Add(new Node(values[0], double.Parse(values[1]), double.Parse(values[2])));               
            }

            string[] lines2 = Tools.ReadCSV("./Sauvegarde/Distances.csv");
            for(int i = 0; i<lines2.Length; i++){
                string[] values2 = lines2[i].Split(';');
                Node city1 = nodes.Find(x => x.GetName() == values2[0]);
                Node city2 = nodes.Find(x => x.GetName() == values2[1]);

                city1.AddArrête(new Arrête(city1, city2,double.Parse(values2[2]), double.Parse(values2[3])));
                city2.AddArrête(new Arrête(city1, city2,double.Parse(values2[2]), double.Parse(values2[3])));
            }
        }

        public override string ToString()
        {
            string output="";
            for(int i = 0; i<nodes.Count; i++){
                output+= nodes[i].ToString() + "\n";
            }
            return output;
        }

        public string Shortest_Path(string start, string end)
        {
            Dictionary<Node,(Node,double)> distance = new Dictionary<Node,(Node,double)>();
            bool modif = true;

            Node Start_Node = nodes.Find(node => node.GetName() == start);
            Node End_Node = nodes.Find(node => node.GetName() == end);


            for(int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].GetName() == Start_Node.GetName())
                {
                    distance.Add(nodes[i], (Start_Node, 0));
                }
                else
                {
                    distance.Add(nodes[i],(null, 100000));
                }
            }



            Node last = null;
            Node temp = Start_Node;
            while (modif)
            {
                modif = false;
                double min = 1000000;
                Node node_min = null;
                double length = 0;
                List<Arrête> temp_arrete = temp.GetArrêtes(); 
                for(int i = 0; i<temp_arrete.Count; i++) 
                {
                    if (temp.GetName() == temp_arrete[i].GetStart().GetName())
                    {
                        if (last != null)
                        {
                            length = distance[temp].Item2 + temp_arrete[i].GetTime();
                        }
                        else
                        {
                            length = temp_arrete[i].GetTime();
                        }
                        Console.WriteLine("Length : " + length);
                        Console.WriteLine(distance[temp_arrete[i].GetEnd()].Item2);

                        if (length < distance[temp_arrete[i].GetEnd()].Item2)
                        {
                            Console.WriteLine("Modification");
                            modif = true;
                            distance[temp_arrete[i].GetEnd()] = (temp, length);
                            if (min > length)
                            {
                                min = length;
                                node_min = temp_arrete[i].GetEnd();
                            }
                        }
                    }
                }
                last = temp;
                temp = node_min;

            }


            foreach (var key in distance.Keys)
            {
                if (distance[key].Item1 != null)
                {
                    Console.WriteLine("Ville : {0}, Valeur : ({1}, {2})", key.GetName(), distance[key].Item1.GetName(), distance[key].Item2);
                }
            }

            return "";
        }

    }
}