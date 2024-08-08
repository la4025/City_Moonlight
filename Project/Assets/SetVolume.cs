using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    public void SetGlobalVolume(float volume)
    {
        foreach (var each in GlobalSounds.entityById)
        {
            foreach (var each2 in each.Value)
            {
                each2.volume = volume;
            }
        }
    }
}
