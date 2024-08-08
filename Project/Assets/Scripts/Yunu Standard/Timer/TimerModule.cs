using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TimerModule
{
    public float duration = 1f;
    public float playrate = 1f;
    public bool repeat = false;
    public bool isTimeScaled = true;
    public bool isTimeNormalized = true;
    public AnimationCurve timeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public UnityAction<float> updateAction;
    public UnityAction expireAction;
    public float elapsed { get; private set; } = 0;
    public bool active { get; private set; }
    public IEnumerator StartTimer()
    {
        elapsed = playrate > 0 ? 0 : duration;
        active = true;
        while (true)
        {
            if (!active)
            {
                yield return null;
                continue;
            }
            elapsed += playrate * (isTimeScaled ? Time.deltaTime : Time.unscaledDeltaTime);
            float alpha;
            if (duration != 0)
                alpha = isTimeNormalized ? elapsed / duration : elapsed;
            else
                alpha = 1f;
            alpha = Mathf.Clamp(alpha, 0, isTimeNormalized ? 1 : duration);
            if (updateAction != null)
                updateAction.Invoke(timeCurve.Evaluate(alpha));
            if (elapsed > duration && playrate > 0)
            {
                if (expireAction != null)
                    expireAction.Invoke();
                if (repeat)
                    elapsed = 0;
                else
                    break;
            }
            if (elapsed < 0 && playrate < 0)
            {
                if (expireAction != null)
                    expireAction.Invoke();
                if (repeat)
                    elapsed = duration;
                else
                    break;
            }

            yield return null;
        }
    }
    public void Pause()
    {
        active = false;
    }
    public void Resume()
    {
        active = true;
    }
}
