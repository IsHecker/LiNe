using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class D_GameModeSetup : ScriptableObject
{
    public GameObject modePrefab;
    public GameObject playerPrefab;

    public Vector3 playerOffset;
    public Vector3 camerasOffset;
    public Vector3 angleOffset;

    public float prepareTime = 2f;

}
