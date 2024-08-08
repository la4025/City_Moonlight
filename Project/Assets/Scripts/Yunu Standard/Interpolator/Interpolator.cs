using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif

     //          //
    //   🚘    //
   //   🚘    //
  //   🚘    //
 //   🧍‍     //
//          //

public abstract class Interpolator : MonoBehaviour
{
    [SerializeField]
    float A = 0, B = 1;
    protected float inverselerp(float point) { return A == B ? 0 : (point - A) / (B - A); }
    public abstract void Setter(float point);
    public void Invoke(float point) { Setter(point); }
#if UNITY_EDITOR
    public void RegisterPersistentSetter(UnityEvent<float> listener, int index)
    {
        System.Reflection.MethodInfo methodInfo = UnityEvent.GetValidMethodInfo(this, "Setter", new Type[] { typeof(float) });
        UnityAction<float> unityAction = Delegate.CreateDelegate(typeof(UnityAction<float>),this, methodInfo) as UnityAction<float>;
        UnityEventTools.RegisterPersistentListener(listener, index, unityAction);
    }
#endif
}
