using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Battle {
    //Complicated Items are directly Items
    //Simpler ones (like healing a fixed amount) can be derived
    //Also, only players can use items
    [Serializable]
    public class Item {

        public string Name;
        public string Description;
        public int Cost;//I'm assuming it goes here and not in a shop class
        public int Power;
        public string Stat;
        public int BuffDuration;
        public bool TargetAllies;
        public static Dictionary<string, Item> Items=new Dictionary<string, Item>();
        public enum UseType { OverWorld,Battle,Both};
        public UseType useType;

        public enum EffectType { Heal,Buff,Other}
        public EffectType effectType;

        public Item(string _Name)
        {
            FromJSON(_Name);
        }
        public Item()
        {
            
        }
        public IEnumerator Do(PlayerShip User, Ship Target)
        {
            switch (effectType)
            {
                case EffectType.Heal:
                    yield return Heal(Target);
                    break;
                case EffectType.Buff:
                    yield return Buff(Target);
                    break;
                case EffectType.Other:
                    yield return ItemParser.UseEffect(this, "Use", User, Target);
                    break;
                default:
                    yield return ItemParser.UseEffect(this, "Use", User, Target);
                    break;
            }
            PlayerShip.Items[Name]--;
            if (PlayerShip.Items[Name] == 0)
            {
                PlayerShip.Items.Remove(Name);
            }

            
        }
        public Ship[] ValidTargets(Ship User)
        {
            if (TargetAllies) { return PlayerShip.Player.Allies.ToList().Where(x=>x.Alive()).ToArray(); }
            else {
                return new Ship[] { User };
            }
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

        public IEnumerator Heal(Ship Target)
        {
            Target.Heal(Power);
            yield return new WaitForEndOfFrame();
        }
        public IEnumerator Buff(Ship Target)
        {
            Target.stats.AddBuff(Stat, Power, BuffDuration);
            yield return new WaitForEndOfFrame();
        }
    }
}