using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Extensions.Collections
{
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
    }
       
}
