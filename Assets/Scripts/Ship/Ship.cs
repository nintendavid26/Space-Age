using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Battle
{
    public abstract class Ship : MonoBehaviour
    {
        [System.Serializable]
        public struct Stats
        {
            public int fuel, maxFuel;
            public int health, maxHealth;
            public int atk,def;
            public int speed;
            public int luck;
            public int level, exp;
        };

        public int MaxItems;//Total or types?
        public Dictionary<string, int> Items;
        public Stats stats;
        public Character Pilot;
        public List<string> SkillStrings;
        [HideInInspector]public List<BattleSkill> KnownSkills=new List<BattleSkill>();
        [HideInInspector]public List<BattleCommand> KnownCommands = new List<BattleCommand>();
        public Element element = Element.None;
        public List<Status> Statuses;
        [HideInInspector]public Ship[] Enemies;
        public Ship[] Allies;
        public Vector3 DefaultRot;
        // Use this for initialization

        // Update is called once per frame
        public abstract IEnumerator GetCommand();
        public abstract IEnumerator GetTarget(BattleCommand bc);
        public bool Alive() { return stats.health > 0; }

        public virtual void Start()
        {
            foreach(string s in SkillStrings)
            {
                KnownSkills.Add(BattleSkill.Skills[s]);
                KnownCommands.Add(BattleSkill.Skills[s]);
            }
            
        }

        public void Heal(int amnt)
        {
            stats.health += amnt;
            stats.health = stats.health > stats.maxHealth ? stats.maxHealth : stats.health;
        }

        public void TakeDamage(int dmg)
        {
            stats.health -= dmg;
            if (stats.health <= 0)
            {
                Die();
            }

        }
        /// <summary>
        /// put the base last?
        /// </summary>
        public virtual void Die()
        {
            
            BattlePrefabs.p.Make("Big Explosion", transform.position);
            gameObject.SetActive(false);
        }

        public virtual void GetFuel(int amnt)
        {
            stats.fuel += amnt;
            if (stats.fuel > stats.maxFuel) { stats.fuel = stats.maxFuel; }
        }
        public virtual void GetHP(int amnt)
        {
            stats.health += amnt;
            if (stats.health > stats.maxHealth) { stats.health = stats.maxHealth; }
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

        public void GetItem(Item I,int amnt=1)
        {
            if (Items.ContainsKey(I.Name))
            {
                Items[I.Name] += amnt;
            }
            else
            {
                Items.Add(I.Name, amnt);
            }
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
            if (EndOfTurn != null)
            {
                yield return EndOfTurn(this);
            }
        }

        public IEnumerator GetHit(Vector3 Direction,float power)//
        {
           yield return null;
        }

    }
}
