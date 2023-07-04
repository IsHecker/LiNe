using UnityEngine;

[CreateAssetMenu( menuName = "SceneData")]
public class ScenesData : ScriptableObject
{
    public static bool firstTimeOpened;
    public bool fpsActiveState;
    public RenderMode renderMode;
    public static Color trailColor = Color.white;
}
