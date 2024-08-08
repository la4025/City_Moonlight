using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

/// <summary>
/// �޺��� ������� - emissive ����� - �𷺼ų� ����Ʈ�� ����� - �ؽ��� ��ο��� - â������ ���� - ������Ʈ �����
/// �޺��� ��ο����� - emissive ��ο��� - �𷺼ų� ����Ʈ�� ��ο��� - �ؽ��� ����� - â������ ���� - ������Ʈ ��ο���
/// 
/// �޺� (�����̴�)
/// Moon Material �� Emissive
/// Directional Light
/// RimLight Material�� BaseColor Intensity
/// RimLight Material�� Emissive
/// RimLight Material�� RimLight Intensity
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
