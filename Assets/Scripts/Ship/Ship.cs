using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace Battle
{
    [System.Serializable]
    public abstract class Ship : MonoBehaviour
    {
        [HideInInspector]public Stats stats;

        public string Name;
        public int MaxItems;//Total or types?
        public Character Pilot;

        //TODO Change inspector so that skills can only be added from a predetermined list


        public List<BattleSkill> KnownSkills=new List<BattleSkill>();
        public List<BattleCommand> KnownCommands = new List<BattleCommand>();
        public Element element = Element.None;
        public List<Status> Statuses=new List<Status>();
        [HideInInspector]public Ship[] Enemies;
        public Ship[] Allies;
        public Vector3 DefaultRot;
        [HideInInspector]public List<string> SkillStrings;

        public abstract IEnumerator GetCommand();
        public abstract IEnumerator GetTarget(BattleCommand bc);
        public bool Alive() { return stats["Health",false] > 0; }


        public virtual void Start()
        {
            Stats tempStats=stats;
            foreach(string s in SkillStrings)
            {
                KnownSkills.Add(BattleSkill.Skills[s]);
                KnownCommands.Add(BattleSkill.Skills[s]);
            }
            FromJSON();
            
            stats=tempStats;
            stats.SetShip(this);
        }

        public void Heal(int amnt)
        {
            this.PlaySound("Heal");
            stats.Heal(amnt);
        }

        public void TakeDamage(int dmg)
        {
            if (dmg < 0) { dmg = 0; }
            stats.TakeDamage(dmg);
            
            Debug.Log("Took " + dmg + ". HP=" + stats["Health"].Base);
            if (stats["Health",false] <= 0)
            {
                Die();
            }


        }
        public virtual void Die()
        {
            this.PlaySound("Explosion",volume:0.1f);
            BattlePrefabs.p.Make("Big Explosion", transform.position);
            gameObject.SetActive(false);
        }

        public virtual void GetFuel(int amnt)
        {
            stats.GetFuel(amnt);
        }

        public void GetStatus(Status S)
        {
            if (HasStatus(S))
            {
                return;
            }
            Statuses.Add(S);
            S.OnGain(this);
        }

        public bool HasStatus(Status S)
        {
            return Statuses.Any(x => S.Name() == x.Name());
        }

        public void RemoveStatus(Status S)
        {
            if (!HasStatus(S))
            {
                return;
            }

            Status s = Statuses.Single(x => x.Name() == S.Name());
            s.OnCure(this);
            Statuses.Remove(s);

        }

        public void MakeExplosion()
        {
            Destroy(BattlePrefabs.p.Make("Explosion",transform.position),2);
        }

        public void debug(string s)
        {
            Debug.Log("LUA: "+s);
        }

        

        public delegate IEnumerator EffectAnimation(Ship S);

        public event EffectAnimation StartOfTurn;
        public event EffectAnimation EndOfTurn;
        public IEnumerator StartTurn()
        {
            if (StartOfTurn != null)
            {
                yield return StartOfTurn(this);
            }
        }
        public IEnumerator EndTurn()
        {
            stats.EndTurn();

            if (EndOfTurn != null)
            {
                yield return EndOfTurn(this);
            }
        }

        public IEnumerator GetHit(Vector3 Direction,float power)
        {
           yield return null;
        }

        public Ship FromJSON()
        {
            /*
            try
            {
                string FilePath = Application.streamingAssetsPath + "/Ships/" + Name + ".json";
                string json = File.ReadAllText(FilePath);
                try { JsonUtility.FromJsonOverwrite(json, this); }
                catch (Exception e)
                {
                    Debug.LogError(Name + " has bad JSON\n" + e);
                }
            }
            catch (FileNotFoundException e)
            {
                File.Create(Application.streamingAssetsPath + "/Ships/" + Name + ".json");
            }
            */
            stats = stats.FromJSON(this);
            //Allies = allies;
            return this;

        }
        public void ToJSON()
        {
            stats.ToJSON(this);
           
            /*
            Debug.Log("Saved " + Name + " to json");
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(Application.streamingAssetsPath + "/Ships/" + Name + ".json", json);
            */
            
        }


    }
}
