using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        }
    }
}
