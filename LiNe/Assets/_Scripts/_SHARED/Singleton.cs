using System.Collections;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : class
{
    private static T _instance;

    public static T Instance => _instance;

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}
