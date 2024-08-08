using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSound : GlobalEntity<AudioSource>
{
    private void Reset()
    {
        entity = GetComponent<AudioSource>();
    }
}
