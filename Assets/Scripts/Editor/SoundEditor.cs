using Extensions.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundEffects))]
[CanEditMultipleObjects]
[ExecuteInEditMode]

public class SoundEditor : Editor
{


    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        SoundEffects s = ((SoundEffects) target);
        Event e = Event.current;
        SoundEffects SFX = ((SoundEffects) target);
        Dictionary<string, AudioClip> sfx = SFX.sfx ?? new Dictionary<string, AudioClip>();
        /*  sfx.ToLists(names, sounds);
     for (int i = 0; i < names.Count; i++)
      {
          EditorGUILayout.BeginHorizontal();
          names[i] = EditorGUI.TextField(new Rect(10, i * 10, 20, 20), "");
          EditorGUILayout.EndHorizontal();
      }*/
        if (SFX == null)
        {
            SFX = new SoundEffects();
            Debug.Log("new SFX");
        }
        if (SFX.names == null)
        {
            SFX.names = new List<string>();
        }
        if (SFX.names.Count > 0)
        {
            for (int i = 0; i < SFX.names.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-"))
                {
                    SFX.names.RemoveAt(i);
                    SFX.sounds.RemoveAt(i);
                    
                }
                SFX.names[i] = GUILayout.TextField(SFX.names[i]);
                if (SFX.sounds[i] == null)
                {
                    SFX.sounds[i] = (AudioClip)EditorGUILayout.ObjectField(new AudioClip(), typeof(AudioClip), true);
                }
                else
                {
                    SFX.sounds[i] = (AudioClip)EditorGUILayout.ObjectField(SFX.sounds[i], typeof(AudioClip), true);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        //GUILayout.text(SFX.sounds.Count);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            //    Debug.Log("test");
            SFX.names.Add("");
            SFX.sounds.Add(new AudioClip());
        }
        if (GUILayout.Button("Clear"))
        {
            //    Debug.Log("test");
            SFX.names.Clear();
            SFX.sounds.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update"))
        {
            //    Debug.Log("test");
            if (SFX.sfx == null)
            {
                SFX.sfx = new Dictionary<string, AudioClip>();
            }
            SFX.sfx.FromLists(SFX.names, SFX.sounds);
            Debug.Log(SFX.sfx.Count);
        }
        EditorGUILayout.EndHorizontal();
    }

}

