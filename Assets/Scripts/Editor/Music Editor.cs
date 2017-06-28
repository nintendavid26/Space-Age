using Extensions.Collections;
using Helper_Scripts;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Music))]
[CanEditMultipleObjects]
[ExecuteInEditMode]

public class MusicEditor : Editor
{

    
    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        Music s = ((Music)target);
        Event e = Event.current;
        Music music = ((Music)target);
        music.CurrentSong= (AudioClip)EditorGUILayout.ObjectField(music.CurrentSong, typeof(AudioClip), true);
        Dictionary<string, AudioClip> sfx = music.SongsDict ?? new Dictionary<string, AudioClip>();
        /*  sfx.ToLists(names, sounds);
     for (int i = 0; i < names.Count; i++)
      {
          EditorGUILayout.BeginHorizontal();
          names[i] = EditorGUI.TextField(new Rect(10, i * 10, 20, 20), "");
          EditorGUILayout.EndHorizontal();
      }*/
        if (music == null)
        {
            music = new Music();
            Debug.Log("new music");
        }
        if (music.names == null)
        {
            music.names = new List<string>();
        }
        if (music.names.Count > 0)
        {
            for (int i = 0; i < music.names.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button("-"))
                {
                    music.names.RemoveAt(i);
                    music.Songs.RemoveAt(i);

                }
                music.names[i] = GUILayout.TextField(music.names[i]);
                if (music.Songs[i] == null)
                {
                    music.Songs[i] = (AudioClip)EditorGUILayout.ObjectField(new AudioClip(), typeof(AudioClip), true);
                }
                else
                {
                    music.Songs[i] = (AudioClip)EditorGUILayout.ObjectField(music.Songs[i], typeof(AudioClip), true);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        //GUILayout.text(music.sounds.Count);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("+"))
        {
            //    Debug.Log("test");
            music.names.Add("");
            music.Songs.Add(new AudioClip());
        }
        if (GUILayout.Button("Clear"))
        {
            //    Debug.Log("test");
            music.names.Clear();
            music.Songs.Clear();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update"))
        {
            //    Debug.Log("test");
            if (music.SongsDict == null)
            {
                music.SongsDict = new Dictionary<string, AudioClip>();
            }
            music.SongsDict.FromLists(music.names, music.Songs);
            Debug.Log(music.SongsDict.Count);
        }
        EditorGUILayout.EndHorizontal();
    }
    
}

