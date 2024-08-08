using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// 달빛이 밝아지면 - emissive 밝아짐 - 디렉셔널 라이트가 밝아짐 - 텍스쳐 어두워짐 - 창문빛이 꺼짐 - 림라이트 밝아짐
/// 달빛이 어두워지면 - emissive 어두워짐 - 디렉셔널 라이트가 어두워짐 - 텍스쳐 밝아짐 - 창문빛이 켜짐 - 림라이트 어두워짐
/// 
/// 달빛 (슬라이더)
/// Moon Material 의 Emissive
/// Directional Light
/// RimLight Material의 BaseColor Intensity
/// RimLight Material의 Emissive
/// RimLight Material의 RimLight Intensity
/// </summary>

[ExecuteInEditMode]
public class SC_MoonLight : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float Moonlight = 0.0f;

    public Light directionalLight;
    public Material MTMoon;
    public Material MTRimLight;


    // Start is called before the first frame update
    void Start()
    {
        directionalLight = GetComponentInChildren<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        directionalLight.intensity = Moonlight / 10.0f;

        MTMoon.SetFloat("_Emissive_Intensity", Moonlight / 10.0f);

        MTRimLight.SetFloat("_BaseColorIntensity", (10.0f - Moonlight) / 10.0f);
        MTRimLight.SetFloat("_Emissive_Map_Intensity", (10.0f - Moonlight) / 10.0f);
        MTRimLight.SetFloat("_strength", (Moonlight) / 10.0f);

        //foreach (Material mat in MTRimLight)
        //{
        //    mat.SetFloat("_TextureIntensity", 1.0f - Moonlight);
        //    //Debug.Log(1.0f - Moonlight);
        //}
        
    }
}
