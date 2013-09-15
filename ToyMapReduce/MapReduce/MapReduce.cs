using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToyMapReduce.MapReduce
{
    abstract class MapReduce<KMap, VMap, KReduce, VReduce>
    {
        private Dictionary<KMap, List<VMap>> intermediate;
        private Dictionary<KReduce, VReduce> output;

        public MapReduce()
        {
            intermediate = new Dictionary<KMap, List<VMap>>();
            output = new Dictionary<KReduce, VReduce>();
        }

        /// <summary>
        /// Add a key-value pair to intermediate variable for reducing afterward
        /// </summary>
        /// <param name="Key">Key for mapping</param>
        /// <param name="Value">Value for mapping</param>
        /// <returns>Returns true if the key doesn't exist and is adding, false otherwise</returns>
        public bool addMap(KMap Key, VMap Value)
        {
            if (!intermediate.ContainsKey(Key))
            {
                List<VMap> ValueList = new List<VMap>();
                ValueList.Add(Value);
                intermediate.Add(Key, ValueList);
                return false;
            }
            else
            {
                intermediate[Key].Add(Value);
                return true;
            }
        }

        /// <summary>
        /// Add a key-value pair to output variable
        /// </summary>
        /// <param name="Key">Reduced key</param>
        /// <param name="Value">Reduced value</param>
        public void addReduce(KReduce Key, VReduce Value )
        {
            output.Add(Key, Value);
        }

        /// <summary>
        /// Execute MapReduce
        /// </summary>
        /// <param name="Path">File path of input data</param>
        public void execute(String Path)
        {
            try
            {
                StreamReader sr = new StreamReader(Path);
                while (!sr.EndOfStream)
                {
                    map(preprocess(sr.ReadLine()));
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            foreach (KeyValuePair<KMap, List<VMap>> KVPair in intermediate) reduce(KVPair.Key, KVPair.Value);
        }

        public Dictionary<KReduce, VReduce> KeyValDictionary
        {
            get { return output; }
        }
        /// <summary>
        /// Preprocessing data
        /// </summary>
        /// <param name="data">String data</param>
        /// <returns>Returns KeyValuePair, where key is the first column of data and value is next columns</returns>
        abstract public KeyValuePair<object, object> preprocess(string data);
        abstract public void map(KeyValuePair<object, object> KVPair);
        abstract public void reduce(KMap Key, List<VMap> Values);
    }
}
