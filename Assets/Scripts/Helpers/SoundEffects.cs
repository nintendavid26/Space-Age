using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Helper_Scripts;
using Extensions.Collections;

[Serializable]
public class SoundEffects : MonoBehaviour
{
    public Dictionary<string, AudioClip> sfx;
    public string test;
    public List<string> names, prevNames;
    public List<AudioClip> sounds, prevSounds;
    public bool Updated;
    public float DefaultVolume;
    public static SoundEffects DefaultSounds;

    public void PlaySound(string Sound)
    {
        PlaySound(Sound, DefaultVolume);
    }

    public void Start()
    {
        if (DefaultSounds == null)
        {
            DefaultSounds = GameObject.Find("Sound Effects").GetComponent<SoundEffects>();
        }
        if (DefaultSounds == this)
        {
            DontDestroyOnLoad(this);
        }
    }

    public void PlaySound(string Sound, float volume)
    {
        //Debug.Log(Music.Source.pitch);
        if (sfx == null)
        {
            sfx = new Dictionary<string, AudioClip>();
            sfx.FromLists(names, sounds);
        }
        // Debug.Log(sfx["Jump"]);
        if (sfx.ContainsKey(Sound)) {
            AudioClip s = sfx[Sound];
            Music.PlaySound(s, volume);
            Music.Source.pitch = 1;
        }
        else if (this!=DefaultSounds)
        {
            DefaultSounds.PlaySound(Sound, volume);
        }
        else {
            Debug.Log("Doesn't contain sound effect " + Sound);
        }
    }

}