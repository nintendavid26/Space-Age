using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for SkillParser, so it also has a bunch of helper functions for lua
/// </summary>
public class BattlePrefabs : MonoBehaviour {

    public static BattlePrefabs p;

    [Serializable]
    public struct NamedPrefab
    {
        public string name;
        public GameObject Object;
    }
    public NamedPrefab[] objects;

    public Dictionary<string, GameObject> prefabs=new Dictionary<string, GameObject>();

    public void Awake()
    {
        p = this;

        for (int i = 0; i < objects.Length; i++)
        {
            prefabs.Add(objects[i].name, objects[i].Object);
        }

    }

    public float deltaTime()
    {
        return Time.deltaTime;
    }

    public Vector3 MoveTowards(Vector3 a,Vector3 b, float c)
    {
        return Vector3.MoveTowards(a,b,c);
    }

    public GameObject Make(string s,Vector3 pos){
        if (prefabs.ContainsKey(s))
        {
            return Instantiate(prefabs[s], pos, Quaternion.identity);
        }
        return null;
    }
    public T Make<T> (string s, Vector3 pos) where T : MonoBehaviour
    {
        if (prefabs.ContainsKey(s))
        {
            return Instantiate(prefabs[s], pos, Quaternion.identity).GetComponent<T>();
        }
        return null;
    }
    public GameObject Make(string s, Vector3 pos,Quaternion rot)
    {
        return Instantiate(prefabs[s], pos, rot);
    }
    public GameObject this[string s]{
        get { return prefabs[s]; }
        set { prefabs[s] = value; }
    }

    public LightningBolt MakeBolt(Vector3 pos,Vector3 Target)
    {
        LightningBolt Bolt=Instantiate(prefabs["Lightning"], pos,Quaternion.identity).GetComponent<LightningBolt>();
        Bolt.Initialize(pos,Target);
        return Bolt;
    }

    public void Log(string S)
    {
        Debug.Log("Lua: " + S);
       
    }

    
	
}
