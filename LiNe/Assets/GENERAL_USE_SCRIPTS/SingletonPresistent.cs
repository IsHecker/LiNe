using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPresistent<T> : MonoBehaviour where T : Component
{
    
    private static T _instance = null;
    //public static T Instance { get { _instance ??= new SingletonPresistent(); return _instance; } }

    //private SingletonPresistent() {  }
    //public static T Instance 
    //{
    //    get
    //    {
    //        if(_instance is null)
    //        {

    //        }
    //    }
    //}
    
}
