using Battle;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CustomEditor(typeof (Ship),true)]
//[CustomEditor(typeof(EnemyShip))]
public class ShipEditor : Editor {

    public bool shouldShowStats=true;
    public bool shouldShowSkills = true;
    public Ship selected;
    public bool Loaded = false;
    public string[] Allskills;
    int prev;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Ship ship = (Ship)target;
        if (ship.GetComponent<PlayerShip>())
        {
            PlayerShip.Money = EditorGUILayout.IntField("Money", PlayerShip.Money);
        }
        
        shouldShowStats = EditorGUILayout.Toggle("Stats",shouldShowStats);
        if (ship.stats==null||ship.stats.stats.Count==0)
        {
            ship.FromJSON();
        }
        if (shouldShowStats)
        {
            ShowStats(ship);
        }
        shouldShowSkills = EditorGUILayout.Toggle("Skills", shouldShowSkills);
        if (shouldShowSkills)
        {
            ShowSkills(ship);
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


    public void ShowSkills(Ship s)
    {
        for (int i=0;i<s.SkillStrings.Count;i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("-")) {
                s.SkillStrings.RemoveAt(i);
            }
            EditorGUILayout.LabelField(s.SkillStrings[i]);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }
        if (Allskills == null) { Allskills = GetAllSkills(); }
        if (Allskills.Length == 0) { Allskills = GetAllSkills(); }
        int j = prev;
        prev=EditorGUILayout.Popup("Add Skill",j,Allskills);
        if(j!=prev && !s.SkillStrings.Contains(Allskills[prev]))
        {
            s.SkillStrings.Add(Allskills[prev]);
        }
    }

    public string[] GetAllSkills()
    {
        List<string> skills = new List<string>();
        DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath + "/Skills/JSON/");
        FileInfo[] f = d.GetFiles();
        foreach (FileInfo file in f)
        {
            if (file.Name.Split('.').Last() == "json")
            {
                skills.Add(file.Name.Split('.')[0]);
            }
        }
        return skills.OrderBy(x=>x).ToArray();
    }
}
