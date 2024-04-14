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
        
        public Node(string name, double longitude, double latitude){
            this.name = name;
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public string GetName(){
            return this.name;
        }
    }

    
}