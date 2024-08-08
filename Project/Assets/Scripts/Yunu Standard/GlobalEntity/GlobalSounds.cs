using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSounds : GlobalEntity<AudioSource[]>
{
    private void Reset()
    {
        entity = GetComponents<AudioSource>();
    }
    public static void PlayRandomSound(string entityId)
    {
        var entity = GlobalSounds.entityById[entityId];
        var length = entity.Length;
        var i = Random.Range(0, length);
        for (int delta = 0; delta < length; delta++)
        {
            if (!entity[(i + delta)%length].isPlaying)
            {
                entity[(i + delta)%length].Play();
                break;
            }
        }
    }
}
