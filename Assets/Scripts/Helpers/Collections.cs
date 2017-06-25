using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Extensions.Collections
{
    public struct ListPair<T, S>
    {
        public List<T> l1;
        public List<S> l2;

        public ListPair(List<T> l1, List<S> l2) : this()
        {
            this.l1 = l1;
            this.l2 = l2;
        }
    }
    public static class Collections
    {
        public static T[] Add<T>(this T Item,T[] other) {
            T[] temp = new T[]{ Item};
            return temp.Concat(other).ToArray();
        }
        public static T[] Add<T>(this T[] Items, T other)
        {
            T[] temp = new T[] { other };
            return Items.Concat(temp).ToArray();
        }
        public static T[] Add<T>(this T[] Items, T[] other)
        {
            return Items.Concat(other).ToArray();
        }

        public static T[] Remove<T>(this T[] array, T i)
        {
            var foos = new List<T>(array);
            foos.Remove(i);
            return foos.ToArray();
        }

        public static void Shuffle<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T temp = list[i];
                int randomIndex = UnityEngine.Random.Range(i, list.Count);
                list[i] = list[randomIndex];
                list[randomIndex] = temp;
            }
        }

        public static T RandomItemConditional<T>(this T[] list, Predicate<T> condition = null)
        {
            return list.ToList().RandomItem(condition);
        }

        public static T RandomItem<T>(this List<T> list, Predicate<T> condition = null)
        {
            List<T> temp = condition == null ? list : list.FindAll(condition);
            if (temp.Count == 0) { return default(T); }
            int r = UnityEngine.Random.Range(0, list.Count);
            return temp[r];

        }

        public static ListPair<T,S> ToLists<T, S>(this Dictionary<T, S> d, List<T> l1, List<S> l2)
        {
            l1 = new List<T>();
            l2 = new List<S>();
            foreach (KeyValuePair<T, S> pair in d)
            {
                l1.Add(pair.Key);
                l2.Add(pair.Value);
            }
            return new ListPair<T, S>(l1, l2);

        }
        public static Dictionary<T,S> FromLists<T, S>(this Dictionary<T, S> d, List<T> l1, List<S> l2)
        {
            if (l1.Count != l2.Count)
            {
                throw new System.ArgumentException(
                    "Error, both lists need to be the same length to convert to dictionary.\nList1 Count=" + l1.Count + " List2 Count=" + l2.Count);
            }
            if (l1.Count != l1.Distinct().Count())
            {
                throw new System.ArgumentException("Error, Can not create dictionary with duplicate keys");
            }
            d.Clear();
            for (int i = 0; i < l1.Count; i++)
            {
                d.Add(l1[i], l2[i]);
            }
            return d;
        }
    }
       
}
