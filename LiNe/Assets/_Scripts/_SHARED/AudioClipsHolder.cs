using UnityEngine;

[CreateAssetMenu()]
public class AudioClipsHolder : ScriptableObject
{
    public Audio[] audioClips = { new Audio { ClipName = "Death" }, new Audio { ClipName = "Tap" } };
}
