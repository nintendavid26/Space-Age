using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StatsWindow : EditorWindow
{
    public Ship ship;
    public Stats stats;
    public List<string> StatNames=new List<string>();

    public static void Init(Ship s)
    {
        Debug.Log("init");
        StatsWindow window = (StatsWindow)GetWindow(typeof(StatsWindow));
        window.ship = s;
        window.stats = s.stats;
        window.Show();
        window.Initialize();

    }

    void Initialize()
    {

    }

    void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            foreach(string s in StatNames) {
                Debug.Log(s);
            }
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name:");
        EditorGUILayout.LabelField("Base:");
        EditorGUILayout.LabelField("Modified:");
        EditorGUILayout.EndHorizontal();
        foreach (Stat stat in stats.StatTypes())
        {
            EditorGUILayout.BeginHorizontal();
            stat.Name = EditorGUILayout.TextField(stat.Name);
            string name = stat.Name;
            stat.Base = EditorGUILayout.IntField(stat.Base);
            EditorGUILayout.EndHorizontal();

        }

        if (GUILayout.Button("+"))
        {
            stats.Set(new Stat("New", 0));
        }

    }

}
