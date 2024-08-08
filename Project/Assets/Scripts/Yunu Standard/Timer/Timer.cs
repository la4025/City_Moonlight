using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
using System.Linq;
#endif

public class Timer : MonoBehaviour
{
    [Serializable]
    public class UpdateFloatEvent : UnityEvent<float> { }
    [Serializable]
    public class UpdateListener
    {
        public UpdateFloatEvent listenerEvent;
        public AnimationCurve curve;
        // if set true, time input is interpolated between 0~1
        public bool normalizedTime = true;
        //if set true, animation curve input is estimated as time & curveLength
        public bool modTimeByCurveLength = false;
        // if set true, delta riemannSum of the curve at a frame is passed to listener Event.
        public bool riemannSum = false;
    }

    [SerializeField]
    private float _elapsed, _playrate = 1.0f, _duration = 1;
    [SerializeField]
    private bool _isUnscaled = false, _active = false, _repeat = false;
    [SerializeField]
    private UnityEvent timerStartEvents;
    [SerializeField]
    private UnityEvent timeOutEvents;
    [SerializeField]
    private UpdateListener[] updateListener;
    public float playRate { get { return _playrate; } set { _playrate = value; } } // playRate of timer
    public float elapsed { get { return Mathf.Clamp(_elapsed, 0, duration); }  set { _elapsed = Mathf.Clamp(value, 0, duration); } } // elapsed time of timer
    public float duration { set { _duration = value; } get { return _duration; } }
    public float progress { get { return duration == 0 ? (_playrate > 0 ? 1 : 0) : Mathf.Clamp((_elapsed / duration), 0, 1); } set { _elapsed = value * duration; } }
    public bool active { get { return _active; } set { _active = value; } }
    public bool repeat { get { return _repeat; } set { _repeat = value; } } // if set true, timer restarts after elapsed time reaches duration
    public bool isUnscaled { get { return _isUnscaled; } }
    public void Invoke()
    {
        _elapsed = _playrate > 0 ? 0 : duration;
        timerStartEvents.Invoke();
        active = true;
    }
    public void Pause()
    {
        active = false;
    }
    public void Resume()
    {
        active = true;
    }
    public void InvokeTimeoutEvents()
    {
        timeOutEvents.Invoke();
    }
    public void ResetElapsedTime(){
        elapsed = playRate >= 0 ? 0 : duration;
    }
    protected virtual void Update()
    {
        if (active)
        {
            elapsed += (_isUnscaled) ? Time.unscaledDeltaTime * _playrate : Time.deltaTime * _playrate;
            foreach (UpdateListener each in updateListener)
            {
                float input = each.normalizedTime ? progress : elapsed;
                if (each.modTimeByCurveLength)
                    input %= each.curve.keys[each.curve.length - 1].time;
                input = each.curve.Evaluate(input);

                if (each.riemannSum)
                {
                    input *= Time.deltaTime * playRate;
                    if (each.normalizedTime)
                        input /= duration;
                }
                each.listenerEvent.Invoke(input);
            }
            if (_playrate > 0)
            {
                if (elapsed == duration)
                {
                    if (repeat)
                        elapsed -= duration;
                    else
                        active = false;
                    timeOutEvents.Invoke();
                }
            }
            else
            {
                if (elapsed == 0)
                {
                    if (repeat)
                        elapsed += duration;
                    else
                        active = false;
                    timeOutEvents.Invoke();
                }
            }
        }
    }
    protected virtual void OnValidate()
    {
#if UNITY_EDITOR
        if (updateListener == null)
            return;
        foreach (UpdateFloatEvent each in updateListener.Select((UpdateListener element) => element.listenerEvent))
        {
            AutoRegistration.AutoAttachPersistentMethod(each, "Invoke");
        }
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(timerStartEvents, "Invoke");
        UnityEditor.Events.AutoRegistration.AutoAttachMethod(timeOutEvents, "Invoke");
#endif
    }
}
