using System;
using System.Collections.Generic;
using System.Text;

namespace BmsSurvey.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void ForEach<Tkey, Tvalue>(this IDictionary<Tkey, Tvalue> dictionary, Action<KeyValuePair<Tkey, Tvalue>> action)
        {
            foreach (KeyValuePair<Tkey, Tvalue> entry in dictionary)
            {
                action(entry);
            }
        }
    }
}
