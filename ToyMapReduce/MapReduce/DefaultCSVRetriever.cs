using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ToyMapReduce.MapReduce
{
    class DefaultCSVRetriever : IRetriever
    {
        public IEnumerable<KeyValuePair<object, object>> Retrieve(object Path)
        {
            StreamReader sr = new StreamReader((string)Path);
            while (!sr.EndOfStream)
            {
                string[] Values = sr.ReadLine().Split(',', ';');
                yield return new KeyValuePair<object, object>(Values[0].Trim(), Values[1].Trim());
            }
            sr.Close();
        }
    }
}
