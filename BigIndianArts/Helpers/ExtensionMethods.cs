using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BigIndianArts.Helpers
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> item)
        {
            return new ObservableCollection<T>(item);
        }

        public static T ConvertAs<T>(this object obj)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(Convert.ToString(obj));
            }
            catch (Exception e)
            {
                string _msg = e.ToString();
                return default(T);
            }
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }
    }
}
