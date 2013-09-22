using System;
using System.Collections.Generic;
using ToyMapReduce.MapReduce;
using System.IO;

namespace ToyMapReduce.examples
{
    sealed class WineAcidity : MapReduce<int, double, int, double>
    {
        public WineAcidity() : base(new CustomCSVRetriever()) { }

        public override void map(KeyValuePair<object, object> KVPair)
        {
            emitIntermediate((int)KVPair.Key, (double)KVPair.Value);
        }

        public override void reduce(int Key, List<double> Values)
        {
            double summ = 0;
            foreach (double s in Values) summ += s;
            summ /= Values.Count;
            emit(Key, summ);
        }
    }

    sealed class CustomCSVRetriever : IRetriever
    { 
        public IEnumerable<KeyValuePair<object, object>> Retrieve(object Path)
        {
            StreamReader sr = new StreamReader((string)Path);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string[] Values = sr.ReadLine().Split(',', ';');
                yield return new KeyValuePair<object, object>(Convert.ToInt32(Values[11].Trim()), 
                                                              Convert.ToDouble(Values[0].Trim()));
            }
            sr.Close();
         }
    }
}
