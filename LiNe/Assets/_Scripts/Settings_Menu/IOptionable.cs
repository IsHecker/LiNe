using Save_System;
using UnityEngine;

public abstract class IOptionable : MonoBehaviour, ISaveable
{
    public abstract object CaptureSate();

    public abstract void RestoreState(object state);

    public abstract void SwitchState(SwitchBehaviour switchValue);
    
}
