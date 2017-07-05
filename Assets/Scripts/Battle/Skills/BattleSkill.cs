using Extensions.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Battle
{

    [Serializable]
    public class BattleSkill : BattleCommand
    {
        public Element element;
        public element e;
        public static Dictionary<string,BattleSkill> Skills=new Dictionary<string,BattleSkill>();
        public bool Auto;
        public int Power;
        public string Category;
        public int UnlockCost=0;
        public string UnlockConditions;

        public override Ship[] GetTarget(Ship User)
        {
            if (!SingleTarget) { return ValidTargets(User);}
            return null;
        }

        public override IEnumerator Do(Ship User, Ship[] Target)
        {
            User.stats["Fuel"].Base -= Cost;
            yield return SkillParser.UseEffect(this, "Use", User,Target);
            
            
             
        }

        public override bool CanUse(Ship User)
        {
            if (ValidTargets(User) == null) { return false; }
            if (ValidTargets(User).Length == 0) { return false; }
            if (User.stats["Fuel"].Base < Cost) { return false; }
            return true;

        }

        public int CalculateDamage(Ship User,Ship Target)
        {
            int dmg = 0;
            dmg = 2 * (User.stats["Atk"].Modified+Power) - Target.stats["Def"].Modified;
            int netLuck =1+User.stats["Luck"].Modified - Target.stats["Luck"].Modified;
            netLuck = Mathf.Clamp(netLuck,1,95);//Should  be from 1 to 95
            double crit = UnityEngine.Random.Range(1, 100)<=netLuck?1.5:1;

            //dmg = (int)(dmg * crit);
            return dmg;
        }

        public override Ship[] ValidTargets(Ship User)
        {
            if (target == TargetType.Self)
            {
                return new Ship[] { User };
            }
            else if (target == TargetType.Enemies)
            {
                return User.Enemies.ToList().Where(x=>x.Alive()).ToArray();
            }
            else if (target == TargetType.Allies)
            {
                return User.Allies.ToList().Where(x => x.Alive()).ToArray();
            }
            else { return User.Allies.Add(User.Enemies); }

        }

        public BattleSkill FromJSON(string s)
        {
            string FilePath = Application.streamingAssetsPath + "/Skills/JSON/" + s + ".json";
            string json = File.ReadAllText(FilePath);
            try { JsonUtility.FromJsonOverwrite(json, this); }
            catch (Exception e)
            {
                Debug.LogError(s + " has bad JSON\n" + e);
            }
            element = Element.FromEnum(e);
            return this;

        }
        public void ToJSON()
        {
            Debug.Log("Saved " + Name + " to json");
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(Application.streamingAssetsPath + "/Skills/JSON/" + Name + ".json", json);
        }

        public bool CanUnlock(Ship s)
        {
            return true;
        }

    }


}
