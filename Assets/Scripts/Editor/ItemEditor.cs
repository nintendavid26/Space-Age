using Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;

public class ItemEditor : EditorWindow {

    public Item item = new Item();
    public string LUA;
    public int defaultPos;
    public string[] Items;
    public Vector2 luaPos = new Vector2(100, 100);
    public bool ShowLUA = true;

    [MenuItem("Window/Items")]
    public static void Init()
    {
        ItemEditor window = (ItemEditor)GetWindow(typeof(ItemEditor));
        window.Show();
        window.SetUp();


    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        int prev = defaultPos;
        defaultPos = EditorGUILayout.Popup(defaultPos, Items);
        if (prev != defaultPos)
        {
            LoadItem(Items[defaultPos]);
        }
        if (GUILayout.Button("Save"))
        {
            Save();
            Items = GetAllItems();

        }

        if (GUILayout.Button("Delete"))
        {
            Delete(Items[defaultPos]);

        }

        if (GUILayout.Button("New Item"))
        {
            item = new Item();
            LUA = "--" + "\nfunction Use()\n\nend";
        }

        EditorGUILayout.EndHorizontal();
        item.Name = EditorGUILayout.TextField("Name", item.Name);
        item.Description = EditorGUILayout.TextField("Description", item.Description);
        item.Cost = EditorGUILayout.IntField("Cost", item.Cost);
        item.TargetAllies = EditorGUILayout.Toggle("Can target allies", item.TargetAllies);
        item.effectType = (Item.EffectType)EditorGUILayout.EnumPopup("Type:", item.effectType);

        if (item.effectType==Item.EffectType.Heal)
        {
            item.Power = EditorGUILayout.IntField("Power", item.Power);
        }
        else if (item.effectType == Item.EffectType.Buff)
        {
            item.Power = EditorGUILayout.IntField("Power", item.Power);
            item.Stat = EditorGUILayout.TextField("Stat", item.Stat);
            item.BuffDuration = EditorGUILayout.IntField("Duration", item.BuffDuration);
        }

        else
        {
            if (LUA.Contains("\t"))
            {
                LUA = LUA.Replace("\t", "    ");
            }

            LUA = EditorGUILayout.TextArea(LUA.Replace("\t", "   "), GUILayout.Height(position.height - 200));
        }
    }

    public void SetUp()
    {
        Items = GetAllItems();
        LoadItem(Items[0]);
    }

    public void Save()
    {
        item.ToJSON();
        File.WriteAllText(Application.streamingAssetsPath + "/Items/LUA/" + item.Name + ".lua", LUA);
    }

    private void LoadItem(string s)
    {
        item.FromJSON(Items[defaultPos]);
        if (File.Exists(Application.streamingAssetsPath + "/Items/LUA/" + s + ".lua"))
        {
            LUA = File.ReadAllText(Application.streamingAssetsPath + "/Items/LUA/" + s + ".lua");
        }
        else
        {
            LUA = "--" + s + "\nfunction Use()\n\nend";
        }
    }

    private string[] GetAllItems()
    {
        List<string> items = new List<string>();
        DirectoryInfo d = new DirectoryInfo(Application.streamingAssetsPath + "/Items/JSON/");
        FileInfo[] f = d.GetFiles();
        foreach (FileInfo file in f)
        {
            if (file.Name.Split('.').Last() == "json")
            {
                items.Add(file.Name.Split('.')[0]);
            }
        }
        return items.ToArray();
    }

    void Delete(string item)
    {
        File.Delete(Application.streamingAssetsPath + "/Items/LUA/" + item + ".lua");
        File.Delete(Application.streamingAssetsPath + "/Items/JSON/" + item + ".json");
        Items = GetAllItems();
        LoadItem(Items[0]);


    }
}
