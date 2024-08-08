using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundsCaller : GlobalEntityCaller<AudioSource[]>
{
    public void PlayRandomSound()
    {
        var entity = GlobalSounds.entityById[id];
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
