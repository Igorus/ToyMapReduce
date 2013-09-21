using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

namespace ToyMapReduce.MapReduce
{
    abstract class MapReduce<KMap, VMap, KReduce, VReduce>
    {
        private Dictionary<KMap, List<VMap>> intermediate;
        private Dictionary<KReduce, VReduce> output;
        private IRetriever retriever;

        public Dictionary<KReduce, VReduce> KeyValDictionary { get { return output; } }

        public MapReduce() : this(new DefaultCSVRetriever()) { }

        public MapReduce(IRetriever _Retriever)
        {
            retriever = _Retriever;
            intermediate = new Dictionary<KMap, List<VMap>>();
            output = new Dictionary<KReduce, VReduce>();
        }

        /// <summary>
        /// Add a key-value pair to intermediate variable for reducing afterward
        /// </summary>
        /// <param name="Key">Key for mapping</param>
        /// <param name="Value">Value for mapping</param>
        /// <returns>Returns true if the key doesn't exist and is adding, false otherwise</returns>
        public void addMap(KMap Key, VMap Value)
        {
            if (!intermediate.ContainsKey(Key))
            {
                List<VMap> ValueList = new List<VMap>();
                ValueList.Add(Value);
                intermediate.Add(Key, ValueList);
            }
            else { intermediate[Key].Add(Value); }
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
                IEnumerable<KeyValuePair<object, object>> KVPCollection =
                    (IEnumerable<KeyValuePair<object, object>>)retriever.Retrieve(Path);
                foreach (KeyValuePair<object, object> KVP in KVPCollection) map(KVP);
                foreach (KeyValuePair<KMap, List<VMap>> KVPair in intermediate) reduce(KVPair.Key, KVPair.Value);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        abstract public void map(KeyValuePair<object, object> KVPair);

        abstract public void reduce(KMap Key, List<VMap> Values);
    }
}
