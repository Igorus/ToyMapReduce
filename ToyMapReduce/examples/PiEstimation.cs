using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ToyMapReduce.MapReduce;

namespace ToyMapReduce.examples
{
    //With 1 000 000 instances get ~3.140. I thought it would be better. Can try another user defined random function.
    class PiEstimation : MapReduce<string, int, string, double>
    {
        public PiEstimation() : base(new RandomRetriever()) { }

        Func<double, double, double> sqrDistance = (x, y) => x * x + y * y;
        public override void map(KeyValuePair<object, object> KVPair)
        {
            if (sqrDistance((double)KVPair.Key, (double)KVPair.Value) <= 1) { emitIntermediate("circle", 1); }
            else { emitIntermediate("circle", 0); }
        }

        public override void reduce(string Key, List<int> Values)
        {
            emit("pi_" + Key.ToString(), 4 * (double)Values.Sum() / Values.Count);
        }
    }

    sealed class RandomRetriever : IRetriever
    {
        public IEnumerable<KeyValuePair<object, object>> Retrieve(object RetrieverData)
        {
            Random R = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < (int)RetrieverData; ++i)
            {
                yield return new KeyValuePair<object, object>(R.NextDouble(), R.NextDouble());
            }
        }
    }
}
