using Battle;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

//TODO Combine this with the skill tree somehow
public class SkillEditor: EditorWindow {

    public BattleSkill Skill=new BattleSkill();
    public string LUA;
    public int defaultPos;
    public string[] Skills;
    public Vector2 luaPos = new Vector2(100, 100);
    public bool ShowLUA=true;

    [MenuItem("Window/Skills")]
    public static void Init() {
        SkillEditor window = (SkillEditor)GetWindow(typeof(SkillEditor));
        window.Show();
        window.SetUp();
        

    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        defaultPos=EditorGUILayout.Popup(defaultPos, Skills);
        if (GUILayout.Button("Load")) {
            LoadSkill(Skills[defaultPos]);
        }
        if (GUILayout.Button("Save"))
        {
            Save();
            Skills = GetAllSkills();

        }
        if (GUILayout.Button("New Skill"))
        {
            Skill=new BattleSkill();
            LUA = "--"+"\nfunction Use()\n\nend";
        }

        EditorGUILayout.EndHorizontal();
        Skill.Name=EditorGUILayout.TextField("Name", Skill.Name);
        Skill.Description = EditorGUILayout.TextField("Description", Skill.Description);
        Skill.Category = EditorGUILayout.TextField("Category", Skill.Category);
        Skill.Auto = EditorGUILayout.Toggle("Auto",Skill.Auto);
        if (!Skill.Auto)
        {
            Skill.SingleTarget = EditorGUILayout.Toggle("SingleTarget", Skill.SingleTarget);
            Skill.Power = EditorGUILayout.IntField("Power", Skill.Power);
            Skill.Cost = EditorGUILayout.IntField("Cost", Skill.Cost);
            Skill.target= (BattleCommand.TargetType)EditorGUILayout.EnumPopup("Target Type:", Skill.target);
            Skill.e = (element)EditorGUILayout.EnumPopup("Element:", Skill.e);
        }

        ShowLUA = EditorGUILayout.Toggle("LUA:", ShowLUA);
        if (ShowLUA)
        {
            if (LUA.Contains("\t"))
            {
                LUA = LUA.Replace("\t", "    ");
            }

            LUA = EditorGUILayout.TextArea(LUA.Replace("\t", "   "), GUILayout.Height(position.height - 200));
        }
    }

    public static string[] GetAllSkills()
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
        return skills.ToArray();
    }

    void LoadSkill(string s)
    {
        Skill.FromJSON(Skills[defaultPos]);
        if (File.Exists(Application.streamingAssetsPath + "/Skills/LUA/" + s + ".lua"))
        {
            LUA = File.ReadAllText(Application.streamingAssetsPath + "/Skills/LUA/" + s + ".lua");
        }
        else
        {
            LUA = "--" + s + "\nfunction Use()\n\nend";
        }

    }

    public void Save()
    {
        Skill.ToJSON();
        File.WriteAllText(Application.streamingAssetsPath + "/Skills/LUA/" + Skill.Name + ".lua", LUA);
    }

    public void SetUp()
    {
        Skills = GetAllSkills();
        LoadSkill(Skills[0]);



    }
    void LuaEditor()
    {
       
        
    }

}
