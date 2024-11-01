using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Node
    {
        private string name { get;}
        private double latitude { get;}
        private double longitude { get;}
        
        private List<Arrête> arrêtes = new List<Arrête>();

        public string Name { get { return name; } }
        public Node(string name, double longitude, double latitude){
            this.name = name;
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public string GetName(){
            return this.name;
        }

        public double GetLongitude(){
            return this.longitude;
        }

        public double GetLatitude(){
            return this.latitude;
        }

        public List<Arrête> GetArrêtes(){
            return this.arrêtes;
        }

        public void AddArrête(Arrête arrête){
            this.arrêtes.Add(arrête);
        }

        public override string ToString()
        {
            string output = "Name : " + this.name + " Longitude : " + this.longitude + " Latitude : " + this.latitude;

            foreach(Arrête arrête in this.arrêtes){
                output += "\n" + arrête.ToString();
            }

            return output; 
        }

        
    }
}