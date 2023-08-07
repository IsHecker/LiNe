using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }
    public virtual void Awake() => Instance ??= this as T;
}