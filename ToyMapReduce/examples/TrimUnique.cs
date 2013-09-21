using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToyMapReduce.MapReduce;

namespace ToyMapReduce.examples
{
    sealed class TrimUnique : MapReduce<string, string, string, int> 
    {
        public TrimUnique(): base(new DefaultCSVRetriever()) { }

        public override void map(KeyValuePair<object, object> KVPair)
        {
            string Value = (string)KVPair.Value;
            addMap("all", Value.Substring(0, Value.Length - 10));
        }

        public override void reduce(string Key, List<string> Values)
        {
            HashSet<string> ReducedValues = new HashSet<string>(Values);
            addReduce("unique trims", ReducedValues.Count);
        }
    }
}
