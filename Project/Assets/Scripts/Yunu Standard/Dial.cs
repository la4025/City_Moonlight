using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//public class Dial : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler
//{
//    [SerializeField]
//    private bool clockwise=true;
//    [SerializeField]
//    private float position;
//    [SerializeField]
//    private string graduationFormat="{0}";
//    [SerializeField]
//    private int interval = 1, min = 1, max = 100;
//    [SerializeField]
//    private (Text text,float deltaRotation)[] graduationTexts;
//    private RectTransform rectTransform { get { return transform as RectTransform; } }
//    private void Update()
//    {
//        float rotation;
//        foreach(var each in graduationTexts)
//        {
//            Vector3 position = new Vector3();
//            position.x = each.deltaRotation+*rectTransform.rect.width / 2;
//            each.text.rectTransform.localPosition;
//        }
//    }
//#if UNITY_EDITOR
//    private void OnValidate()
//    {
//        List<(Text, float)> textPairs = new List<(Text, float)>();
//        var texts = GetComponentsInChildren<Text>();
//        for(int i = 0; i < texts.Length; i++)
//        {
//            textPairs.Add((texts[i], i*2*Mathf.PI/texts.Length));
//        }
//        graduationTexts = textPairs.ToArray();
//    }
//#endif
//Onhandle
//}
