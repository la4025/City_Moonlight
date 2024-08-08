using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class RadioMenu<OptionType> : MonoBehaviour where OptionType : Behaviour
{
    [SerializeField]
    protected OptionType[] options;
    [SerializeField]
    protected OptionType currentOption;
    [SerializeField]
    private bool isListCircular = false;
    protected int currentIndex { get; private set; } = 0;
    public void Start()
    {
        if (currentOption)
            OnSelect(currentOption);
        foreach (var each in options)
        {
            if (each != currentOption)
            {
                OnDeselect(each);
            }
        }
    }
    public void Select(OptionType option)
    {
        if (option == currentOption)
            return;
        if (currentOption)
            OnDeselect(currentOption);
        currentOption = option;
        OnSelect(option);
        for (int i = 0; i < options.Length; i++)
            if (options[i] == option)
                currentIndex = i;
    }
    public void Deselect()
    {
        if (null == currentOption)
            return;
        if (currentOption)
            OnDeselect(currentOption);
        currentOption = null;
    }
    public void Toggle(OptionType option)
    {
        if (option is null)
            return;
        if (option == currentOption)
        {
            OnDeselect(option);
            currentOption = null;
        }
        else
        {
            Select(option);
        }
    }
    public void Toggle()
    {
        Toggle(currentOption);
    }
    public void SelectPrevious()
    {
        var optionBefore = currentOption;
        SelectByIndex(currentIndex - 1);
        if (optionBefore != currentOption)
            OnPreviousSelect(currentOption);
    }
    public void SelectNext()
    {
        var optionBefore = currentOption;
        SelectByIndex(currentIndex + 1);
        if (optionBefore != currentOption)
            OnNextSelect(currentOption);
    }
    public void SelectByIndex(int index)
    {
        if (options.Length == 0)
            Debug.LogError("List is empty!!");
        if (index < 0)
            index = (isListCircular ? options.Length - 1 : 0);
        if (index >= options.Length)
            index = (isListCircular ? 0 : options.Length - 1);
        var option = options[index];
        if (option == currentOption)
            return;
        if (currentOption)
            OnDeselect(currentOption);
        currentOption = option;
        currentIndex = index;
        OnSelect(option);
    }
    protected virtual void OnSelect(OptionType option) { }
    protected virtual void OnDeselect(OptionType option) { }
    protected virtual void OnPreviousSelect(OptionType option) { }
    protected virtual void OnNextSelect(OptionType option) { }
}
