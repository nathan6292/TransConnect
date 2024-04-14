using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class Arrête
    {

        public Node Start {get;}
        public Node End {get;}
        public double distance {get;}
        public double time {get;}
        public Arrête(Node Start, Node End, double distance, double time){
            this.Start = Start;
            this.End = End;
            this.distance = distance;
            this.time = time;
        }
    }
}