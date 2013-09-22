using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using ToyMapReduce.examples;

namespace ToyMapReduce
{
    class Program
    {
        static void Main(string[] args)
        {
            TrimUnique TrimProblem = new TrimUnique();
            TrimProblem.execute("../../examples/dna.txt");
            foreach(KeyValuePair<string, int> KVPair in TrimProblem.KeyValDictionary) 
                Console.WriteLine(KVPair.Key + ": " + KVPair.Value.ToString());
            Console.ReadLine();

            WineAcidity RedWine = new WineAcidity();
            RedWine.execute("../../examples/winequality-red.csv");
            foreach (KeyValuePair<int, double> KVPair in RedWine.KeyValDictionary)
                Console.WriteLine(KVPair.Key + ": " + KVPair.Value.ToString());
            Console.ReadLine();

            PiEstimation PiEstimator = new PiEstimation();
            PiEstimator.execute(1000000);
            foreach (KeyValuePair<string, double> KVPair in PiEstimator.KeyValDictionary)
                Console.WriteLine(KVPair.Key + ": " + KVPair.Value.ToString());
            Console.ReadLine();
        }
    }
}
