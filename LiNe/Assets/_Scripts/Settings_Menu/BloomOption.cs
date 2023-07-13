using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;


public class BloomOption : IOptionable
{
    private Bloom bloom;
    private void Awake() => transform.parent.GetComponent<Volume>().profile.TryGet<Bloom>(out bloom);

    public override void SwitchState(SwitchBehaviour switchValue) => bloom.active = switchValue.GetSwitchState();

    public override object CaptureSate() => new SaveData { bloomState = bloom.active };

    public override void RestoreState(object state) 
    {
        SaveData savedData = (SaveData)state;
        bloom.active = savedData.bloomState;
    }

    [Serializable]
    struct SaveData { public bool bloomState; }
}
