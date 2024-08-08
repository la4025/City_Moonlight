using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAnimatorCaller : GlobalEntityCaller<Animator>
{
    public void SetTrigger(string name) {
        GlobalAnimator.entityById[id].SetTrigger(name);
    }
    public void ResetTrigger(string name) { 
        GlobalAnimator.entityById[id].ResetTrigger(name);
    }
    public void SetTrigger(int index) {
        GlobalAnimator.entityById[id].SetTrigger(index);
    }
    public void ResetTrigger(int index) { 
        GlobalAnimator.entityById[id].ResetTrigger(index);
    }
}
