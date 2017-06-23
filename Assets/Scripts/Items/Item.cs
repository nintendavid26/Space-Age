using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Battle {
    //Complicated Items are directly Items
    //Simpler ones (like healing a fixed amount) can be derived
    [Serializable]
    public class Item {

        public string Name;
        public string Description;
        public int Cost;//I'm assuming it goes here and not in a shop class
        public static Dictionary<string, Item> Items=new Dictionary<string, Item>();
        public enum UseType { OverWorld,Battle,Both};
        public UseType useType;

        public IEnumerator Do(Ship User, Ship Target)
        {
            yield return ItemParser.UseEffect(this, "Use", User, Target);
        }
        public Ship[] ValidTargets(Ship User)
        {
            return null;
        }

        public Item FromJSON(string s)
        {
            string FilePath = Application.streamingAssetsPath + "/Items/JSON/" + s + ".json";
            string json = File.ReadAllText(FilePath);
            try { JsonUtility.FromJsonOverwrite(json, this); }
            catch (Exception e)
            {
                Debug.LogError(s + " has bad JSON\n" + e);
            }
            return this;

        }
        public void ToJSON()
        {
            Debug.Log("Saved " + Name + " to json");
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(Application.streamingAssetsPath + "/Items/JSON/" + Name + ".json", json);
        }
    }
}