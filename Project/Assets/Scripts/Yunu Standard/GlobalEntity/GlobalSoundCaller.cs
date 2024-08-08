using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundCaller : GlobalEntityCaller<AudioSource>
{
    public void Play()
    {
        GlobalSound.entityById[id].Play();
    }
    public void Pause()
    {
        GlobalSound.entityById[id].Pause();
    }
    public void Stop()
    {
        GlobalSound.entityById[id].Stop();
    }
    public float volume
    {
        get { return GlobalSound.entityById[id].volume; }
        set { GlobalSound.entityById[id].volume = value; }
    }
    public float time
    {
        get { return GlobalSound.entityById[id].time; }
        set { GlobalSound.entityById[id].time = value; }
    }
    public float pitch
    {
        get { return GlobalSound.entityById[id].pitch; }
        set { GlobalSound.entityById[id].pitch = value; }
    }
    public bool loop
    {
        get { return GlobalSound.entityById[id].loop; }
        set { GlobalSound.entityById[id].loop = value; }
    }
}
