using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio {

    public static void PlaySound(this MonoBehaviour g, string soundName,bool OnScreen=false, float volume = 1, bool randomPitch = false)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(g.transform.position);
        bool InView = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (OnScreen&&!InView)
        {
            return;
        }
        SoundEffects sfx = g.GetComponent<SoundEffects>();
        if (sfx == null)
        {
            SoundEffects.DefaultSounds.PlaySound(soundName, volume);
            return;
        }
        else
        {
            sfx.PlaySound(soundName, volume);
        }
    }

}
