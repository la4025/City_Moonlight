using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Reflection;

/*
 * AutoRegistration은 Unityevent에 콜백함수를 손쉽게 할당하기 위한 기능을 탑재하고 있습니다.
 * 여기에 탑재된 기능들이 어떻게 활용되는지 살펴보고 싶다면, BoolBranch 코드를 확인하세요.
 * 구조가 단순하면서도, AutoRegistration 클래스의 기능이 어떻게 활용될 수 있는지 쉽게 파악할 수 있게 코드가 짜여져 있습니다.
 */
namespace UnityEditor.Events
{
#if UNITY_EDITOR
    public static class AutoRegistration
    {
        //public static void AddEmptyMethod<T0>(Component component, UnityEvent<T0> unityEvent)
        //{
        //    var unityAction = new UnityAction<T0>(component=> { }); 
        //        //Delegate.CreateDelegate(typeof(UnityAction<T0>), component,new MethodInfo()) as UnityAction<T0>;
        //    UnityEventTools.RegisterPersistentListener(unityEvent, unityEvent.GetPersistentEventCount(), unityAction);
        //}
        public static void AutoAttachPersistentMethod<T0>(UnityEvent<T0> unityEvent, string methodName)
        {
            if (unityEvent is null)
                return;
            Type[] methodArgType = new Type[1] { typeof(T0) };
            foreach (var each in GetEmptyCallIndexes(unityEvent))
            {
                (Component component, System.Reflection.MethodInfo methodInfo) infos = GetValidInstance(unityEvent, methodName, methodArgType, each);
                if (infos.component != null)
                {
                    UnityAction<T0> unityAction = Delegate.CreateDelegate(typeof(UnityAction<T0>), infos.component, infos.methodInfo) as UnityAction<T0>;
                    UnityEventTools.RegisterPersistentListener(unityEvent, each, unityAction);
                }
            }
        }
        public static void AutoAttachPersistentMethod(UnityEvent unityEvent, string methodName)
        {
            if (unityEvent is null)
                return;
            foreach (var each in GetEmptyCallIndexes(unityEvent))
            {
                (Component component, MethodInfo methodInfo) infos = GetValidInstance(unityEvent, methodName, new Type[0], each);
                if (infos.component != null)
                {
                    UnityAction unityAction = Delegate.CreateDelegate(typeof(UnityAction), infos.component, infos.methodInfo) as UnityAction;
                    UnityEventTools.RegisterPersistentListener(unityEvent, each, unityAction);
                }
            }
        }
        public static void AutoAttachMethodWithLiteral<T>(UnityEventBase unityEventBase, string methodName, T argument)
        {
            if (unityEventBase is null)
                return;
            foreach (var index in GetEmptyCallIndexes(unityEventBase))
            {
                (Component component, MethodInfo methodInfo) infos;
                infos = GetValidInstance(unityEventBase, methodName, new Type[1] { typeof(T) }, index);
                if (infos.component != null)
                {
                    switch (argument)
                    {
                        case bool arg:
                            UnityAction<bool> unityBoolAction = Delegate.CreateDelegate(typeof(UnityAction<bool>), infos.component, infos.methodInfo) as UnityAction<bool>;
                            UnityEventTools.RegisterBoolPersistentListener(unityEventBase, index, unityBoolAction, arg);
                            break;
                        case float arg:
                            UnityAction<float> unityFloatAction = Delegate.CreateDelegate(typeof(UnityAction<float>), infos.component, infos.methodInfo) as UnityAction<float>;
                            UnityEventTools.RegisterFloatPersistentListener(unityEventBase, index, unityFloatAction, arg);
                            break;
                        case int arg:
                            UnityAction<int> unityIntAction = Delegate.CreateDelegate(typeof(UnityAction<int>), infos.component, infos.methodInfo) as UnityAction<int>;
                            UnityEventTools.RegisterIntPersistentListener(unityEventBase, index, unityIntAction, arg);
                            break;
                        case string arg:
                            UnityAction<string> unityStringAction = Delegate.CreateDelegate(typeof(UnityAction<string>), infos.component, infos.methodInfo) as UnityAction<string>;
                            UnityEventTools.RegisterStringPersistentListener(unityEventBase, index, unityStringAction, arg);
                            break;
                    }
                }

            }
        }
        public static void AutoAttachMethodWithObject<T>(UnityEventBase unityEventBase, string methodName, T argument) where T : UnityEngine.Object
        {
            if (unityEventBase is null)
                return;
            foreach (var index in GetEmptyCallIndexes(unityEventBase))
            {
                (Component component, MethodInfo methodInfo) infos;
                infos = GetValidInstance(unityEventBase, methodName, new Type[1] { typeof(T) }, index);
                if (infos.component != null)
                {
                    UnityAction<T> unityObjectAction = Delegate.CreateDelegate(typeof(UnityAction<T>), infos.component, infos.methodInfo) as UnityAction<T>;
                    UnityEventTools.RegisterObjectPersistentListener(unityEventBase, index, unityObjectAction, argument);
                }
            }
        }
        public static void AutoAttachMethod(UnityEventBase unityEventBase, string methodName)
        {
            if (unityEventBase is null)
                return;
            foreach (var index in GetEmptyCallIndexes(unityEventBase))
            {
                (Component component, MethodInfo methodInfo) infos;
                infos = GetValidInstance(unityEventBase, methodName, new Type[0], index);
                if (infos.component != null)
                {
                    UnityAction unityAction = Delegate.CreateDelegate(typeof(UnityAction), infos.component, infos.methodInfo) as UnityAction;
                    UnityEventTools.RegisterVoidPersistentListener(unityEventBase, index, unityAction);
                }
            }
        }
        static (Component, System.Reflection.MethodInfo) GetValidInstance(UnityEventBase unityEvent, string methodName, Type[] argType, int index)
        {
            if (!(unityEvent.GetPersistentTarget(index) is GameObject))
                throw new Exception("AttachMethod only works when type of persistent target is gameobject.");
            GameObject persistent_target = unityEvent.GetPersistentTarget(index) as GameObject;
            foreach (Component component in persistent_target.GetComponents<Component>())
            {
                //System.Reflection.MethodInfo method_info = component.GetType().GetMethod(methodName);
                var property = component.GetType().GetProperty(methodName);
                MethodInfo methodInfo = null;
                if (property != null)
                {
                    methodInfo = property.GetSetMethod();
                    //methodInfo = UnityEvent.GetValidMethodInfo(component, methodName, argType);
                }
                if (methodInfo == null)
                    methodInfo = UnityEvent.GetValidMethodInfo(component, methodName, argType);
                if (methodInfo != null)
                    return (component, methodInfo);
            }
            return (null, null);
        }
        public static int[] GetEmptyCallIndexes(UnityEventBase unityEvent)
        {
            List<int> index_set = new List<int>();
            for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
            {
                if (unityEvent.GetPersistentMethodName(i) == "" && unityEvent.GetPersistentTarget(i) is GameObject)
                    index_set.Add(i);
            }
            return index_set.ToArray();
        }
    }
#endif
}
