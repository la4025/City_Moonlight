using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TextFormatter : MonoBehaviour
{
    [SerializeField]
    private Text target;
    [SerializeField]
    [TextArea(3, 20)]
    private string text;
    [SerializeField]
    private StringContainer[] arguments;

    public void Refresh()
    {
        if (target)
            target.text = string.Format(text, arguments.Select(t => t.Value).ToArray());
    }
    private void OnValidate()
    {
        Refresh();
    }
    private void Reset()
    {
        target = GetComponent<Text>();
        arguments = GetComponentsInChildren<StringContainer>();
    }
}
