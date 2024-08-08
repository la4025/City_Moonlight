using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowDeltatime : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Update()
    {
        text.text = Time.deltaTime.ToString();
    }
}
