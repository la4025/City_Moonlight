using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
using System;
using Unity.VisualScripting;

public class Tutorial : MonoBehaviour
{
    [Serializable]
    public enum TutorialPhase
    {
        NotInTutorial,
        RestrainingUserInput,
        GuidingHold,
        GuidingUnhold,
    }
    public TutorialPhase currentPhase { get; set; }
    public static Tutorial instance { get; private set; }
    [SerializeField]
    private GameObject holdGuider;
    [SerializeField]
    private GameObject unholdGuider;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        switch (currentPhase)
        {
            case TutorialPhase.RestrainingUserInput:
                break;
            case TutorialPhase.GuidingHold:
                holdGuider.SetActive(!Input.GetButton("Jump"));
                Time.timeScale = Input.GetButton("Jump") ? 1 : 0;
                break;
            case TutorialPhase.GuidingUnhold:
                if (Input.GetButton("Jump"))
                {
                    unholdGuider.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    unholdGuider.SetActive(false);
                    currentPhase = TutorialPhase.RestrainingUserInput;
                    Time.timeScale = 1;
                }
                break;
        }
    }
}
