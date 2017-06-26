using Battle;
using Extensions.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



/// <summary>
/// Use PascalCase for all stat Names!!!
/// </summary>
[System.Serializable]
public class Stats:System.Object {


    public Dictionary<string, Stat> stats;
    [HideInInspector]
    public List<string> StatNames;
    [HideInInspector]
    public List<Stat> StatList;


    public void Remove(string s)
    {
        stats.Remove(s);
    }

    public void Start()
    {

    }

    public void Clear()
    {
        stats = new Dictionary<string, Stat>();
    }

    public int Count()
    {
        return stats.Count;
    }

    public void Set(Stat stat)
    {
        if (stats.ContainsKey(stat.Name))
        {
            stats[stat.Name] = stat;
        }
        else
        {
            stats.Add(stat.Name, stat);
        }
    }

    public int Get(Dictionary<string, int> d, string s)
    {
        return d[s];
    }

    public Stats()
    {
        stats = new Dictionary<string, Stat>();
        Set(new Stat("New", 0));
    }

    public List<string> Names()
    {
        return new List<string>(stats.Keys);
    }
    public List<Stat> StatTypes()
    {
        return new List<Stat>(stats.Values);
    }

    public Stat this[string stat]
    {
        get
        {
            if (!stats.ContainsKey(stat))
            {
                stats.Add(stat, new Stat(stat, 0));
            }
            return stats[stat];
        }

        set
        {
            if (!stats.ContainsKey(stat))
            {
                stats.Add(stat, new Stat(stat, 0));
            }
            else
            {
                stats[stat] = value;
            }
        }
    }

    public int this[string stat,bool modified]
    {
        get {
            if (!stats.ContainsKey(stat))
            {
                stats.Add(stat, new Stat(stat,0));
            }
            return modified?stats[stat].Modified:stats[stat].Base;
        }

        set
        {
            if (!stats.ContainsKey(stat))
            {
                stats.Add(stat, new Stat(stat,0));
            }
            if (modified)
            {
                stats[stat].Base = value;
            }
            else
            {
                stats[stat].Base = value;
            }
        }
    }

    public void ResetModified()
    {
        foreach (Stat s in stats.Values)
        {
            s.Buffs = new List<Stat.Buff>();
        }
    }

    public void Heal(int amnt)
    {
        stats["Health"].Base += amnt;
        stats["Health"].Base = stats["Health"].Base > stats["MaxHealth"].Base ? stats["MaxHealth"].Base : stats["Health"].Base;
    }

    public void AddModifier(string stat,float amnt,op Op,int Duration)
    {
        
    }

    public void EndTurn()
    {
        foreach (Stat s in stats.Values)
        {
            s.TurnPasses();
        }
    }

    public void RemoveModifier()
    {

    }

    public void IncreaseBase(string stat,int amnt)
    {
        stats[stat].Base += amnt;
    }

    public void TakeDamage(int amnt)
    {
        stats["Health"].Base -= amnt;
    }

    public void GetFuel(int amnt)
    {
        stats["Fuel"].Base += amnt;
        if (stats["Fuel"].Base > stats["MaxFuel"].Base) { stats["Fuel"].Base = stats["MaxFuel"].Base; }
    }


    public Stats FromJSON(Ship S)
    {

        try
        {
            string FilePath = Application.streamingAssetsPath + "/Stats/" + S.Name+"_Stats" + ".json";
            string json = File.ReadAllText(FilePath);
            try { JsonUtility.FromJsonOverwrite(json, this); }
            catch (Exception e)
            {
                Debug.LogError(S.Name + "_Stats has bad JSON\n" + e);
            }
        }
        catch (FileNotFoundException e)
        {
            File.Create(Application.streamingAssetsPath + "/Stats/" + S.Name + "_Stats" + ".json");
        }
        stats.FromLists(StatNames, StatList);
        return this;

    }
    public void ToJSON(Ship S)
    {
        ListPair<string,Stat> lp =stats.ToLists(StatNames,StatList);
        StatNames = lp.l1;
        StatList = lp.l2;
        Debug.Log("Saved " + S.Name+"_Stats" + " to json");
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(Application.streamingAssetsPath + "/Stats/" + S.Name + "_Stats" + ".json", json);
    }
}
