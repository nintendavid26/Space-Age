using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CustomEditor(typeof (Ship),true)]
//[CustomEditor(typeof(EnemyShip))]
public class ShipEditor : Editor {

    public bool shouldShowStats=true;
    public Ship selected;
    public bool Loaded = false;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Ship ship = (Ship)target;
        
        
        shouldShowStats = EditorGUILayout.Toggle("Stats",shouldShowStats);
        if (ship.stats==null||ship.stats.stats.Count==0)
        {
            ship.FromJSON();
        }
        if (shouldShowStats)
        {
            ShowStats(ship);
        }
        
    }

    public void ShowStats(Ship S)
    {
        Stats stats = S.stats;
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 50;
        EditorGUILayout.LabelField("Name:");
        EditorGUILayout.LabelField("Base:");
        EditorGUILayout.LabelField("Modified:");
        EditorGUILayout.EndHorizontal();
        List<Stat> tempList = new List<Stat>();
        foreach (Stat stat in S.stats.StatTypes())
        {
            EditorGUILayout.BeginHorizontal();
            
            stat.Name = EditorGUILayout.TextField(stat.Name);
            stat.Base = EditorGUILayout.IntField(stat.Base);
            EditorGUILayout.LabelField(stat.Modified+"");
            tempList.Add(stat);
            EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save"))
        {
            S.stats.Clear();
            foreach (Stat stat in tempList)
            {
                S.stats.Set(stat);
            }

            serializedObject.ApplyModifiedProperties();
            S.ToJSON();

        }

        if (GUILayout.Button("Load"))
        {
            S.FromJSON();
        }

        if (GUILayout.Button("+"))
        {
            S.stats.Set(new Stat("New", 0,S));
        }

        if (GUILayout.Button("Clear"))
        {
            S.stats = new Stats();
        }

        EditorGUILayout.EndHorizontal();
    }

}
