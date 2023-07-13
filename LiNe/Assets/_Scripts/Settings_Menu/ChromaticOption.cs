using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class ChromaticOption : IOptionable
{
    private ChromaticAberration chromatic;
    private void Awake() => transform.parent.GetComponent<Volume>().profile.TryGet<ChromaticAberration>(out chromatic);

    public override void SwitchState(SwitchBehaviour switchValue) => chromatic.active = switchValue.GetSwitchState();

    public override object CaptureSate() => new SaveData { chromaticState = chromatic.active };

    public override void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        chromatic.active = savedData.chromaticState;
    }

    [Serializable]
    struct SaveData { public bool chromaticState; }

}
