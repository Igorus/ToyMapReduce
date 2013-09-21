using System;
using System.Collections.Generic;

namespace ToyMapReduce.MapReduce
{
    interface IRetriever
    {
        IEnumerable<KeyValuePair<object, object>> Retrieve(string Path);
    }
}
